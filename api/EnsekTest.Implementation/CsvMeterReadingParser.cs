using System;
using System.Collections.Generic;
using System.IO;
using System.Globalization;
using System.Text.RegularExpressions;

namespace EnsekTest.Implementation
{
	public class CsvMeterReadingParser : IMeterReadingParser
	{
		static Regex meterReadPattern = new Regex("^\\d{5,5}$");

		public List<SubmittedMeterReading> Parse(string csvContent)
		{
			var result = new List<SubmittedMeterReading>();

			using (var reader = new StringReader(csvContent))
			{
				int lineNumber = 0;
				for (string line = reader.ReadLine(); line != null; line = reader.ReadLine(), lineNumber++)
				{
					var reading = ParseLine(lineNumber, line);

					result.Add(reading);
				}
			}

			return result;
		}

		SubmittedMeterReading ParseLine(int lineNumber, string line)
		{
			var reading = new SubmittedMeterReading
			{
				LineNumber = lineNumber,
				Content = line
			};

			var fields = line.Split(",");
			if (fields.Length == 3)
			{
				var accountId = ParseAccountId(fields[0]);
				var dateTime = ParseReadingDate(fields[1]);
				var meterReading = ParseMeterReading(fields[2]);

				if (accountId != null && dateTime != null && meterReading != null)
				{
					reading.ParsedReading = new MeterReading
					{
						AccountId = accountId.Value,
						MeterReadingDateTime = dateTime.Value,
						MeterReadValue = meterReading.Value,
					};

					reading.Valid = true;
				}
			}

			return reading;
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
