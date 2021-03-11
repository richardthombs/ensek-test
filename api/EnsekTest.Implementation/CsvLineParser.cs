using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace EnsekTest.Implementation
{
	public class CsvLineParser : IMeterReadingLineParser
	{
		static Regex meterReadPattern = new Regex("^\\d{5,5}$");

		public MeterReading Parse(string line)
		{
			var fields = line.Split(",");
			if (fields.Length != 3) return null;

			var accountId = ParseAccountId(fields[0]);
			var dateTime = ParseReadingDate(fields[1]);
			var meterReading = ParseMeterReading(fields[2]);

			if (accountId == null || dateTime == null || meterReading == null) return null;

			return new MeterReading
			{
				AccountId = accountId.Value,
				MeterReadingDateTime = dateTime.Value,
				MeterReadValue = meterReading.Value,
			};
		}

		bool IsReadingValid(MeterReading reading)
		{
			if (reading.MeterReadValue < 0 || reading.MeterReadValue > 99999) return false;
			return true;
		}

		int? ParseAccountId(string str)
		{
			int accountId;
			if (Int32.TryParse(str, out accountId)) return accountId;
			return null;
		}

		DateTime? ParseReadingDate(string str)
		{
			try
			{
				return DateTime.ParseExact(str, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);
			}
			catch (FormatException)
			{
				return null;
			}
		}

		int? ParseMeterReading(string reading)
		{
			if (!meterReadPattern.IsMatch(reading)) return null;
			return Int32.Parse(reading);
		}
	}
}
