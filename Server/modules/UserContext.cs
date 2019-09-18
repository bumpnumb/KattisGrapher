using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using static Server.modules.Classes;

namespace Server.modules
{
    class UserContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<DataPoint> DataPoints { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Override OnModelCreating to fetch our data from db.

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.ID);
                entity.Property(e => e.Name).IsRequired();
                entity.HasMany(e => e.DataPoints);
            });

            modelBuilder.Entity<DataPoint>(entity =>
            {
                entity.HasKey(e => e.UserID);
                entity.Property(e => e.Time).IsRequired();
                entity.Property(e => e.Value).IsRequired();
            });
        }
    }
}