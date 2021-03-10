using System;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace EnsekTest.Integrations.EntityFramework
{
	public class MeterReadingContextFactory : IDesignTimeDbContextFactory<MeterReadingContext>
	{
		public Action<DbContextOptionsBuilder> GetDbContextOptionsBuildAction(DatabaseConfiguration config)
		{
			// Add configuration that is common to all providers here
			return (builder) => builder
				.AddProviderSpecificOptions(config);
		}

		public MeterReadingContext CreateDbContext(string[] args)
		{
			if (args.Length != 2) throw new ArgumentException("Expected the name of a provider and a connection string as two parameters");

			var config = new DatabaseConfiguration
			{
				Provider = Enum.Parse<DatabaseProvider>(args[0], true),
				ConnectionString = args[1]
			};

			var builder = new DbContextOptionsBuilder<MeterReadingContext>();
			var build = GetDbContextOptionsBuildAction(config);
			build.Invoke(builder);

			return new MeterReadingContext(builder.Options);
		}
	}
}
