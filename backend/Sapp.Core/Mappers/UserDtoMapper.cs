using System;
using Sapp.Common;
using Sapp.Core.Entities;

namespace Sapp.Core.Mappers
{
    public class UserDtoMapper : MapperBase<User, UserDto>
    {
        public override UserDto Map(User source)
        {
            if (source is null) throw new ArgumentNullException(nameof(source));

            return new UserDto
            {
                Id = source.Id,
                Username = source.Username,
                IsGuest = source.IsGuest,
            };
        }
    }
}