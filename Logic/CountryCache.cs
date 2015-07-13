using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using CsvHelper;
using SpartanExtensions;
using SpartanExtensions.DataToCSV;

namespace IPToLocation
{
	public static class CountryCache
	{
		public static List<IPRangeCountry> Cache;

		public static void Load()
		{
			var csvDatabasePath = string.Format("{0}ipCountryRanges.csv",
				typeof(CountryCache).GetAssemblyOutputPath());

			var csvDatabase = new FileInfo(csvDatabasePath);
			if (!csvDatabase.Exists)
			{
				Cache = IPToLocationHandler.Download();
				SaveCountryRanges(csvDatabasePath, Cache);
			}
			else
			{
				LoadCountryRanges(csvDatabasePath);
			}
		}

		public static IpAddressLocation GetIpAddressLocation(IPAddress ipAddress)
		{
			var ipAddressIntegerRepresentation = ipAddress.ToInteger();

			InitCache();

			var correspondingRange = Cache.FirstOrDefault(r => r.StartIP <= ipAddressIntegerRepresentation
															   && r.EndIP >= ipAddressIntegerRepresentation);
			return correspondingRange != null ?
				new IpAddressLocation(correspondingRange.ISO_Code_2)
				: new IpAddressLocation();
		}

		private static void InitCache()
		{
			if (Cache == null)
				Load();
		}

		private static void LoadCountryRanges(string csvDatabasePath)
		{
			var csv = new CsvReader(File.OpenText(csvDatabasePath));
			Cache = csv.GetRecords<IPRangeCountry>().ToList();
		}

		private static void SaveCountryRanges(string csvDatabasePath, IEnumerable<IPRangeCountry> ranges)
		{
			var csv = new DataToCsv<IPRangeCountry>(ranges, new List<HeaderBase<IPRangeCountry>>
            {
                new CustomHeader<IPRangeCountry>("StartIP", item => item.StartIP),
                new CustomHeader<IPRangeCountry>("EndIP", item => item.EndIP),
                new CustomHeader<IPRangeCountry>("ISO_Code_2", item => item.ISO_Code_2)
            }, offsetValue: ",", useOffsetBeforeNewLine: false).Csv;
			var fileStream = new FileStream(csvDatabasePath, FileMode.CreateNew);
			csv.WriteTo(fileStream);
			fileStream.Close();
			csv.Close();

			//Using CsvWriter causes a strange behaviour of adding a random integer ("3" or "37") at the end of the csv values, thus making the file not usable for loading of the cache.
			//Updating the CsvHelper didn't help either.
			//var csv = new CsvWriter(File.CreateText(csvDatabasePath));
			//csv.WriteRecords(ranges);
		}
	}
}