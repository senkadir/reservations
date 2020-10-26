using AutoMapper;
using Reservations.Services.Rooms.Commands;
using Reservations.Services.Rooms.Entities;
using Reservations.Services.Rooms.Models;
using System;

namespace Reservations.Services.Rooms.MapperProfiles
{
    public class ResourceProfile : Profile
    {
        public ResourceProfile()
        {
            CreateMap<CreateResourceCommand, Resource>().AfterMap((src, dest) =>
            {
                dest.Id = Guid.NewGuid();
            });

            CreateMap<Resource, ResourceViewModel>();
        }
    }
}
