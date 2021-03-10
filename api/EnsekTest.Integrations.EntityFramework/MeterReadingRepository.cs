using System;
using System.Collections.Generic;
using System.Linq;

namespace EnsekTest.Integrations.EntityFramework
{
	public class MeterReadingRepository : IMeterReadingsRepository
	{
		MeterReadingContext db;

		public MeterReading Create(MeterReading reading)
		{
			db.MeterReadings.Add(reading);
			return reading;
		}

		public bool Exists(MeterReading reading)
		{
			var existing = db.MeterReadings.FirstOrDefault(x =>
				x.AccountId == reading.AccountId &&
				x.MeterReadingDateTime == reading.MeterReadingDateTime &&
				x.MeterReadValue == x.MeterReadValue
			);

			return (existing != null);
		}
	}
}
