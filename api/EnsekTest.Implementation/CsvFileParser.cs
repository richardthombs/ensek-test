using System.Collections.Generic;
using System.IO;

namespace EnsekTest.Implementation
{
	public class CsvFileParser : IMeterReadingFileParser
	{
		IMeterReadingLineParser lineParser;

		public CsvFileParser(IMeterReadingLineParser lineParser)
		{
			this.lineParser = lineParser;
		}

		public List<SubmittedMeterReading> Parse(string csvContent)
		{
			var result = new List<SubmittedMeterReading>();

			using (var reader = new StringReader(csvContent))
			{
				int lineNumber = 1;
				for (string line = reader.ReadLine(); line != null; line = reader.ReadLine(), lineNumber++)
				{
					// Skip header, if present
					if (lineNumber == 1 && line == "AccountId,MeterReadingDateTime,MeterReadValue") continue;

					var parsed = lineParser.Parse(line);

					result.Add(new SubmittedMeterReading
					{
						LineNumber = lineNumber,
						Content = line,
						ParsedReading = parsed,
						Valid = parsed != null
					});
				}
			}

			return result;
		}
	}
}
