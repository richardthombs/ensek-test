using System;
using System.Collections.Generic;
using System.Linq;

namespace EnsekTest.Implementation
{
	public class MeterReadingUploadService : IMeterReadingUploadService
	{
		IMeterReadingsRepository readingsRepo;
		IAccountsRepository accountsRepo;

		public MeterReadingUploadService(IMeterReadingsRepository readingsRepo, IAccountsRepository accountsRepo)
		{
			this.readingsRepo = readingsRepo;
			this.accountsRepo = accountsRepo;
		}

		public List<SubmittedMeterReading> Upload(List<SubmittedMeterReading> readings)
		{
			foreach (var reading in readings)
			{
				if (!reading.Valid) continue;

				if (!accountsRepo.Exists(reading.ParsedReading.AccountId))
				{
					reading.Valid = false;
					continue;
				}

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
