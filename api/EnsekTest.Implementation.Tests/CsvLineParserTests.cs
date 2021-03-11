using System;
using System.Linq;

using NUnit.Framework;

namespace EnsekTest.Implementation.Tests
{
	public class CsvFileParserTests
	{
		[Test]
		public void File_Is_Parsed_Correctly()
		{
			// Arrange
			var file = "2344,22/04/2019 09:24,01002\n2349,22/04/2019 12:25,VOID";
			var parser = new CsvFileParser(new CsvLineParser());

			// Act
			var parsed = parser.Parse(file);

			// Assert
			Assert.AreEqual(2, parsed.Count);
			Assert.AreEqual(true, parsed[0].Valid);
			Assert.IsNotNull(parsed[0].ParsedReading);
			Assert.AreEqual(1, parsed[0].LineNumber);

			Assert.AreEqual(false, parsed[1].Valid);
			Assert.IsNull(parsed[1].ParsedReading);
			Assert.AreEqual(2, parsed[1].LineNumber);
		}
	}

	public class CsvLineParserTests
	{
		[Test]
		public void Line_Is_Parsed_Correctly()
		{
			var line = "2344,22/04/2019 09:24,01002";

			var parser = new CsvLineParser();
			var parsed = parser.Parse(line);

			Assert.AreEqual(2344, parsed.AccountId);
			Assert.AreEqual(new DateTime(2019, 4, 22, 09, 24, 00), parsed.MeterReadingDateTime.DateTime);
			Assert.AreEqual(1002, parsed.MeterReadValue);
		}

		[TestCase("2344,22/04/2019 09:24,01002")]
		[TestCase("2233,22/04/2019 12:25,00323")]
		[TestCase("8766,22/04/2019 12:25,03440")]
		[TestCase("2344,22/04/2019 12:25,01002")]
		[TestCase("2345,22/04/2019 12:25,45522")]
		[TestCase("2347,22/04/2019 12:25,00054")]
		[TestCase("2348,22/04/2019 12:25,00123")]
		[TestCase("2350,22/04/2019 12:25,05684")]
		[TestCase("2351,22/04/2019 12:25,57579")]
		[TestCase("2352,22/04/2019 12:25,00455")]
		[TestCase("2353,22/04/2019 12:25,01212")]
		[TestCase("2354,22/04/2019 12:25,00889")]
		[TestCase("2355,06/05/2019 09:24,00001")]
		[TestCase("2356,07/05/2019 09:24,00000")]
		[TestCase("6776,10/05/2019 09:24,23566")]
		[TestCase("1234,12/05/2019 09:24,09787")]
		[TestCase("1236,10/04/2019 19:34,08898")]
		[TestCase("1237,15/05/2019 09:24,03455")]
		[TestCase("1238,16/05/2019 09:24,00000")]
		[TestCase("1239,17/05/2019 09:24,45345")]
		[TestCase("1240,18/05/2019 09:24,00978")]
		[TestCase("1242,20/05/2019 09:24,00124")]
		[TestCase("1243,21/05/2019 09:24,00077")]
		[TestCase("1244,25/05/2019 09:24,03478")]
		[TestCase("1245,25/05/2019 14:26,00676")]
		[TestCase("1246,25/05/2019 09:24,03455")]
		[TestCase("1247,25/05/2019 09:24,00003")]
		[TestCase("1248,26/05/2019 09:24,03467")]
		public void Valid_Lines_Are_Accepted(string line)
		{
			// Arrange
			var parser = new CsvLineParser();

			// Act
			var result = parser.Parse(line);

			// Assert
			Assert.IsNotNull(result);
		}

		[TestCase("2346,22/04/2019 12:25,999999")]
		[TestCase("2349,22/04/2019 12:25,VOID")]
		[TestCase("2344,08/05/2019 09:24,0X765")]
		[TestCase("6776,09/05/2019 09:24,-06575")]
		[TestCase("4534,11/05/2019 09:24,")]
		[TestCase("1235,13/05/2019 09:24,")]
		[TestCase("1241,11/04/2019 09:24,00436,X")]
		[TestCase("9999,11/31/2019 09:24,999")]

		public void Invalid_Lines_Are_Rejected(string line)
		{
			// Arrange
			var parser = new CsvLineParser();

			// Act
			var result = parser.Parse(line);

			// Assert
			Assert.IsNull(result);
		}
	}
}
