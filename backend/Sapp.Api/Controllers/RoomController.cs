using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Sapp.Common;
using Sapp.Core.Interfaces;

namespace Sapp.Api.Controllers
{
    [Route("rooms")]
    public class RoomController : Controller
    {
        private readonly IRoomService _roomService;

        public RoomController(IRoomService roomService)
        {
            _roomService = roomService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRoomAsync(
            [FromRoute] Guid id,
            CancellationToken token = default)
        {
            var result = await _roomService.GetAsync(id, token);
            return result is null
                ? NotFound()
                : Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateRoomAsync(
            [FromBody] CreateRoomRequest request,
            CancellationToken token = default)
        {
            var result = await _roomService.CreateAsync(request, token);
            return Created(nameof(GetRoomAsync), result);
        }

        [HttpPost("{id}/users/{userId}/add")]
        public async Task<IActionResult> AddParticipantAsync(
            [FromRoute] Guid id,
            [FromRoute] Guid userId,
            CancellationToken token = default)
        {
            var result = await _roomService.AddParticipantAsync(id, userId, token);
            return Ok(result);
        }

        [HttpPost("{id}/users/{userId}/remove")]
        public async Task<IActionResult> RemoveParticipantAsync(
            [FromRoute] Guid id,
            [FromRoute] Guid userId,
            CancellationToken token = default)
        {
            var result = await _roomService.RemoveParticipantAsync(id, userId, token);
            return Ok(result);
        }

        [HttpPost("{id}/items")]
        public async Task<IActionResult> SetItemsAsync(
            [FromRoute] Guid id,
            [FromBody] SetRoomItemsRequest request,
            CancellationToken token = default)
        {
            var result = await _roomService.SetItemsAsync(id, request, token);
            return Ok(result);
        }

        [HttpPost("{id}/items/{itemId}/set")]
        public async Task<IActionResult> SetSelectedItemAsync(
            [FromRoute] Guid id,
            [FromRoute] Guid itemId,
            CancellationToken token = default)
        {
            var result = await _roomService.SetSelectedItemAsync(id, itemId, token);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRoomAsync(
            [FromRoute] Guid id,
            CancellationToken token = default)
        {
            var _ = await _roomService.RemoveAsync(id, token);
            return NoContent();
        }
    }
}