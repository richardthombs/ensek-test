using System.Collections.Generic;

namespace EnsekTest
{
	public class ParsedMeterReadings
	{
		public ParsedMeterReadings()
		{
			Valid = new List<MeterReading>();
			Invalid = new List<InvalidMeterReading>();
		}

		public List<MeterReading> Valid { get; set; }
		public List<InvalidMeterReading> Invalid { get; set; }
	}
}
