using AutoMapper;
using AutoMapper.QueryableExtensions;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Reservations.Common.Mvc;
using Reservations.Common.Shared;
using Reservations.Services.Common.Types;
using Reservations.Services.Contracts.Requests;
using Reservations.Services.Contracts.Responds;
using Reservations.Services.Rooms.Commands;
using Reservations.Services.Rooms.Data;
using Reservations.Services.Rooms.Entities;
using Reservations.Services.Rooms.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reservations.Services.Rooms.Business
{
    public class RoomBusiness : BusinessBase, IRoomBusiness
    {
        private readonly ApplicationContext _applicationContext;
        private readonly IMapper _mapper;
        private readonly IBus _bus;

        public RoomBusiness(ApplicationContext applicationContext,
                              IMapper mapper,
                              IBus bus)
        {
            _applicationContext = applicationContext;
            _mapper = mapper;
            _bus = bus;
        }

        public async Task<List<RoomViewModel>> AvailableRoomsByOfficesAsync(GetAvailableRoomsByOfficesCommand command)
        {
            Check.NotNull(command, nameof(command));

            Check.NotNull(command.Offices, nameof(command.Offices));

            return await _applicationContext.Rooms
                                            .Where(x => command.Offices.Contains(x.OfficeId))
                                            .ProjectTo<RoomViewModel>(_mapper.ConfigurationProvider)
                                            .ToListAsync();
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
                throw new ServiceException($"Office not found with id: {command.OfficeId}");
            }

            Room room = _mapper.Map<Room>(command);

            await _applicationContext.Rooms.AddAsync(room);

            await _applicationContext.SaveChangesAsync();
        }

        public async Task AddResourceAsync(AddResourceToRoomCommand command)
        {
            Check.NotNull(command, nameof(command));

            var resource = await _applicationContext.Resources
                                                    .Where(x => x.Id == command.ResourceId)
                                                    .FirstOrDefaultAsync();

            Check.NotNull(resource, nameof(resource));

            if (resource.Specific)
            {
                var checkSpecificResourceAlreadyAdded = await _applicationContext.RoomResources
                                                                                           .Where(x => x.Resource.Specific && x.RoomId == command.RoomId)
                                                                                           .AnyAsync();

                if (checkSpecificResourceAlreadyAdded)
                {
                    throw new ServiceException("Can not add two specific resource to the same room");
                }
            }

            var usedResourcesQuantity = await _applicationContext.RoomResources
                                                                 .Where(x => x.ResourceId == command.ResourceId)
                                                                 .SumAsync(x => x.Quantity);

            if (usedResourcesQuantity + command.Quantity > resource.TotalQuantity)
            {
                throw new ServiceException("There is no available resource");
            }

            RoomResource roomResource = _mapper.Map<RoomResource>(command);

            await _applicationContext.RoomResources.AddAsync(roomResource);

            await _applicationContext.SaveChangesAsync();
        }
    }
}
