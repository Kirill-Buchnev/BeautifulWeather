using BeautifulWeather.Models;
using BeautifulWeather.Services.GeoCoder;
using BeautifulWeather.Services.Settings;
using Microsoft.EntityFrameworkCore;
using System;

namespace BeautifulWeather.Storages
{
    public class DatabaseContext : DbContext
    {
        public DbSet<GeoLocation> GeoLocations { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Settings> Settings { get; set; }
        public DbSet<WeatherForecast> WeatherForecasts { get; set; }
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
