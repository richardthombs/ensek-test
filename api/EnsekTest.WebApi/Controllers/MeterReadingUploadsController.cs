using System;
using System.Collections.Generic;
using System.IO;
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
		public async Task<IActionResult> Post()
		{
			using (var reader = new StreamReader(Request.Body))
			{
				var body = await reader.ReadToEndAsync();
				var readings = parser.Parse(body);
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
}
