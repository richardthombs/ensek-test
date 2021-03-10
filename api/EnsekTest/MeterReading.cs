using System;

namespace EnsekTest
{
	public class MeterReading
	{
		public int AccountId { get; set; }
		public Account Account { get; set; }
		public DateTimeOffset MeterReadingDateTime { get; set; }
		public int MeterReadValue { get; set; }
	}
}
