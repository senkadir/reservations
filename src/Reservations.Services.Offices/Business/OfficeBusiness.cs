using AutoMapper;
using AutoMapper.QueryableExtensions;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Reservations.Common.Shared;
using Reservations.Services.Common.Types;
using Reservations.Services.Contracts.Events;
using Reservations.Services.Offices.Commands;
using Reservations.Services.Offices.Data;
using Reservations.Services.Offices.Entities;
using Reservations.Services.Offices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Reservations.Services.Offices.Business
{
    public class OfficeBusiness : BusinessBase, IOfficeBusiness
    {
        private readonly ApplicationContext _applicationContext;
        private readonly IMapper _mapper;
        private readonly IDistributedCache _distributedCache;
        private readonly IBus _bus;

        public OfficeBusiness(ApplicationContext applicationContext,
                              IMapper mapper,
                              IDistributedCache distributedCache,
                              IBus bus)
        {
            _applicationContext = applicationContext;
            _mapper = mapper;
            _distributedCache = distributedCache;
            _bus = bus;
        }

        public async Task CreateAsync(CreateOfficeCommand command)
        {
            Check.NotNull(command, nameof(command));

            Office office = _mapper.Map<Office>(command);

            await _applicationContext.Offices.AddAsync(office);

            await _applicationContext.SaveChangesAsync();

            await _bus.Publish<OfficeCreated>(new
            {
                OfficeId = office.Id
            });
        }

        public async Task<List<OfficeViewModel>> GetAsync()
        {
            var cachedData = await _distributedCache.GetStringAsync(CacheKeys.Officess);

            List<OfficeViewModel> offices;

            if (cachedData == null)
            {
                offices = await _applicationContext.Offices
                                                   .ProjectTo<OfficeViewModel>(_mapper.ConfigurationProvider)
                                                   .ToListAsync();

                await _distributedCache.SetStringAsync(CacheKeys.Officess, JsonSerializer.Serialize(offices));
            }
            else
            {
                offices = JsonSerializer.Deserialize<List<OfficeViewModel>>(cachedData);
            }

            return offices;
        }

        public async Task<bool> ExistsAsync(Guid officeId)
        {
            return await _applicationContext.Offices
                                      .AnyAsync(x => x.Id == officeId);
        }

        public async Task<List<Guid>> AvailableOfficesAsync(CheckAvailableOfficesCommand command)
        {
            var availableOffices = await _applicationContext.Offices
                                                          .Where(x => x.OpenTime <= command.StartTime
                                                                   && x.CloseTime >= command.EndTime)
                                                          .Select(x => x.Id)
                                                          .ToListAsync();

            return availableOffices;
        }
    }
}
