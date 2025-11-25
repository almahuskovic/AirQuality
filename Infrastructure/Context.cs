using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Models.Entities;
using System;
using System.Runtime.CompilerServices;

namespace Infrastructure
{
    public class Context : IdentityDbContext<ApplicationUser>
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {
        }
        public DbSet<City> Cities { get; set; }
        public DbSet<AirQualityMeasurement> AirQualityMeasurements { get; set; }
        public DbSet<FavouriteCities> UserFavouriteCities { get; set; }
    }
}
