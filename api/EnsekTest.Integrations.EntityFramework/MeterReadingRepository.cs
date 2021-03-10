using System;
using System.Collections.Generic;
using System.Linq;

namespace EnsekTest.Integrations.EntityFramework
{
	public class MeterReadingRepository : IMeterReadingsRepository
	{
		MeterReadingContext db;

		public MeterReadingRepository(MeterReadingContext db)
		{
			this.db = db;
		}

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
				x.MeterReadValue == reading.MeterReadValue
			);

			return (existing != null);
		}
	}
}
