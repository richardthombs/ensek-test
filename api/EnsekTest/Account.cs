using System;
using System.Collections.Generic;

namespace EnsekTest
{
	public class Account
	{
		public int AccountId { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }

		public IEnumerable<MeterReading> MeterReadings { get; set; }
	}
}
