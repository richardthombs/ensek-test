namespace EnsekTest
{
	public interface IMeterReadingsRepository
	{
		bool Exists(MeterReading reading);
		MeterReading Create(MeterReading reading);
	}
}
