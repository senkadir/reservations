using AutoMapper;
using MassTransit;
using MassTransit.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Moq;
using Reservations.Services.Contracts.Events;
using Reservations.Services.Offices.Business;
using Reservations.Services.Offices.Commands;
using Reservations.Services.Offices.Data;
using Reservations.Services.Offices.Entities;
using Reservations.Services.Offices.MapperProfiles;
using Reservations.Services.Offices.Models;
using Reservations.Services.Tests.Common;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Reservations.Services.Offices.Tests.Business
{
    public class OfficeBusinessTests : IClassFixture<InMemoryDatabaseOptions<ApplicationContext>>
    {
        private readonly InMemoryDatabaseOptions<ApplicationContext> _inMemoryDatabaseOptions;
        private readonly IMapper _mapper;
        private readonly Mock<IBus> _busMock;
        private readonly IDistributedCache _cache;

        public OfficeBusinessTests(InMemoryDatabaseOptions<ApplicationContext> inMemoryDatabaseOptions)
        {
            _inMemoryDatabaseOptions = inMemoryDatabaseOptions;

            var config = new MapperConfiguration(c =>
            {
                c.AddProfile<OfficeProfile>();
            });

            config.CreateMapper();

            _mapper = new Mapper(config);

            _busMock = new Mock<IBus>();

            _cache = new MemoryDistributedCache(Options.Create(new MemoryDistributedCacheOptions()));
        }

        private ApplicationContext Context
        {
            get
            {
                return new ApplicationContext(_inMemoryDatabaseOptions.New().Options);
            }
        }

        [Fact]
        public async Task CreateAsync_Should_Throw_Exception_IfModelNull()
        {
            //Arrange
            IOfficeBusiness officeBusiness = new OfficeBusiness(Context,
                                                                _mapper,
                                                                _cache,
                                                                _busMock.Object);

            async Task func() => await officeBusiness.CreateAsync(null);

            //Act & Assert
            await Assert.ThrowsAsync<ArgumentNullException>(func);
        }

        [Fact]
        public async Task CreateAsync_Should_Create_Successfully()
        {
            //Arrange
            var context = Context;

            IOfficeBusiness officeBusiness = new OfficeBusiness(context,
                                                                _mapper,
                                                                _cache,
                                                                _busMock.Object);

            CreateOfficeCommand command = new CreateOfficeCommand
            {
                Location = "Amsterdam",
                OpenTime = DateTime.Now.TimeOfDay,
                CloseTime = DateTime.Now.AddHours(1).TimeOfDay
            };

            //Act
            await officeBusiness.CreateAsync(command);

            var addedOffice = await context.Offices
                                           .FirstOrDefaultAsync();

            //Assert
            Assert.NotNull(addedOffice);

            Assert.Equal("Amsterdam", addedOffice.Location);
        }

        [Fact]
        public async Task CreateAsync_Should_Publish_Event_Successfully()
        {
            //Arrange
            var context = Context;

            var harness = new InMemoryTestHarness();

            await harness.Start();

            IOfficeBusiness officeBusiness = new OfficeBusiness(context,
                                                                _mapper,
                                                                _cache,
                                                                harness.Bus);

            CreateOfficeCommand command = new CreateOfficeCommand
            {
                Location = "Amsterdam",
                OpenTime = DateTime.Now.TimeOfDay,
                CloseTime = DateTime.Now.AddHours(1).TimeOfDay
            };

            //Act
            await officeBusiness.CreateAsync(command);

            //Assert
            try
            {
                Assert.True(await harness.Published.Any<OfficeCreated>());
            }
            finally
            {
                await harness.Stop();
            }
        }

        [Fact]
        public async Task GetAsync_Should_Get_Data_From_Database_For_First_Time_Sucessfully()
        {
            //Arrange
            var context = Context;

            IOfficeBusiness officeBusiness = new OfficeBusiness(context,
                                                                _mapper,
                                                                _cache,
                                                                _busMock.Object);

            await context.Offices.AddAsync(new Office
            {
                Id = Guid.NewGuid(),
                OpenTime = DateTime.Now.TimeOfDay,
                CloseTime = DateTime.Now.AddHours(1).TimeOfDay,
                Location = "Amsterdam"
            });

            await context.SaveChangesAsync();

            //Act
            var offices = await officeBusiness.GetAsync();

            //Assert
            Assert.NotNull(offices);

            Assert.IsType<List<OfficeViewModel>>(offices);

            Assert.Single(offices);
        }

        /// <summary>
        /// Test steps:
        /// Add office to database.
        /// Call method for the first time to fill cache.
        /// Remove office object from database to be sure data come from cache
        /// Call method to get data from cache
        /// Assert data
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task GetAsync_Should_Get_Data_From_Cache_Successfully()
        {
            //Arrange
            var context = Context;

            IOfficeBusiness officeBusiness = new OfficeBusiness(context,
                                                                _mapper,
                                                                _cache,
                                                                _busMock.Object);

            Office office = new Office
            {
                Id = Guid.NewGuid(),
                OpenTime = DateTime.Now.TimeOfDay,
                CloseTime = DateTime.Now.AddHours(1).TimeOfDay,
                Location = "Amsterdam"
            };

            await context.Offices.AddAsync(office);

            await context.SaveChangesAsync();

            //Act
            await officeBusiness.GetAsync();

            context.Offices.Remove(office);

            await context.SaveChangesAsync();

            var officesFromCache = await officeBusiness.GetAsync();

            //Assert
            Assert.NotNull(officesFromCache);

            Assert.Single(officesFromCache);
        }

        [Fact]
        public async Task ExistsAsync_Should_Return_True_If_Exists_In_Database()
        {
            //Arrange
            var context = Context;

            IOfficeBusiness officeBusiness = new OfficeBusiness(context,
                                                                _mapper,
                                                                _cache,
                                                                _busMock.Object);

            Office office = new Office
            {
                Id = Guid.NewGuid(),
                OpenTime = DateTime.Now.TimeOfDay,
                CloseTime = DateTime.Now.AddHours(1).TimeOfDay,
                Location = "Amsterdam"
            };

            await context.Offices.AddAsync(office);

            await context.SaveChangesAsync();

            //Act
            var exists = await officeBusiness.ExistsAsync(office.Id);

            //Assert
            Assert.True(exists);
        }

        [Fact]
        public async Task ExistsAsync_Should_Return_False_If_Not_Exists_In_Database()
        {
            //Arrange
            var context = Context;

            IOfficeBusiness officeBusiness = new OfficeBusiness(context,
                                                                _mapper,
                                                                _cache,
                                                                _busMock.Object);

            //Act
            var exists = await officeBusiness.ExistsAsync(Guid.NewGuid());

            //Assert
            Assert.False(exists);
        }

        [Fact]
        public async Task AvailableOfficesAsync_Should_Throw_Exception_IfModelNull()
        {
            //Arrange
            IOfficeBusiness officeBusiness = new OfficeBusiness(Context,
                                                                _mapper,
                                                                _cache,
                                                                _busMock.Object);

            async Task func() => await officeBusiness.CreateAsync(null);

            //Act & Assert
            await Assert.ThrowsAsync<ArgumentNullException>(func);
        }

        [Fact]
        public async Task AvailableOfficesAsync_Should_Return_Available_Office_By_Current_User_Is_Location()
        {
            //Arrange
            var context = Context;

            IOfficeBusiness officeBusiness = new OfficeBusiness(context,
                                                                _mapper,
                                                                _cache,
                                                                _busMock.Object);

            Office amsterdamOffice = new Office
            {
                Id = Guid.NewGuid(),
                OpenTime = new TimeSpan(8, 0, 0),
                CloseTime = new TimeSpan(17, 30, 0),
                Location = "Amsterdam"
            };

            Office berlinOffice = new Office
            {
                Id = Guid.NewGuid(),
                OpenTime = new TimeSpan(8, 0, 0),
                CloseTime = new TimeSpan(20, 0, 0),
                Location = "Berlin"
            };

            await context.Offices.AddAsync(amsterdamOffice);
            await context.Offices.AddAsync(berlinOffice);

            await context.SaveChangesAsync();

            CheckOfficeAvailailityCommand commandShouldCoverBoth = new CheckOfficeAvailailityCommand
            {
                //Should cover both offices
                Location = "Amsterdam",
                StartTime = new TimeSpan(8, 0, 0),
                EndTime = new TimeSpan(16, 0, 0)
            };

            //Act

            var amsterdamOfficeId = await officeBusiness.AvailableOfficesAsync(commandShouldCoverBoth);

            //Assert
            Assert.Equal(amsterdamOffice.Id, amsterdamOfficeId);

        }
    }
}
