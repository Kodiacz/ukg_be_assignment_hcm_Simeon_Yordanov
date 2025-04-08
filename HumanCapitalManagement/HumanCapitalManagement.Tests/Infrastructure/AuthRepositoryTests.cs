namespace HumanCapitalManagement.Tests.Infrastructure;

public class AuthRepositoryTests : TestBase
{
	private static ApplicationUser GetTestUser(string email = "john.doe@example.com")
	{
		return new ApplicationUser
		{
			PasswordHash = "someHash",
			PasswordSalt = "someSalt",
			Person = new Person
			{
				FirstName = "John",
				LastName = "Doe",
				Email = email,
				JobTitle = "Engineer",
				Salary = 90000,
				Department = "IT"
			},
			UserRoles = new List<UserRoles>
				{
					new UserRoles
					{
						Role = new Role
						{
							Name = "Admin"
						}
					}
				}
		};
	}

	[Fact]
	public async Task CreateUserAsync_ShouldAddUserToDatabase()
	{
		var dbContext = base.CreateDbContext();
		var repository = new AuthRepository(dbContext);
		var user = GetTestUser();

		await repository.CreateUserAsync(user);

		var savedUser = await dbContext.ApplicationUsers
			.Include(u => u.Person)
			.FirstOrDefaultAsync();

		Assert.NotNull(savedUser);
		Assert.Equal("john.doe@example.com", savedUser.Person.Email);
	}

	[Fact]
	public async Task GetUserByEmail_ShouldReturnCorrectUser()
	{
		var dbContext = base.CreateDbContext();
		var user = GetTestUser();
		await dbContext.ApplicationUsers.AddAsync(user);
		await dbContext.SaveChangesAsync();

		var repository = new AuthRepository(dbContext);

		var result = await repository.GetUserByEmail("john.doe@example.com");

		Assert.NotNull(result);
		Assert.Equal("John", result.Person.FirstName);
		Assert.Collection(result.UserRoles, item => Assert.Equal("Admin", item.Role.Name));
	}

	[Fact]
	public async Task GetUserByEmail_ShouldReturnNullIfNotFound()
	{
		var dbContext = base.CreateDbContext();
		var repository = new AuthRepository(dbContext);

		var result = await repository.GetUserByEmail("nonexistent@example.com");

		Assert.Null(result);
	}
}
