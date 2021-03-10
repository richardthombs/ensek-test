
using Microsoft.EntityFrameworkCore;

namespace EnsekTest.Integrations.EntityFramework
{
	public class MeterReadingContext : DbContext
	{
		public DbSet<Account> Accounts { get; set; }
		public DbSet<MeterReading> MeterReadings { get; set; }

		public MeterReadingContext(DbContextOptions<MeterReadingContext> options) : base(options)
		{
		}

		protected override void OnModelCreating(ModelBuilder builder)
		{
			builder.Entity<Account>()
				.HasMany(x => x.MeterReadings)
				.WithOne(x => x.Account);
		}
	}
}
