using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Sapp.Common;
using Sapp.Common.Enums;
using Sapp.Core.Entities;
using Sapp.Core.Interfaces;
using Sapp.Core.Persistence;

namespace Sapp.Core.Services
{
    public class RoomService : IRoomService
    {
        private readonly ApiContext _apiContext;
        private readonly IMapper<Room, RoomDto> _roomDtoMapper;

        public RoomService(ApiContext apiContext, IMapper<Room, RoomDto> roomDtoMapper)
        {
            _apiContext = apiContext;
            _roomDtoMapper = roomDtoMapper;
        }

        public async Task<RoomDto> GetAsync(Guid id, CancellationToken token = default)
        {
            var room = await _apiContext.Rooms
                .Include(r => r.RoomUsers)
                .ThenInclude(ru => ru.User)
                .Include(r => r.Items)
                .Include(r => r.Votes)
                .FirstOrDefaultAsync(r => r.Id == id, token);

            return room is null
                ? null
                : _roomDtoMapper.Map(room);
        }

        public async Task<RoomDto> CreateAsync(CreateRoomRequest request, CancellationToken token = default)
        {
            var owner = await _apiContext.Users.FindAsync(
                new object[]
                {
                    request.OwnerId
                },
                token);

            var moment = DateTimeOffset.UtcNow;
            var room = new Room
            {
                Name = request.Name,
                RoomUsers = new List<RoomUser>
                {
                    new()
                    {
                        UserId = owner.Id,
                        IsLeader = true
                    }
                },
                Status = RoomStatus.Idle,
                CreatedAt = moment,
                UpdatedAt = moment,
            };

            _apiContext.Rooms.Add(room);
            await _apiContext.SaveChangesAsync(token);

            return await GetAsync(room.Id, token);
        }

        public async Task<RoomDto> AddParticipantAsync(
            Guid roomId,
            Guid userId,
            CancellationToken token = default)
        {
            var room = await _apiContext.Rooms
                .Include(r => r.RoomUsers)
                .FirstOrDefaultAsync(r => r.Id == roomId, token);

            var user = await _apiContext.Users
                .FindAsync(
                    new object[]
                    {
                        userId
                    },
                    token);

            if (!room.RoomUsers.Select(p => p.UserId).Contains(user.Id))
            {
                room.RoomUsers.Add(
                    new RoomUser
                    {
                        RoomId = room.Id,
                        UserId = user.Id,
                        IsLeader = false
                    });
            }

            room.UpdatedAt = DateTimeOffset.UtcNow;

            await _apiContext.SaveChangesAsync(token);

            return await GetAsync(room.Id, token);
        }

        public async Task<RoomDto> RemoveParticipantAsync(
            Guid roomId,
            Guid userId,
            CancellationToken token = default)
        {
            // TODO Throw if not found
            var room = await _apiContext.Rooms
                .Include(r => r.RoomUsers)
                .FirstOrDefaultAsync(r => r.Id == roomId, token);

            // TODO Throw if not found
            var roomUser = room.RoomUsers.FirstOrDefault(u => u.UserId == userId);

            room.RoomUsers.Remove(roomUser);
            room.UpdatedAt = DateTimeOffset.UtcNow;

            var user = await _apiContext.Users.FirstOrDefaultAsync(u => u.Id == userId, token);
            if (user.IsGuest)
            {
                _apiContext.Users.Remove(user);
            }

            var userVotes = await _apiContext.Votes.Where(v => v.UserId == user.Id).ToListAsync(token);
            _apiContext.Votes.RemoveRange(userVotes);

            await _apiContext.SaveChangesAsync(token);

            return await GetAsync(room.Id, token);
        }

        public async Task<RoomDto> SetItemsAsync(
            Guid roomId,
            SetRoomItemsRequest request,
            CancellationToken token = default)
        {
            var room = await _apiContext.Rooms
                .Include(r => r.Items)
                .FirstOrDefaultAsync(r => r.Id == roomId, token);

            if (room is null) return null;

            _apiContext.Items.RemoveRange(room.Items);
            await _apiContext.SaveChangesAsync(token);

            room.Items = request.Issues.Select(
                i => new Item
                {
                    RoomId = roomId,
                    Data = i.ToString()
                }).ToArray();

            await _apiContext.SaveChangesAsync(token);

            return await GetAsync(roomId, token);
        }

        public async Task<RoomDto> SetSelectedItemAsync(
            Guid roomId,
            Guid itemId,
            CancellationToken token = default)
        {
            var room = await _apiContext.Rooms.FirstOrDefaultAsync(r => r.Id == roomId, token);
            var item = await _apiContext.Items.FirstOrDefaultAsync(i => i.Id == itemId, token);

            if (room is null || item is null) return null;

            room.SelectedItemId = itemId;

            await _apiContext.SaveChangesAsync(token);

            return await GetAsync(roomId, token);
        }

        public async Task<RoomDto> SetSelectedItemStatusAsync(
            Guid roomId,
            RoomItemStatus status,
            CancellationToken token = default)
        {
            var room = await _apiContext.Rooms.FirstOrDefaultAsync(r => r.Id == roomId, token);
            var item = await _apiContext.Items.FirstOrDefaultAsync(i => i.Id == room.SelectedItemId, token);

            if (room is null || item is null) return null;

            item.Status = status;

            await _apiContext.SaveChangesAsync(token);

            return await GetAsync(roomId, token);
        }

        public async Task<RoomDto> SetStatus(
            Guid roomId,
            RoomStatus status,
            CancellationToken token = default)
        {
            var room = await _apiContext.Rooms.FirstOrDefaultAsync(r => r.Id == roomId, token);

            if (room is null) return null;

            room.Status = status;
            room.UpdatedAt = DateTimeOffset.UtcNow;

            await _apiContext.SaveChangesAsync(token);

            return await GetAsync(roomId, token);
        }

        public async Task<RoomDto> SetUserVoteAsync(
            Guid roomId,
            Guid userId,
            Guid itemId,
            float? voteData,
            CancellationToken token = default)
        {
            var vote = await _apiContext.Votes.FirstOrDefaultAsync(
                r => r.RoomId == roomId && r.UserId == userId && r.ItemId == itemId,
                token);

            if (vote is null)
            {
                vote = new Vote
                {
                    RoomId = roomId,
                    UserId = userId,
                    ItemId = itemId
                };
                _apiContext.Add(vote);
            }

            vote.VoteData = voteData;

            await _apiContext.SaveChangesAsync(token);

            return await GetAsync(roomId, token);
        }

        public async Task<RoomDto> ResetVotes(
            Guid roomId,
            Guid itemId,
            CancellationToken token = default)
        {
            var votes = await _apiContext
                .Votes
                .Where(v => v.RoomId == roomId && v.ItemId == itemId)
                .ToListAsync(token);

            foreach (var vote in votes)
            {
                vote.VoteData = null;
            }

            await _apiContext.SaveChangesAsync(token);

            return await GetAsync(roomId, token);
        }

        public async Task<RoomDto> RemoveAsync(Guid roomId, CancellationToken token = default)
        {
            var room = await _apiContext.Rooms
                .Include(r => r.RoomUsers)
                .ThenInclude(ru => ru.User)
                .Include(r => r.Items)
                .Include(r => r.Votes)
                .FirstOrDefaultAsync(r => r.Id == roomId, token);

            if (room is null) return null;

            _apiContext.Rooms.Remove(room);
            await _apiContext.SaveChangesAsync(token);

            return _roomDtoMapper.Map(room);
        }
    }
}