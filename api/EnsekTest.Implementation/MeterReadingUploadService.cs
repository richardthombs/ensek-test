using System;
using System.Collections.Generic;
using System.Linq;

namespace EnsekTest.Implementation
{
	public class MeterReadingUploadService : IMeterReadingUploadService
	{
		IMeterReadingsRepository readingsRepo;

		public MeterReadingUploadService(IMeterReadingsRepository readingsRepo)
		{
			this.readingsRepo = readingsRepo;
		}

		public List<SubmittedMeterReading> Upload(List<SubmittedMeterReading> readings)
		{
			foreach (var reading in readings)
			{
				if (!reading.Valid) continue;

				if (readingsRepo.Exists(reading.ParsedReading))
				{
					reading.Valid = false;
					continue;
				}

				try
				{
					readingsRepo.Create(reading.ParsedReading);
					reading.Uploaded = true;
				}
				catch
				{
					reading.Uploaded = false;
				}
			}

			return readings;
		}
	}

}
