using AutoMapper;
using MassTransit;
using Microsoft.Extensions.Caching.Distributed;
using Reservations.Common.Shared;
using Reservations.Services.Common.Types;
using Reservations.Services.Contracts.Requests.Offices;
using Reservations.Services.Rooms.Commands;
using Reservations.Services.Rooms.Data;
using Reservations.Services.Rooms.Entities;
using System;
using System.Threading.Tasks;

namespace Reservations.Services.Rooms.Business
{
    public class RoomBusiness : BusinessBase, IRoomBusiness
    {
        private readonly ApplicationContext _applicationContext;
        private readonly IMapper _mapper;
        private readonly IDistributedCache _distributedCache;
        private readonly IBus _bus;

        public RoomBusiness(ApplicationContext applicationContext,
                              IMapper mapper,
                              IDistributedCache distributedCache,
                              IBus bus)
        {
            _applicationContext = applicationContext;
            _mapper = mapper;
            _distributedCache = distributedCache;
            _bus = bus;
        }

        public async Task CreateAsync(CreateRoomCommand command)
        {
            Check.NotNull(command, nameof(command));

            var response = await _bus.Request<CheckOfficeExistence, OfficeExistenceRespond>(new
            {
                command.OfficeId
            });

            if (response.Message.Exists == false)
            {
                throw new Exception($"Office not found with id: {command.OfficeId}");
            }

            Room room = _mapper.Map<Room>(command);

            await _applicationContext.Rooms.AddAsync(room);

            await _applicationContext.SaveChangesAsync();
        }
    }
}
