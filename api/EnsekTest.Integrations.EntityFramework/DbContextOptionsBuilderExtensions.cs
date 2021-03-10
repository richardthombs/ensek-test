using System;

using Microsoft.EntityFrameworkCore;

namespace EnsekTest.Integrations.EntityFramework
{
	static class DbContextOptionsBuilderExtensions
	{
		const string MigrationsAssemblyName = "EnsekTest.WebApi";

		public static DbContextOptionsBuilder AddProviderSpecificOptions(this DbContextOptionsBuilder builder, DatabaseConfiguration config)
		{
			switch (config.Provider)
			{
				case DatabaseProvider.SqlServer:
					return builder.UseSqlServer(config.ConnectionString, options => options
						.MigrationsAssembly(MigrationsAssemblyName)
						.UseQuerySplittingBehavior(QuerySplittingBehavior.SingleQuery)
					);

				case DatabaseProvider.Npgsql:
					return builder.UseNpgsql(config.ConnectionString, options => options
						.MigrationsAssembly(MigrationsAssemblyName)
						.UseQuerySplittingBehavior(QuerySplittingBehavior.SingleQuery)
					);

				case DatabaseProvider.Sqlite:
					return builder.UseSqlite(config.ConnectionString, options => options
							.MigrationsAssembly(MigrationsAssemblyName)
							.UseQuerySplittingBehavior(QuerySplittingBehavior.SingleQuery)
					);

				default: throw new NotSupportedException(config.Provider.ToString());
			}
		}
	}
}
