using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Sapp.Common;

namespace Sapp.Core.Interfaces
{
    public interface IUserService
    {
        public Task<UserDto> GetAsync(Guid id, CancellationToken token = default);
        public Task<UserDto> CreateGuestAsync(CreateGuestRequest request, CancellationToken token = default);
        public Task<IEnumerable<UserDto>> RemoveAsync(IEnumerable<Guid> ids, CancellationToken token = default);
        public Task<UserDto> RemoveAsync(Guid id, CancellationToken token = default);
    }
}