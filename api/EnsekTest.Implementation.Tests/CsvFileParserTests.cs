
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
}
