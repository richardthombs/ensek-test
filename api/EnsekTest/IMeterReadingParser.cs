namespace EnsekTest
{
	public interface IMeterReadingParser
	{
		ParsedMeterReadings Parse(string csvContent);
	}
}
