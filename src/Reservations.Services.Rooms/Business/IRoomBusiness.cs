﻿using Reservations.Services.Common.Types;
using Reservations.Services.Rooms.Commands;
using Reservations.Services.Rooms.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Reservations.Services.Rooms.Business
{
    public interface IRoomBusiness : IBusinessBase
    {
        public Task CreateAsync(CreateRoomCommand command);

        public Task<List<RoomViewModel>> AvailableRoomsByOfficeAsync(GetAvailableRoomsByOfficeCommand command);

        public Task AddResourceAsync(AddResourceToRoomCommand command);

        public Task<List<RoomResourceViewModel>> GetRoomsResourcesAsync();

        public Task<List<RoomResourceViewModel>> GetRoomResources(GetRoomResourcesCommand command);
    }
}
