using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace EnsekTest.WebApi.Controllers
{
	[ApiController]
	[Route("meter-reading-uploads")]
	public class MeterReadingUploadsController : ControllerBase
	{
		IMeterReadingParser parser;
		IMeterReadingUploadService uploadSvc;

		public MeterReadingUploadsController(IMeterReadingParser parser, IMeterReadingUploadService uploadSvc)
		{
			this.parser = parser;
			this.uploadSvc = uploadSvc;
		}

		[HttpPost]
		public IActionResult Post([FromBody] string csv)
		{
			var readings = parser.Parse(csv);
			readings = uploadSvc.Upload(readings);

			return Ok(new
			{
				validCount = readings.Count(x => x.Uploaded),
				invalidCount = readings.Count(x => !x.Uploaded),
				results = readings
			});
		}
	}
}
