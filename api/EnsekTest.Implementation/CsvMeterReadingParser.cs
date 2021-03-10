using System;
using System.Collections.Generic;
using System.IO;
using System.Globalization;

namespace EnsekTest.Implementation
{
	public class CsvMeterReadingParser : IMeterReadingParser
	{
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
				try
				{
					var accountId = Int32.Parse(fields[0], CultureInfo.InvariantCulture);
					var dateTime = DateTime.ParseExact(fields[1], "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);
					var meterReading = Int32.Parse(fields[2], CultureInfo.InvariantCulture);

					reading.ParsedReading = new MeterReading
					{
						AccountId = accountId,
						MeterReadingDateTime = dateTime,
						MeterReadValue = meterReading
					};

					reading.Valid = IsReadingValid(reading.ParsedReading);
				}
				catch (FormatException)
				{
					reading.Valid = false;
				}
			}

			return reading;
		}

		bool IsReadingValid(MeterReading reading)
		{
			if (reading.MeterReadValue < 0 || reading.MeterReadValue > 99999) return false;
			return true;
		}
	}
}
