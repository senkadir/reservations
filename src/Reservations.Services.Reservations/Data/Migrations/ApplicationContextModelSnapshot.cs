﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using NpgsqlTypes;
using Reservations.Services.Reservations.Data;

namespace Reservations.Services.Reservations.Data.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    partial class ApplicationContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("reservations")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.1.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("Reservations.Services.Reservations.Entities.Reservation", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("uuid");

                    b.Property<NpgsqlRange<DateTime>>("Duration")
                        .HasColumnName("duration")
                        .HasColumnType("tsrange");

                    b.Property<DateTime>("EndTime")
                        .HasColumnName("end_time")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("PersonCount")
                        .HasColumnName("person_count")
                        .HasColumnType("integer");

                    b.Property<Guid>("RoomId")
                        .HasColumnName("room_id")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("StartTime")
                        .HasColumnName("start_time")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id")
                        .HasName("pk_reservations");

                    b.HasIndex("Duration")
                        .HasName("ix_reservations_duration");

                    b.ToTable("reservations");
                });
#pragma warning restore 612, 618
        }
    }
}
