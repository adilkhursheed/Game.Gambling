using Game.Gambling.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Gambling.Infrastructure.Context
{
    /// <summary>
    /// EF DB context for Gambling DB
    /// To keep this app simple its using same DB for Authentication
    /// Ideally its best to have different DB for each microservice(ie. Auth and Bet API)
    /// </summary>
    public class GamblingDBConext : DbContext
    {
        #region Constructor

        public GamblingDBConext() : base()
        {
        }
        public GamblingDBConext(DbContextOptions<GamblingDBConext> options) : base(options)
        {
        }
        #endregion

        #region DbSets/Tables
        public DbSet<UserGamblingDetail> UserGamblingDetails { get; set; }
        #endregion
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite("Data Source=../Database/gambling.db");
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<UserGamblingDetail>().ToTable("UserGamblingDetails");
            // Seed data for the gambling account balance
            modelBuilder.Entity<UserGamblingDetail>().HasData(
                new UserGamblingDetail { Id = 1, UserId= "2c4cf163-3f29-4eb5-a572-06e6f4da79dc", AccountBalance = 10000 },
                new UserGamblingDetail { Id = 2, UserId= "8389d604-8191-4a30-af54-6f17fca78b4a", AccountBalance = 100 },
                new UserGamblingDetail { Id = 3, UserId = "7ed87f88-a134-4659-be6f-edabbaddb06a", AccountBalance = 0 }
            );
        }
    }
}
