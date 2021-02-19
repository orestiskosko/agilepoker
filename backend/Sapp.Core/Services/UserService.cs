using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Sapp.Common;
using Sapp.Core.Entities;
using Sapp.Core.Interfaces;
using Sapp.Core.Persistence;

namespace Sapp.Core.Services
{
    public class UserService : IUserService
    {
        private readonly ApiContext _apiContext;
        private readonly IMapper<User, UserDto> _mapper;

        public UserService(ApiContext apiContext, IMapper<User, UserDto> mapper)
        {
            _apiContext = apiContext;
            _mapper = mapper;
        }

        public async Task<UserDto> GetAsync(Guid id, CancellationToken token = default)
        {
            var user = await _apiContext.Users.FindAsync(
                new object[]
                {
                    id
                },
                token);

            return user is null
                ? null
                : _mapper.Map(user);
        }

        public async Task<UserDto> CreateGuestAsync(CreateGuestRequest request, CancellationToken token = default)
        {
            var moment = DateTimeOffset.UtcNow;
            var user = new User
            {
                Username = request.Username,
                IsGuest = true,
                CreatedAt = moment,
                UpdatedAt = moment
            };

            var result = _apiContext.Users.Add(user);
            await _apiContext.SaveChangesAsync(token);

            return _mapper.Map(result.Entity);
        }

        public async Task<IEnumerable<UserDto>> RemoveAsync(
            IEnumerable<Guid> ids,
            CancellationToken token = default)
        {
            var users = await _apiContext.Users
                .Where(u => ids.Contains(u.Id))
                .ToListAsync(token);

            _apiContext.Users.RemoveRange(users);
            await _apiContext.SaveChangesAsync(token);

            return _mapper.Map(users);
        }

        public async Task<UserDto> RemoveAsync(Guid id, CancellationToken token = default)
        {
            var user = await _apiContext.Users.FirstOrDefaultAsync(u => u.Id == id, token);

            if (user is null) return null;

            _apiContext.Users.Remove(user);
            await _apiContext.SaveChangesAsync(token);

            return _mapper.Map(user);
        }
    }
}