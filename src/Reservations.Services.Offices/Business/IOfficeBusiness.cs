using Reservations.Services.Common.Types;
using Reservations.Services.Offices.Commands;
using Reservations.Services.Offices.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Reservations.Services.Offices.Business
{
    public interface IOfficeBusiness : IBusinessBase
    {
        public Task CreateAsync(CreateOfficeCommand command);

        public Task<List<OfficeViewModel>> GetAsync();

        public Task<bool> ExistsAsync(Guid officeId);
    }
}
