﻿using System;

namespace Reservations.Services.Rooms.Entities
{
    public class Resource
    {
        public Guid Id { get; set; }

        public bool Specific { get; set; }

        public int TotalQuantity { get; set; }

        public string Name { get; set; }

        public bool Portable { get; set; }
    }
}
