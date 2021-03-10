namespace EnsekTest
{
	public class DatabaseConfiguration
	{
		public string ConnectionString { get; set; }
		public DatabaseProvider Provider { get; set; }
		public bool EnableMigrations { get; set; }
	}
}
