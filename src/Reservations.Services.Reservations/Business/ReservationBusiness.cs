using MassTransit;
using Reservations.Services.Common.Types;
using Reservations.Services.Contracts.Requests;
using Reservations.Services.Contracts.Responds;
using Reservations.Services.Reservations.Commands;
using Reservations.Services.Reservations.ExternalServices;
using Reservations.Services.Reservations.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Reservations.Services.Reservations.Business
{
    public class ReservationBusiness : BusinessBase, IReservationBusiness
    {
        private readonly IBus _bus;
        private readonly IRoomService _roomService;

        public ReservationBusiness(IBus bus,
                                   IRoomService roomService)
        {
            _bus = bus;
            _roomService = roomService;
        }

        public async Task<List<RoomViewModel>> CheckAvailabilityAsync(CheckAvailableRoomsCommand command)
        {
            var response = await _bus.Request<CheckOfficeAvailability, OfficeAvailabilityRespond>(new
            {
                StartTime = command.StartTime.TimeOfDay,
                EndTime = command.EndTime.TimeOfDay
            });

            if (response.Message.AvailableOffices.Count == 0)
            {
                return new List<RoomViewModel>();
            }

            var rooms = await _roomService.GetRoomsByOfficeAsync(new GetAvailableRoomsByOfficesCommand
            {
                Offices = response.Message.AvailableOffices
            });

            return rooms;
        }
    }
}
