using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;

using Newtonsoft.Json.Serialization;

using EnsekTest.Implementation;
using EnsekTest.Integrations.EntityFramework;

namespace EnsekTest.WebApi
{
	public class Startup
	{
		readonly DatabaseConfiguration databaseConfig;

		public Startup(IConfiguration configuration, IWebHostEnvironment env)
		{
			Configuration = configuration;

			databaseConfig = Configuration.GetSection("Database").Get<DatabaseConfiguration>();
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			var builderAction = new MeterReadingContextFactory().GetDbContextOptionsBuildAction(databaseConfig);
			services.AddDbContext<MeterReadingContext>(builderAction);

			services.AddControllers(x => x.Filters.Add<SaveDatabaseChangesActionFilter>())
				.AddNewtonsoftJson(x =>
				{
					x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
					x.SerializerSettings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter(new CamelCaseNamingStrategy()));
				});

			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo { Title = "api", Version = "v1" });
			});

			services.AddControllers(x => x.Filters.Add<SaveDatabaseChangesActionFilter>());

			services.AddTransient<IAccountsRepository, AccountsRepository>();
			services.AddTransient<IMeterReadingsRepository, MeterReadingRepository>();
			services.AddTransient<IMeterReadingUploadService, MeterReadingUploadService>();
			services.AddTransient<IMeterReadingParser, CsvMeterReadingParser>();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseSwagger();
				app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "api v1"));
			}

			app.UseRouting();

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});

			using (var scope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
			{
				var log = scope.ServiceProvider.GetService<ILogger<Startup>>();
				var db = scope.ServiceProvider.GetService<MeterReadingContext>();

				log.LogInformation($"Running {System.Runtime.InteropServices.RuntimeInformation.FrameworkDescription} on {System.Runtime.InteropServices.RuntimeInformation.OSDescription}");

				log.LogInformation(
					$"Database provider: {databaseConfig.Provider}" + Environment.NewLine +
					$"Database migrations: {(databaseConfig.EnableMigrations ? "Enabled" : "Disabled")}"
				);

				if (databaseConfig.EnableMigrations) db.Database.Migrate();
				else if (env.IsDevelopment())
				{
					db.Database.EnsureDeleted();
					db.Database.EnsureCreated();
				}

				if (db.Accounts.Count() == 0)
				{
					log.LogWarning("Inserting seed data into a (hopefully) fresh database");
					db.SeedDatabase();
				}
			}
		}
	}
}
