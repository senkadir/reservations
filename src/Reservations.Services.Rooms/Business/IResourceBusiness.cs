using Reservations.Services.Common.Types;
using Reservations.Services.Rooms.Commands;
using System.Threading.Tasks;

namespace Reservations.Services.Rooms.Business
{
    public interface IResourceBusiness : IBusinessBase
    {
        public Task CreateResourceAsync(CreateResourceCommand command);
    }
}
