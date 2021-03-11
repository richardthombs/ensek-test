using System;
using System.Collections.Generic;

namespace EnsekTest
{
	public interface IMeterReadingFileParser
	{
		List<SubmittedMeterReading> Parse(string csvContent);
	}
}
