using System;
using System.Collections.Generic;

namespace EnsekTest
{
	public class Account
	{
		public int AccountId { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }

		public IEnumerable<MeterReading> MeterReadings { get; set; }
	}

	public class DatabaseConfiguration
	{
		public string ConnectionString { get; set; }
		public DatabaseProvider Provider { get; set; }
		public bool EnableMigrations { get; set; }
	}

	public enum DatabaseProvider
	{
		SqlServer,
		Npgsql
	}
}
