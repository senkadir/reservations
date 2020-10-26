using AutoMapper;
using AutoMapper.QueryableExtensions;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using NpgsqlTypes;
using Reservations.Common.Mvc;
using Reservations.Common.Shared;
using Reservations.Services.Common.Types;
using Reservations.Services.Contracts.Requests;
using Reservations.Services.Contracts.Responds;
using Reservations.Services.Reservations.Commands;
using Reservations.Services.Reservations.Data;
using Reservations.Services.Reservations.Entities;
using Reservations.Services.Reservations.ExternalServices;
using Reservations.Services.Reservations.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Reservations.Services.Reservations.Business
{
    public class ReservationBusiness : BusinessBase, IReservationBusiness
    {
        private readonly IBus _bus;
        private readonly IRoomService _roomService;
        private readonly IMapper _mapper;
        private readonly ApplicationContext _applicationContext;
        private readonly IHttpContextAccessor _contextAccessor;

        public ReservationBusiness(IBus bus,
                                   IRoomService roomService,
                                   IMapper mapper,
                                   ApplicationContext applicationContext,
                                   IHttpContextAccessor contextAccessor)
        {
            _bus = bus;
            _roomService = roomService;
            _mapper = mapper;
            _applicationContext = applicationContext;
            _contextAccessor = contextAccessor;
        }

        public Guid UserId
        {
            get
            {
                var userClaim = _contextAccessor.HttpContext.User.Claims.Where(x => x.Type == ClaimTypes.NameIdentifier).FirstOrDefault();

                Check.NotNull(userClaim, nameof(userClaim));

                return Guid.Parse(userClaim.Value);
            }
        }

        public async Task<List<RoomViewModel>> CheckAvailabilityAsync(CheckAvailableRoomsCommand command)
        {
            Check.NotNull(command, nameof(command));

            var userLocationClaim = _contextAccessor.HttpContext.User.Claims.Where(x => x.Type == "location").FirstOrDefault();

            if (userLocationClaim == null)
            {
                throw new ServiceException("Can not find user is location");
            }

            var response = await _bus.Request<CheckOfficeAvailability, OfficeAvailabilityRespond>(new
            {
                Location = userLocationClaim.Value,
                StartTime = command.StartTime.TimeOfDay,
                EndTime = command.EndTime.TimeOfDay
            });

            if (response.Message.Available == false)
            {
                throw new ServiceException("Office not available provided times");
            }

            var rooms = await _roomService.GetRoomsByOfficeAsync(new GetAvailableRoomsByOfficeCommand
            {
                OfficeId = response.Message.OfficeId
            });

            return rooms;
        }

        public async Task CreateReservationAsync(CreateReservationCommand command)
        {
            Check.NotNull(command, nameof(command));

            await CheckAvailabilityAsync(command);

            Reservation reservation = _mapper.Map<Reservation>(command);

            reservation.CreatedBy = UserId;

            reservation.Resources.AddRange(command.Resources.Select(x => new Resource
            {
                Id = Guid.NewGuid(),
                ResourceId = x
            }));

            await _applicationContext.Reservations.AddAsync(reservation);

            await _applicationContext.SaveChangesAsync();
        }

        private async Task CheckAvailabilityAsync(CreateReservationCommand command)
        {
            var roomAvailablityTask = _bus.Request<CheckRoomCapacity, RoomCapacityRespond>(new
            {
                command.RoomId,
                command.PersonCount
            });

            var officeAvailabilityTask = _bus.Request<CheckOfficeHoursAvailable, OfficeHoursAvailabilityRespond>(new
            {
                command.OfficeId,
                StartTime = command.StartDate.TimeOfDay,
                EndTime = command.EndDate.TimeOfDay
            });

            var reservationAvailabilityTask = CheckReservationAvailable(new CheckReservationAvailableCommand
            {
                RoomId = command.RoomId,
                StartDate = command.StartDate,
                EndDate = command.EndDate
            });

            await Task.WhenAll(officeAvailabilityTask, roomAvailablityTask, reservationAvailabilityTask);

            var roomAvailabilityResponse = await roomAvailablityTask;

            var officeAvailabilityResponse = await officeAvailabilityTask;

            var reservationAvailabilityResponse = await reservationAvailabilityTask;

            if (roomAvailabilityResponse.Message.Available == false)
            {
                throw new ServiceException("Please make a selection according to the number of office capacity.");
            }

            if (officeAvailabilityResponse.Message.Available == false)
            {
                throw new ServiceException("Please make an appropriate choice for office hours.");
            }

            if (reservationAvailabilityResponse == false)
            {
                throw new ServiceException("Please choose available range of date to make an reservation");
            }
        }

        public async Task<bool> CheckReservationAvailable(CheckReservationAvailableCommand command)
        {
            var reservationDuration = new NpgsqlRange<DateTime>(command.StartDate, command.EndDate);

            var anyExists = await _applicationContext.Reservations
                                                     .Where(x => x.RoomId == command.RoomId
                                                              && x.Duration.Overlaps(reservationDuration))
                                                     .AnyAsync();

            return !anyExists;
        }

        public async Task<List<ReservationViewModel>> GetMyReservationsAsync()
        {
            return await _applicationContext.Reservations
                                     .Where(x => x.CreatedBy == UserId)
                                     .ProjectTo<ReservationViewModel>(_mapper.ConfigurationProvider)
                                     .ToListAsync();
        }
    }
}
