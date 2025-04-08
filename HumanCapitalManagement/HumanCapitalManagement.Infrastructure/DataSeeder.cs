namespace HumanCapitalManagement.Infrastructure;

public class DataSeeder : JsonStoreAccessor
{
	private readonly HcmDbContext dbContext;

	public DataSeeder(HcmDbContext dbContext)
	{
		this.dbContext = dbContext;
	}


	public async Task SeedData()
	{
		await this.SeedPersonData();
		await this.SeedUserData();
		await this.SeedRoleData();
		await this.SeedUserRolesData();
	}

	private async Task SeedPersonData()
	{
		var data = LoadFromJson<Person>("persons.json");

		if (!this.dbContext.Persons.Any())
			await this.AddToDBContext(data);
	}

	private async Task SeedUserData()
	{
		var data = LoadFromJson<ApplicationUser>("users.json");
		if (!this.dbContext.ApplicationUsers.Any())
			await this.AddToDBContext(data);
	}

	private async Task SeedRoleData()
	{
		var data = LoadFromJson<Role>("roles.json");
		if (!this.dbContext.Roles.Any())
			await this.AddToDBContext(data);
	}

	private async Task SeedUserRolesData()
	{
		var data = LoadFromJson<UserRoles>("userroles.json");
		if (!this.dbContext.UserRoles.Any())
			await this.AddToDBContext(data);
	}

	private async Task AddToDBContext<T>(List<T> data)
	{
		foreach (var item in data)
		{
			await this.dbContext.AddAsync(item); // here on the first atempt of adding an ApplicationUser entity
		}

		await this.dbContext.SaveChangesAsync();
	}
}