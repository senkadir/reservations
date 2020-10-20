using AutoMapper;
using NpgsqlTypes;
using Reservations.Services.Reservations.Commands;
using Reservations.Services.Reservations.Entities;
using System;

namespace Reservations.Services.Reservations.MapperProfiles
{
    public class ReservationProfile : Profile
    {
        public ReservationProfile()
        {
            CreateMap<CreateReservationCommand, Reservation>().AfterMap((src, dest) =>
            {
                dest.Id = Guid.NewGuid();
                dest.Duration = new NpgsqlRange<DateTime>(src.StartDate, src.EndDate);
            });
        }
    }
}
