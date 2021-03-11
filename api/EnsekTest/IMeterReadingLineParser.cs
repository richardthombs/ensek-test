namespace EnsekTest
{
	public interface IMeterReadingLineParser
	{
		MeterReading Parse(string csvLine);
	}
}
