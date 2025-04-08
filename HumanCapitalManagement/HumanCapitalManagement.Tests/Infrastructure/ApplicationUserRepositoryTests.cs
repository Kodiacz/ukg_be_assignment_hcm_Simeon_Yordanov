namespace HumanCapitalManagement.Tests.Infrastructure;

public class ApplicationUserRepositoryTests : TestBase
{
	[Fact]
	public void BuildQuery_ShouldReturnQueryWithFilters()
	{
		var dbContext = base.CreateDbContext();
		var repository = new ApplicationUserRepository(dbContext);

		var inputDto = new UserInputDto
		{
			SearchTerm = "John",
			MinSalary = 50000,
			MaxSalary = 150000,
			SortBy = SortBy.FirstName,
			SortOrder = SortOrder.Ascending
		};

		var users = new List<ApplicationUser>
			{
				new ApplicationUser
				{
					PasswordHash = "awdawd",
					PasswordSalt = "adwawdawd",
					Person = new Person
					{
						FirstName = "John",
						LastName = "Doe",
						Email = "john.doe@example.com",
						JobTitle = "Developer",
						Salary = 100000,
						Department = "IT"
					},
					UserRoles = new List<UserRoles>
					{
						new UserRoles
						{
							Role = new Role { Name = "Admin" }
						}
					}
				}
			};

		dbContext.ApplicationUsers.AddRange(users);
		dbContext.SaveChanges();


		var result = repository.BuildQuery(inputDto);

		Assert.NotNull(result);
		var queryResult = result.ToList();
		Assert.Single(queryResult);
		Assert.Equal("John", queryResult.First().Person.FirstName);
	}

	[Fact]
	public async Task CountUsersAsync_ShouldReturnCorrectCount()
	{
		var dbContext = base.CreateDbContext();
		var repository = new ApplicationUserRepository(dbContext);

		var users = new List<ApplicationUser>
			{
				new ApplicationUser
				{
					PasswordHash = "awdawd",
					PasswordSalt = "adwawdawd",
					Person = new Person
					{
						FirstName = "Ben",
						LastName = "Oscar",
						Email = "ben.oscar@example.com",
						JobTitle = "Developer",
						Salary = 200000,
						Department = "IT"
					},
					UserRoles = new List<UserRoles>
					{
						new UserRoles
						{
							Role = new Role { Name = "Manager" }
						}
					}
				},
				new ApplicationUser
				{
					PasswordHash = "awdawd",
					PasswordSalt = "adwawdawd",
					Person = new Person
					{
						FirstName = "John",
						LastName = "Doe",
						Email = "john.doe@example.com",
						JobTitle = "Developer",
						Salary = 100000,
						Department = "IT"
					},
					UserRoles = new List<UserRoles>
					{
						new UserRoles
						{
							Role = new Role { Name = "Admin" }
						}
					}
				}
			};

		dbContext.ApplicationUsers.AddRange(users);
		dbContext.SaveChanges();

		var result = await repository.CountUsersAsync(dbContext.ApplicationUsers.AsQueryable());

		Assert.Equal(2, result);
	}

	[Fact]
	public async Task GetPagedUsersAsync_ShouldReturnCorrectPagedResults()
	{
		var dbContext = base.CreateDbContext();
		var repository = new ApplicationUserRepository(dbContext);

		var inputDto = new UserInputDto
		{
			PageNumber = 1,
			PageSize = 1
		};

		var users = new List<ApplicationUser>
			{
				new ApplicationUser
				{
					PasswordHash = "awdawd",
					PasswordSalt = "adwawdawd",
					Person = new Person
					{
						FirstName = "John",
						LastName = "Doe",
						Email = "john.doe@example.com",
						JobTitle = "Developer",
						Salary = 100000,
						Department = "IT"
					},
					UserRoles = new List<UserRoles>
					{
						new UserRoles
						{
							Role = new Role { Name = "Admin" }
						}
					}
				},
				new ApplicationUser
				{
					PasswordHash = "awdawd",
					PasswordSalt = "adwawdawd",
					Person = new Person
					{
						FirstName = "Jane",
						LastName = "Smith",
						Email = "jane.smith@example.com",
						JobTitle = "Designer",
						Salary = 80000,
						Department = "Design"
					},
					UserRoles = new List<UserRoles>
					{
						new UserRoles
						{
							Role = new Role { Name = "User" }
						}
					}
				}
			};

		dbContext.ApplicationUsers.AddRange(users);
		dbContext.SaveChanges();

		var result = await repository.GetPagedUsersAsync(dbContext.ApplicationUsers.AsQueryable(), inputDto);

		Assert.NotNull(result);
		Assert.Single(result);
		Assert.Equal("John", result.First().Person.FirstName);
	}
}
