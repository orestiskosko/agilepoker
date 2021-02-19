using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Sapp.Common;
using Sapp.Common.Enums;
using Sapp.Core.Interfaces;

namespace Sapp.Core.Hubs
{
    public class VotingHub : Hub
    {
        private readonly IUserService _userService;
        private readonly IRoomService _roomService;

        public VotingHub(
            IUserService userService,
            IRoomService roomService)
        {
            _userService = userService;
            _roomService = roomService;
        }

        public async Task JoinRoom(string roomId, string userId)
        {
            var user = await _userService.GetAsync(Guid.Parse(userId));
            await _roomService.AddParticipantAsync(Guid.Parse(roomId), user.Id);
            await Groups.AddToGroupAsync(Context.ConnectionId, roomId);
            await Clients.Group(roomId).SendAsync("userJoinedRoom", user);
        }

        public async Task LeaveRoom(string roomId, string userId)
        {
            var user = await _userService.GetAsync(Guid.Parse(userId));
            await _roomService.RemoveParticipantAsync(Guid.Parse(roomId), user.Id);
            await _userService.RemoveAsync(Guid.Parse(userId));
            await Clients.OthersInGroup(roomId).SendAsync("userLeftRoom", user);
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, roomId);
        }

        public async Task CloseRoom(string roomId)
        {
            var room = await _roomService.RemoveAsync(Guid.Parse(roomId));

            if (room is null)
            {
                await Clients.Caller.SendAsync("errorOccured", "Room was not found.");
                return;
            }

            await _userService.RemoveAsync(room.Participants.Select(p => p.Id));
            await Clients.Group(roomId).SendAsync("roomClosed", roomId);
        }

        public async Task SetItems(string roomId, SetRoomItemsRequest request)
        {
            var room = await _roomService.SetItemsAsync(Guid.Parse(roomId), request);
            await Clients.Group(roomId).SendAsync("itemsSet", room.Items);
        }

        public async Task SetSelectedItem(string roomId, string itemId)
        {
            await _roomService.SetSelectedItemStatusAsync(Guid.Parse(roomId), RoomItemStatus.Idle);
            var room = await _roomService.SetSelectedItemAsync(Guid.Parse(roomId), Guid.Parse(itemId));

            if (room is null)
            {
                await Clients.Caller.SendAsync("errorOccured", "Invalid room or item.");
                return;
            }

            var item = room.Items.First(i => i.Id == room.SelectedItemId);
            await Clients.OthersInGroup(roomId).SendAsync("selectedItemSet", item);
        }

        public async Task SendVote(
            string roomId,
            string userId,
            string itemId,
            string vote)
        {
            var roomIdGuid = Guid.Parse(roomId);
            var userIdGuid = Guid.Parse(userId);
            var itemIdGuid = Guid.Parse(itemId);

            var voteData = float.TryParse(vote, out var parsedVoteData)
                ? (float?) parsedVoteData
                : null;
            var room = await _roomService.SetUserVoteAsync(roomIdGuid, userIdGuid, itemIdGuid, voteData);

            if (room is null)
            {
                await Clients.Caller.SendAsync("errorOccured", "Room, user or item are invalid.");
                return;
            }

            var voteDto = room.Votes.FirstOrDefault(
                v => v.RoomId == roomIdGuid && v.UserId == userIdGuid && v.ItemId == itemIdGuid);
            await Clients.Group(roomId).SendAsync("userVoted", voteDto);

            var allUsersVoted = room.Participants.All(
                u =>
                {
                    var userVote = room.Votes.FirstOrDefault(v => v.UserId == u.Id && v.ItemId == room.SelectedItemId);
                    return userVote?.Vote is not null;
                });

            if (allUsersVoted)
            {
                await CompleteVoting(roomId);
            }
        }

        public async Task StartVoting(string roomId)
        {
            var room = await _roomService.SetStatus(Guid.Parse(roomId), RoomStatus.Voting);
            room = await _roomService.SetSelectedItemStatusAsync(room.Id, RoomItemStatus.Voting);
            await _roomService.ResetVotes(room.Id, room.SelectedItemId.GetValueOrDefault());
            await Clients.Group(roomId).SendAsync("votingStarted");
        }

        public async Task CompleteVoting(string roomId)
        {
            var room = await _roomService.SetStatus(Guid.Parse(roomId), RoomStatus.Idle);
            await _roomService.SetSelectedItemStatusAsync(Guid.Parse(roomId), RoomItemStatus.Voted);
            await Clients.Group(roomId).SendAsync("votingCompleted");
        }

        public async Task StopVoting(string roomId)
        {
            await _roomService.SetStatus(Guid.Parse(roomId), RoomStatus.Idle);
            await _roomService.SetSelectedItemStatusAsync(Guid.Parse(roomId), RoomItemStatus.Idle);
            await Clients.Group(roomId).SendAsync("votingStopped");
        }
    }
}