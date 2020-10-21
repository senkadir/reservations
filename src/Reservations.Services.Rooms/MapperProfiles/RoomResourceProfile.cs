using AutoMapper;
using Reservations.Services.Rooms.Commands;
using Reservations.Services.Rooms.Entities;
using System;

namespace Reservations.Services.Rooms.MapperProfiles
{
    public class RoomResourceProfile : Profile
    {
        public RoomResourceProfile()
        {
            CreateMap<AddResourceToRoomCommand, RoomResource>().AfterMap((src, dest) =>
            {
                dest.Id = Guid.NewGuid();
            });
        }
    }
}
