namespace HumanCapitalManagement.Infrastructure.Repositories;

public class AuthRepository : IAuthRepository
{
	private readonly HcmDbContext dbContext;

	public AuthRepository(HcmDbContext dbContext)
	{
		this.dbContext = dbContext;
	}

	public async Task CreateUserAsync(ApplicationUser registerData)
	{
		await this.dbContext.AddAsync(registerData);
		await this.dbContext.SaveChangesAsync();
	}

	public async Task<ApplicationUser?> GetUserByEmail(string email)
	{
		var user = await this.dbContext
			.ApplicationUsers
			.Include(u => u.Person)
			.Include(u => u.UserRoles)
			.ThenInclude(ur => ur.Role)
			.FirstOrDefaultAsync(u => u.Person.Email == email);

		return user;
	}
}
