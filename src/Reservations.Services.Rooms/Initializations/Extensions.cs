using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Reservations.Services.Rooms.Data;
using Reservations.Services.Rooms.Entities;
using System;
using System.Collections.Generic;

namespace Reservations.Services.Rooms.Initializations
{
    public static class Extensions
    {
        public static void InitializeService(this IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.CreateScope();

            var context = serviceScope.ServiceProvider.GetService<ApplicationContext>();

            context.Database.Migrate();

            context.Rooms.AddRange(Rooms());

            context.Resources.AddRange(Resources());

            context.RoomResources.AddRange(RoomResources());

            context.SaveChanges();
        }

        public static List<RoomResource> RoomResources()
        {
            Guid room1Id = Guid.Parse("c678fddb-f055-6792-2c44-9ea76263576a");
            Guid room2Id = Guid.Parse("d31a4cc3-ff3c-977f-c6ea-1406b602da21");

            Guid resource1Id = Guid.Parse("d807ce74-6f5a-4ed9-a084-ba9f20748b03");
            Guid resource2Id = Guid.Parse("e3c1c219-3480-4cd3-bf66-99f04cb1bb15");
            Guid resource3Id = Guid.Parse("7ccfb32b-e133-404d-a871-679a45ff5a0d");

            #region Room 1 
            RoomResource roomResource1 = new RoomResource
            {
                Id = Guid.NewGuid(),
                RoomId = room1Id,
                ResourceId = resource1Id,
                Quantity = 1
            };

            RoomResource roomResource2 = new RoomResource
            {
                Id = Guid.NewGuid(),
                RoomId = room1Id,
                ResourceId = resource3Id,
                Quantity = 1
            };
            #endregion

            #region Room 2 
            RoomResource roomResource3 = new RoomResource
            {
                Id = Guid.NewGuid(),
                RoomId = room2Id,
                ResourceId = resource2Id,
                Quantity = 1
            };

            RoomResource roomResource4 = new RoomResource
            {
                Id = Guid.NewGuid(),
                RoomId = room2Id,
                ResourceId = resource3Id,
                Quantity = 1
            };
            #endregion

            return new List<RoomResource>
            {
                roomResource1,
                roomResource2,
                roomResource3,
                roomResource4,
            };
        }

        public static List<Resource> Resources()
        {
            Guid resource1Id = Guid.Parse("d807ce74-6f5a-4ed9-a084-ba9f20748b03");
            Guid resource2Id = Guid.Parse("e3c1c219-3480-4cd3-bf66-99f04cb1bb15");
            Guid resource3Id = Guid.Parse("7ccfb32b-e133-404d-a871-679a45ff5a0d");
            Guid resource4Id = Guid.Parse("3417204a-3026-443b-bddf-3fc57e1652fc");
            Guid resource5Id = Guid.Parse("ac979209-3048-475b-9a36-28a6445da9f2");

            Resource resource1 = new Resource
            {
                Id = resource1Id,
                Name = "Beamer",
                Portable = false,
                Specific = true,
                TotalQuantity = 5
            };

            Resource resource2 = new Resource
            {
                Id = resource2Id,
                Name = "Television on a mount",
                Portable = false,
                Specific = true,
                TotalQuantity = 5
            };

            Resource resource3 = new Resource
            {
                Id = resource3Id,
                Name = "Portable White Board",
                Portable = true,
                Specific = false,
                TotalQuantity = 5
            };

            Resource resource4 = new Resource
            {
                Id = resource4Id,
                Name = "Marker Pen Blue",
                Portable = true,
                Specific = false,
                TotalQuantity = 5
            };

            Resource resource5 = new Resource
            {
                Id = resource5Id,
                Name = "Marker Pen Red",
                Portable = true,
                Specific = false,
                TotalQuantity = 5
            };

            return new List<Resource>
            {
                resource1,
                resource2,
                resource3,
                resource4,
                resource5
            };
        }

        public static List<Room> Rooms()
        {
            Guid amsterdamId = Guid.Parse("6a43eeb5-8984-8bc4-ffe4-3655baa88bcf");
            Guid berlinId = Guid.Parse("1401d8a9-9bec-ca60-d113-66159260ab40");

            Guid room1Id = Guid.Parse("c678fddb-f055-6792-2c44-9ea76263576a");
            Guid room2Id = Guid.Parse("d31a4cc3-ff3c-977f-c6ea-1406b602da21");
            Guid room3Id = Guid.Parse("a0063e88-09d7-722c-ac4c-f5c366c47e98");
            Guid room4Id = Guid.Parse("ae19f403-7fe0-8817-9693-22b0d8660583");
            Guid room5Id = Guid.Parse("6e9d90aa-5f99-a755-33f7-8e4e11b8c6cf");

            Room room1 = new Room
            {
                Id = room1Id,
                Capacity = 10,
                ChairCount = 10,
                Name = "Amsterdam Room 1",
                OfficeId = amsterdamId
            };

            Room room2 = new Room
            {
                Id = room2Id,
                Capacity = 10,
                ChairCount = 5,
                Name = "Amsterdam Room 2",
                OfficeId = amsterdamId
            };

            Room room3 = new Room
            {
                Id = room3Id,
                Capacity = 10,
                ChairCount = 0,
                Name = "Amsterdam Room 3",
                OfficeId = amsterdamId
            };

            Room room4 = new Room
            {
                Id = room4Id,
                Capacity = 10,
                ChairCount = 0,
                Name = "Berlin Room 1",
                OfficeId = berlinId
            };

            Room room5 = new Room
            {
                Id = room5Id,
                Capacity = 10,
                ChairCount = 5,
                Name = "Amsterdam Room 2",
                OfficeId = amsterdamId
            };

            return new List<Room>
            {
                room1,
                room2,
                room3,
                room4,
                room5,
            };
        }
    }
}
