using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Service.Models;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Service.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<VehicleMake> Manufacturers { get; set; }
        public DbSet<VehicleModel> VehicleModels { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<VehicleMake>()
                        .HasMany<VehicleModel>(d => d.VehicleModels)
                        .WithOne(e => e.Manufacturer)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
