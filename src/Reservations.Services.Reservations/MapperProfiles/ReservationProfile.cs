using AutoMapper;
using NpgsqlTypes;
using Reservations.Services.Reservations.Commands;
using Reservations.Services.Reservations.Entities;
using Reservations.Services.Reservations.Models;
using System;
using System.Collections.Generic;

namespace Reservations.Services.Reservations.MapperProfiles
{
    public class ReservationProfile : Profile
    {
        public ReservationProfile()
        {
            CreateMap<CreateReservationCommand, Reservation>().ForMember(x => x.Resources, opt => opt.Ignore()).AfterMap((src, dest) =>
              {
                  dest.Id = Guid.NewGuid();
                  dest.Duration = new NpgsqlRange<DateTime>(src.StartDate, src.EndDate);
                  dest.Resources = new List<Resource>();
              });

            CreateMap<Reservation, ReservationViewModel>()
                .ForMember(x => x.Start, opt => opt.MapFrom(src => src.Duration.LowerBound))
                .ForMember(x => x.End, opt => opt.MapFrom(src => src.Duration.UpperBound));

            CreateMap<Resource, ReservationResourceViewModel>();
        }
    }
}
