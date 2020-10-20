using AutoMapper;
using Reservations.Services.Offices.Commands;
using Reservations.Services.Offices.Entities;
using Reservations.Services.Offices.Models;
using System;

namespace Reservations.Services.Offices.MapperProfiles
{
    public class OfficeProfile : Profile
    {
        public OfficeProfile()
        {
            CreateMap<CreateOfficeCommand, Office>().AfterMap((src, dest) =>
            {
                dest.Id = Guid.NewGuid();
            });

            CreateMap<Office, OfficeViewModel>();
        }
    }
}
