using System;
using System.Collections.Generic;

namespace EnsekTest
{
	public interface IMeterReadingParser
	{
		List<SubmittedMeterReading> Parse(string csvContent);
	}
}
