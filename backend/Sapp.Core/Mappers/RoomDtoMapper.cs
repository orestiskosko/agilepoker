using System;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Sapp.Common;
using Sapp.Core.Entities;
using Sapp.Core.Interfaces;

namespace Sapp.Core.Mappers
{
    public class RoomDtoMapper : MapperBase<Room, RoomDto>
    {
        private readonly IMapper<User, UserDto> _userDtoMapper;

        public RoomDtoMapper(IMapper<User, UserDto> userDtoMapper)
        {
            _userDtoMapper = userDtoMapper;
        }

        public override RoomDto Map(Room source)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));

            return new RoomDto
            {
                Id = source.Id,
                Name = source.Name,
                LeaderId = source.RoomUsers.First(ru => ru.IsLeader).UserId,
                Participants = _userDtoMapper.Map(source.RoomUsers.Select(ru => ru.User)),
                SelectedItemId = source.SelectedItemId,
                Items = source.Items.Select(
                    i => new RoomItemDto
                    {
                        Id = i.Id,
                        Data = JsonConvert.DeserializeObject(i.Data),
                        Status = i.Status
                    }),
                Status = source.Status,
                Votes = source.Votes.Select(
                    ur => new VoteDto
                    {
                        RoomId = ur.RoomId,
                        UserId = ur.UserId,
                        ItemId = ur.ItemId,
                        Vote = ur.VoteData
                    })
            };
        }
    }
}