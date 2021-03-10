using System.Linq;

namespace EnsekTest.Integrations.EntityFramework
{
	public class AccountsRepository : IAccountsRepository
	{
		MeterReadingContext db;

		public AccountsRepository(MeterReadingContext db)
		{
			this.db = db;
		}

		public bool Exists(int accountId)
		{
			var existing = db.Accounts.FirstOrDefault(x => x.AccountId == accountId);
			return (existing != null);
		}
	}
}
