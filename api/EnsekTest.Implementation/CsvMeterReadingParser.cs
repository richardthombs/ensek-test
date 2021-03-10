using System;
using System.IO;
using System.Globalization;

namespace EnsekTest.Implementation
{
	public class CsvMeterReadingParser : IMeterReadingParser
	{
		public ParsedMeterReadings Parse(string csvContent)
		{
			var result = new ParsedMeterReadings();

			using (var reader = new StringReader(csvContent))
			{
				int lineNumber = 0;
				for (string line = reader.ReadLine(); line != null; line = reader.ReadLine(), lineNumber++)
				{
					var reading = ParseLine(line);
					if (reading != null) result.Valid.Add(reading);
					else result.Invalid.Add(new InvalidMeterReading { LineNumber = lineNumber, Content = line });
				}

				return result;
			}
		}

		MeterReading ParseLine(string line)
		{
			var fields = line.Split(",");
			if (fields.Length != 3) return null;

			try
			{
				var accountId = Int32.Parse(fields[0], CultureInfo.InvariantCulture);
				var dateTime = DateTime.ParseExact(fields[1], "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);
				var reading = Int32.Parse(fields[2], CultureInfo.InvariantCulture);

				if (reading < 0 || reading > 99999) return null;

				return new MeterReading
				{
					AccountId = accountId,
					MeterReadingDateTime = dateTime,
					MeterReadValue = reading
				};
			}
			catch (FormatException)
			{
				return null;
			}
		}
	}

	public class MeterReadingUploadService : IMeterReadingUploadService
	{
		public ParsedMeterReadings Upload(ParsedMeterReadings readings)
		{
			throw new NotImplementedException();
		}
	}
}
