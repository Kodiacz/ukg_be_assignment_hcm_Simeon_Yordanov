namespace HumanCapitalManagement.Tests.Application.Services;

public class AdminServiceTests
{
	private readonly Mock<IApplicationUserRepository> _mockUserRepo;
	private readonly AdminService _adminService;

	public AdminServiceTests()
	{
		_mockUserRepo = new Mock<IApplicationUserRepository>();
		_adminService = new AdminService(_mockUserRepo.Object);
	}

	[Fact]
	public async Task GetAllUsers_ShouldReturnPagedResult()
	{
		var inputDto = new UserInputDto
		{
			PageNumber = 1,
			PageSize = 10,
			SortBy = SortBy.FirstName,
			SortOrder = SortOrder.Ascending
		};

		var users = new List<ApplicationUser>
		{
			new ApplicationUser
			{
				Person = new Person
				{
					FirstName = "John",
					LastName = "Doe",
					Email = "john.doe@example.com",
					JobTitle = "Developer",
					Salary = 50000,
					Department = "IT"
				},
				UserRoles = new List<UserRoles>
				{
					new UserRoles { Role = new Role { Name = "Admin" } }
				}
			},
			new ApplicationUser
			{
				Person = new Person
				{
					FirstName = "Jane",
					LastName = "Smith",
					Email = "jane.smith@example.com",
					JobTitle = "Manager",
					Salary = 70000,
					Department = "HR"
				},
				UserRoles = new List<UserRoles>
				{
					new UserRoles { Role = new Role { Name = "Manager" } }
				}
			}
		};

		_mockUserRepo.Setup(repo => repo.BuildQuery(inputDto))
			.Returns(users.AsQueryable());

		_mockUserRepo.Setup(repo => repo.CountUsersAsync(It.IsAny<IQueryable<ApplicationUser>>()))
			.ReturnsAsync(users.Count);

		_mockUserRepo.Setup(repo => repo.GetPagedUsersAsync(It.IsAny<IQueryable<ApplicationUser>>(), inputDto))
			.ReturnsAsync(users);

		var result = await _adminService.GetAllUsers(inputDto);

		Assert.NotNull(result);
		Assert.Equal(2, result.TotalCount);
		Assert.Equal(1, result.PageNumber);
		Assert.Equal(10, result.PageSize);
		Assert.NotEmpty(result.Items);
		Assert.Collection(
			result.Items,
			item => Assert.Equal("John", item.FirstName),
			item => Assert.Equal("Jane", item.FirstName));
	}

	[Fact]
	public async Task GetAllUsers_ShouldApplySortingByFirstNameAscending()
	{
		var inputDto = new UserInputDto
		{
			PageNumber = 1,
			PageSize = 10,
			SortBy = SortBy.FirstName,
			SortOrder = SortOrder.Ascending
		};

		var users = new List<ApplicationUser>
		{
			new ApplicationUser
			{
				Person = new Person
				{
					FirstName = "Adam",
					LastName = "Smith",
					Email = "adam.smith@example.com",
					JobTitle = "Developer",
					Salary = 60000,
					Department = "IT"
				}
			},
			new ApplicationUser
			{
				Person = new Person
				{
					FirstName = "Zoe",
					LastName = "Doe",
					Email = "zoe.doe@example.com",
					JobTitle = "Engineer",
					Salary = 80000,
					Department = "IT"
				}
			},
		};

		var sortedUsers = users.OrderBy(u => u.Person.FirstName).ToList();

		_mockUserRepo.Setup(repo => repo.BuildQuery(inputDto))
			.Returns(users.AsQueryable());

		_mockUserRepo.Setup(repo => repo.CountUsersAsync(It.IsAny<IQueryable<ApplicationUser>>()))
			.ReturnsAsync(users.Count);

		_mockUserRepo.Setup(repo => repo.GetPagedUsersAsync(It.IsAny<IQueryable<ApplicationUser>>(), inputDto))
			.ReturnsAsync(users);

		var result = await _adminService.GetAllUsers(inputDto);

		Assert.Collection(
			result.Items,
			item => Assert.Equal("Adam", item.FirstName),
			item => Assert.Equal("Zoe", item.FirstName));
	}

	[Fact]
	public async Task GetAllUsers_ShouldApplySearchTermFilter()
	{
		var inputDto = new UserInputDto
		{
			PageNumber = 1,
			PageSize = 10,
			SearchTerm = "Adam"
		};

		var users = new List<ApplicationUser>
		{
			new ApplicationUser
			{
				Person = new Person
				{
					FirstName = "Adam",
					LastName = "Smith",
					Email = "adam.smith@example.com",
					JobTitle = "Developer",
					Salary = 60000,
					Department = "IT"
				}
			},
			new ApplicationUser
			{
				Person = new Person
				{
					FirstName = "Zoe",
					LastName = "Doe",
					Email = "zoe.doe@example.com",
					JobTitle = "Engineer",
					Salary = 80000,
					Department = "IT"
				}
			}
		};

		_mockUserRepo.Setup(repo => repo.BuildQuery(inputDto))
			.Returns(users.Where(u => u.Person.FirstName.Contains(inputDto.SearchTerm)).AsQueryable());

		_mockUserRepo.Setup(repo => repo.CountUsersAsync(It.IsAny<IQueryable<ApplicationUser>>()))
			.ReturnsAsync(1);

		_mockUserRepo.Setup(repo => repo.GetPagedUsersAsync(It.IsAny<IQueryable<ApplicationUser>>(), inputDto))
			.ReturnsAsync(users.Where(u => u.Person.FirstName.Contains(inputDto.SearchTerm)).ToList());

		var result = await _adminService.GetAllUsers(inputDto);

		Assert.NotNull(result);
		Assert.Single(result.Items);
		Assert.Collection(
			result.Items,
			item => Assert.Equal("Adam", item.FirstName));
	}

	[Fact]
	public async Task UpdateUserRoleAsync_ShouldThrowNotImplementedException()
	{
		var input = new UserRoleUpdateInputDto();

		await Assert.ThrowsAsync<NotImplementedException>(async () =>
			await _adminService.UpdateUserRoleAsync(input));
	}
}

