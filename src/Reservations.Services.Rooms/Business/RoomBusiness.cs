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

        public async Task<List<RoomViewModel>> AvailableRoomsByOfficeAsync(GetAvailableRoomsByOfficeCommand command)
        {
            Check.NotNull(command, nameof(command));

            return await _applicationContext.Rooms
                                            .Where(x => x.OfficeId == command.OfficeId)
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

        public async Task<List<RoomResourceViewModel>> GetRoomsResourcesAsync()
        {
            return await AllRoomsResources()
                         .ToListAsync();
        }

        public async Task<List<RoomResourceViewModel>> GetRoomResources(GetRoomResourcesCommand command)
        {
            return await AllRoomsResources()
                              .Where(x => x.RoomId == command.RoomId)
                              .ToListAsync();
        }

        public IQueryable<RoomResourceViewModel> AllRoomsResources()
        {
            return (from room_resources in _applicationContext.RoomResources
                    join rooms in _applicationContext.Rooms on room_resources.RoomId equals rooms.Id
                    join resources in _applicationContext.Resources on room_resources.ResourceId equals resources.Id
                    select new
                    {
                        room_resources.RoomId,
                        RoomName = rooms.Name,
                        room_resources.ResourceId,
                        ResourceName = resources.Name,
                        resources.TotalQuantity,
                        room_resources.Quantity
                    })
                    .Select(x => new RoomResourceViewModel
                    {
                        RoomId = x.RoomId,
                        RoomName = x.RoomName,
                        ResourceId = x.ResourceId,
                        ResourceName = x.ResourceName,
                        ResourceTotalQuantity = x.TotalQuantity,
                        ResourceUsedQuantity = x.Quantity
                    });
        }
    }
}
