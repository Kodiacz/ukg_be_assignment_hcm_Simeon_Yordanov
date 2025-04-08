using HumanCapitalManagement.Application.Exceptions;
using HumanCapitalManagement.Domain.Entities;

namespace HumanCapitalManagement.Tests.Application.Services;

public class AuthServiceTests
{
	private readonly Mock<IAuthRepository> mockRepo;
	private readonly AuthService authService;

	public AuthServiceTests()
	{
		mockRepo = new Mock<IAuthRepository>();
		authService = new AuthService(mockRepo.Object);
	}

	[Fact]
	public async Task LoginUserAsync_ShouldReturnUserDto_WhenCredentialsAreValid()
	{
		var password = "password123";
		var (hash, salt) = CreateTestHash(password);

		var user = new ApplicationUser
		{
			PasswordHash = Convert.ToBase64String(hash),
			PasswordSalt = Convert.ToBase64String(salt),
			Person = new Person
			{
				Id = 1,
				FirstName = "Jane",
				LastName = "Doe",
				Email = "jane@example.com",
				JobTitle = "Engineer",
				Salary = 60000,
				Department = "R&D"
			},
			UserRoles = new List<UserRoles> { new() { Role = new Role { Name = "Admin" } } }
		};

		var loginData = new LoginInputData
		{
			Email = user.Person.Email,
			Password = password
		};

		mockRepo.Setup(r => r.GetUserByEmail(user.Person.Email)).ReturnsAsync(user);

		var result = await authService.LoginUserAsync(loginData);

		Assert.NotNull(result);
		Assert.Equal("Jane", result.FirstName);
		Assert.Contains(result.Roles, r => r.Name == "Admin");
	}

	[Fact]
	public async Task LoginUserAsync_ShouldThrow_WhenUserNotFound()
	{
		mockRepo.Setup(r => r.GetUserByEmail(It.IsAny<string>())).ReturnsAsync((ApplicationUser)null);

		var loginData = new LoginInputData { Email = "notfound@example.com", Password = "any" };

		await Assert.ThrowsAsync<ApplicationArgumenNullException>(() => authService.LoginUserAsync(loginData));
	}

	[Fact]
	public async Task LoginUserAsync_ShouldThrow_WhenPasswordIsWrong()
	{
		var (hash, salt) = CreateTestHash("correct-password");

		var user = new ApplicationUser
		{
			PasswordHash = Convert.ToBase64String(hash),
			PasswordSalt = Convert.ToBase64String(salt),
			Person = new Person()
		};

		mockRepo.Setup(r => r.GetUserByEmail(It.IsAny<string>())).ReturnsAsync(user);

		var loginData = new LoginInputData { Email = "jane@example.com", Password = "wrong-password" };

		await Assert.ThrowsAsync<WrongPasswordException>(() => authService.LoginUserAsync(loginData));
	}

	[Fact]
	public async Task RegisterUserAsync_ShouldCreateUser_WhenPasswordsMatch()
	{
		var registerData = new RegisterInputData
		{
			FirstName = "John",
			LastName = "Doe",
			Email = "john@example.com",
			JobTitle = "Manager",
			Department = "Sales",
			Salary = 70000,
			Password = "abc123",
			ConfirmPassword = "abc123"
		};

		ApplicationUser capturedUser = null;
		mockRepo.Setup(r => r.CreateUserAsync(It.IsAny<ApplicationUser>()))
				.Callback<ApplicationUser>(u => capturedUser = u)
				.Returns(Task.CompletedTask);

		await authService.RegisterUserAsync(registerData);

		Assert.NotNull(capturedUser);
		Assert.Equal("John", capturedUser.Person.FirstName);
		Assert.NotNull(capturedUser.PasswordHash);
		Assert.NotNull(capturedUser.PasswordSalt);
	}

	[Fact]
	public async Task RegisterUserAsync_ShouldThrow_WhenPasswordsDoNotMatch()
	{
		var registerData = new RegisterInputData
		{
			Password = "pass1",
			ConfirmPassword = "pass2"
		};

		await Assert.ThrowsAsync<InvalidOperationException>(() => authService.RegisterUserAsync(registerData));
	}

	private static (byte[] Hash, byte[] Salt) CreateTestHash(string password)
	{
		using var hmac = new System.Security.Cryptography.HMACSHA512();
		var salt = hmac.Key;
		var hash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
		return (hash, salt);
	}
}
