using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using static Server.modules.Classes;
using Microsoft.Extensions.Logging.Console;

namespace Server.modules
{
    class UserContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<DataPoint> DataPoints { get; set; }

        public static readonly ILoggerFactory loggerFactory = new LoggerFactory(new[] {
              new ConsoleLoggerProvider((_, __) => true, true)
        });

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Override OnModelCreating to fetch our data from db.

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.ID);
                entity.HasMany(e => e.DataPoints);
                entity.Property(e => e.Name).IsRequired();
            });
            modelBuilder.Entity<DataPoint>(entity =>
            {
                entity.HasKey(e => e.ID);
                entity.Property(e => e.UserID).IsRequired();
                entity.Property(e => e.Time).IsRequired();
                entity.Property(e => e.Value).IsRequired();
            });
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
    //        optionsBuilder.UseLoggerFactory(loggerFactory)  //tie-up DbContext with LoggerFactory object
    //.EnableSensitiveDataLogging();

            Config c = new Config();
            string fp = "..\\..\\..\\..\\.\\config.json";
            optionsBuilder.UseMySQL(c.Read(fp));
        }
    }
}