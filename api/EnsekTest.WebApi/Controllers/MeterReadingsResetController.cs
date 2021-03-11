using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using EnsekTest.Integrations.EntityFramework;

namespace EnsekTest.WebApi.Controllers
{
	[ApiController]
	[Route("api/reset")]
	public class MeterReadingsResetController : ControllerBase
	{
		MeterReadingContext db;

		public MeterReadingsResetController(MeterReadingContext db)
		{
			this.db = db;
		}

		[HttpPost]
		public IActionResult Post()
		{
			db.MeterReadings.RemoveRange(db.MeterReadings);
			db.SaveChanges();
			return Ok();
		}
	}
}
