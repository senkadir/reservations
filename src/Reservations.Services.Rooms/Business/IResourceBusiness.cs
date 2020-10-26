using Reservations.Services.Common.Types;
using Reservations.Services.Rooms.Commands;
using Reservations.Services.Rooms.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Reservations.Services.Rooms.Business
{
    public interface IResourceBusiness : IBusinessBase
    {
        public Task CreateResourceAsync(CreateResourceCommand command);

        public Task<List<ResourceViewModel>> GetResourcesAsync();
    }
}
