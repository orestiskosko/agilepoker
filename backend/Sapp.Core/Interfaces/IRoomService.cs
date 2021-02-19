using System;
using System.Threading;
using System.Threading.Tasks;
using Sapp.Common;
using Sapp.Common.Enums;

namespace Sapp.Core.Interfaces
{
    public interface IRoomService
    {
        Task<RoomDto> GetAsync(Guid id, CancellationToken token = default);
        Task<RoomDto> CreateAsync(CreateRoomRequest request, CancellationToken token = default);

        Task<RoomDto> AddParticipantAsync(
            Guid roomId,
            Guid userId,
            CancellationToken token = default);

        Task<RoomDto> RemoveParticipantAsync(
            Guid roomId,
            Guid userId,
            CancellationToken token = default);

        Task<RoomDto> SetItemsAsync(
            Guid roomId,
            SetRoomItemsRequest request,
            CancellationToken token = default);

        Task<RoomDto> SetSelectedItemAsync(
            Guid roomId,
            Guid itemId,
            CancellationToken token = default);

        Task<RoomDto> SetSelectedItemStatusAsync(
            Guid roomId,
            RoomItemStatus status,
            CancellationToken token = default);

        Task<RoomDto> SetStatus(
            Guid roomId,
            RoomStatus status,
            CancellationToken token = default);

        Task<RoomDto> SetUserVoteAsync(
            Guid roomId,
            Guid userId,
            Guid itemId,
            float? voteData,
            CancellationToken token = default);

        Task<RoomDto> ResetVotes(
            Guid roomId,
            Guid itemId,
            CancellationToken token = default);

        Task<RoomDto> RemoveAsync(Guid roomId, CancellationToken token = default);
    }
}