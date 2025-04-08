namespace HumanCapitalManagement.Tests.Infrastructure;

public abstract class TestBase
{
	protected HcmDbContext CreateDbContext([CallerMemberName] string dbName = "")
	{
		var options = new DbContextOptionsBuilder<HcmDbContext>()
			.UseInMemoryDatabase(databaseName: dbName + Guid.NewGuid())
			.Options;

		return new HcmDbContext(options);
	}
}
