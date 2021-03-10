using System;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc.Filters;

using EnsekTest.Integrations.EntityFramework;

namespace EnsekTest
{
	public class SaveDatabaseChangesActionFilter : IAsyncActionFilter
	{
		readonly MeterReadingContext db;

		public SaveDatabaseChangesActionFilter(MeterReadingContext db)
		{
			this.db = db;
		}

		public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
		{
			var result = await next();
			if (result.Exception == null || result.ExceptionHandled)
			{
				await db.SaveChangesAsync();
			}
		}
	}
}
