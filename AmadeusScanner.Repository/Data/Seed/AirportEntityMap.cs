using AmadeusScanner.Repository.Entities;
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using System;
using System.Linq;

namespace AmadeusScanner.Repository.Data
{
    public class AirportEntityMap : ClassMap<AirportEntity>
    {
        public AirportEntityMap()
        {
            Map(m => m.Id).Name("id").TypeConverter<GuidConverter<Guid>>();
            Map(m => m.DateCreated).Constant(DateTime.UtcNow);
            Map(m => m.DateUpdated).Constant(DateTime.UtcNow);
            Map(m => m.AirportTypeId).Name("type").TypeConverter<AirportTypeConverter<Guid>>();
            Map(m => m.Name).Name("name");
            Map(m => m.IataCode).Name("iata_code");
        }
    }
    public class AirportTypeConverter<T> : DefaultTypeConverter
    {
        public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
        {
            var airportTypeId = AirportTypeLookup.AirportTypes.FirstOrDefault(y => y.Name.ToLower() == text.ToString().Replace("_", " ").ToLower())?.Id;

            return airportTypeId ??= AirportTypeLookup.AirportTypes.First(x => x.Abrv == "Other").Id;
        }
    }

    public class GuidConverter<T> : DefaultTypeConverter
    {
        public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
        {
            return Guid.NewGuid();
        }
    }
}
