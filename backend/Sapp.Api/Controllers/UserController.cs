using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Sapp.Common;
using Sapp.Core.Interfaces;

namespace Sapp.Api.Controllers
{
    [Route("users")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(
            [FromRoute] Guid id,
            CancellationToken token)
        {
            var result = await _userService.GetAsync(id, token);
            return result is null
                ? NotFound()
                : Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateGuest(
            [FromBody] CreateGuestRequest request,
            CancellationToken token)
        {
            var result = await _userService.CreateGuestAsync(request, token);
            return Created(nameof(Get), result);
        }
    }
}