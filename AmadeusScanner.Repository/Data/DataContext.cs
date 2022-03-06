using AmadeusScanner.Repository.Entities;
using Microsoft.EntityFrameworkCore;

namespace AmadeusScanner.Repository.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<AirportEntity> Airport { get; set; }
        public DbSet<AirportTypeEntity> AirportType { get; set; }
        public DbSet<CurrencyEntity> Currency { get; set; }
        public DbSet<FlightEntity> Flight { get; set; }
        public DbSet<FlightSearchEntity> FlightSearch { get; set; }
    }
}
