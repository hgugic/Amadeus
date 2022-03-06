using AmadeusScanner.Repository.Entities;
using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AmadeusScanner.Repository.Data
{
    public class Seed
    {
        public static async Task SeedData(DataContext context)
        {
            if (context.AirportType.Any()) return;

            await context.AirportType.AddRangeAsync(AirportTypeLookup.AirportTypes);
            await context.SaveChangesAsync();

            if (context.Currency.Any()) return;
            await context.Currency.AddRangeAsync(CurrencyLookup.Currencies);
            await context.SaveChangesAsync();

            if (context.Airport.Any()) return;

            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                Delimiter = ","
            };

            using var reader = new StreamReader("..\\AmadeusScanner.Repository\\Data\\Seed\\airports.csv");
            using var csv = new CsvReader(reader, config);
            csv.Context.RegisterClassMap<AirportEntityMap>();
            var airports = csv.GetRecords<AirportEntity>();
            await context.Airport.AddRangeAsync(airports);
            await context.SaveChangesAsync();


        }
    }
}
