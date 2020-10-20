using AutoMapper;
using Reservations.Services.Rooms.Commands;
using Reservations.Services.Rooms.Entities;
using System;

namespace Reservations.Services.Rooms.MapperProfiles
{
    public class RoomProfile : Profile
    {
        public RoomProfile()
        {
            CreateMap<CreateRoomCommand, Room>().AfterMap((src, dest) =>
            {
                dest.Id = Guid.NewGuid();
            });
        }
    }
}
