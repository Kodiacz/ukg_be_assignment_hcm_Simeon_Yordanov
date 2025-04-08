namespace HumanCapitalManagement.Infrastructure;

public class DataPersister : JsonStoreAccessor
{
	private readonly HcmDbContext dbContext;

	public DataPersister(HcmDbContext dbContext)
	{
		this.dbContext = dbContext;
	}

	public async Task PersistDataAsync()
	{
		var people = await this.dbContext.Persons.ToListAsync();
		this.SaveToJson("persons.json", people);

		var users = await this.dbContext.ApplicationUsers.Select(u => new
		{
			Id = u.Id,
			PasswordHash = u.PasswordHash,
			PasswordSalt = u.PasswordSalt,
			PersonId = u.PersonId,
		})
		.ToListAsync();
		this.SaveToJson("users.json", users);

		var roles = await this.dbContext.Roles.Select(r => new
		{
			Id = r.Id,
			Name = r.Name,
		})
		.ToListAsync();
		this.SaveToJson("roles.json", roles);

		var userRoles = await this.dbContext.UserRoles
			.Select(ur => new UserRolesToJson()
			{
				UserId = ur.UserId,
				RoleId = ur.RoleId,
			})
			.ToListAsync();

		this.SaveToJson("userroles.json", userRoles);
	}

	public class UserRolesToJson
	{
		public int UserId { get; set; }
		public int RoleId { get; set; }
	}
}
