using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Reservations.Common.Shared;
using Reservations.Services.Common.Types;
using Reservations.Services.Rooms.Commands;
using Reservations.Services.Rooms.Data;
using Reservations.Services.Rooms.Entities;
using Reservations.Services.Rooms.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Reservations.Services.Rooms.Business
{
    public class ResourceBusiness : BusinessBase, IResourceBusiness
    {
        private readonly ApplicationContext _applicationContext;
        private readonly IMapper _mapper;

        public ResourceBusiness(ApplicationContext applicationContext,
                                IMapper mapper)
        {
            _applicationContext = applicationContext;
            _mapper = mapper;
        }

        public async Task CreateResourceAsync(CreateResourceCommand command)
        {
            Check.NotNull(command, nameof(command));

            Resource resource = _mapper.Map<Resource>(command);

            await _applicationContext.Resources.AddAsync(resource);

            await _applicationContext.SaveChangesAsync();
        }

        public async Task<List<ResourceViewModel>> GetResourcesAsync()
        {
            return await _applicationContext.Resources
                                     .ProjectTo<ResourceViewModel>(_mapper.ConfigurationProvider)
                                     .ToListAsync();
        }
    }
}
