﻿// <auto-generated />
using Game.Gambling.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Game.Gambling.Infrastructure.Migrations
{
    [DbContext(typeof(GamblingDBConext))]
    partial class GamblingDBConextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.13");

            modelBuilder.Entity("Game.Gambling.Domain.Entities.UserGamblingDetail", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<long>("AccountBalance")
                        .HasColumnType("INTEGER");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("UserGamblingDetails", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            AccountBalance = 10000L,
                            UserId = "2c4cf163-3f29-4eb5-a572-06e6f4da79dc"
                        },
                        new
                        {
                            Id = 2L,
                            AccountBalance = 100L,
                            UserId = "8389d604-8191-4a30-af54-6f17fca78b4a"
                        },
                        new
                        {
                            Id = 3L,
                            AccountBalance = 0L,
                            UserId = "7ed87f88-a134-4659-be6f-edabbaddb06a"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
