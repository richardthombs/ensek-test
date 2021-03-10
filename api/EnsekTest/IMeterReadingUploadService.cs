using System;
using System.Collections.Generic;

namespace EnsekTest
{
	public interface IMeterReadingUploadService
	{
		List<SubmittedMeterReading> Upload(List<SubmittedMeterReading> readings);
	}
}
