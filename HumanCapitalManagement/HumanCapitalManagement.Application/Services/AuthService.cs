namespace HumanCapitalManagement.Application.Services;

public class AuthService : IAuthService
{
	private readonly IAuthRepository authRepository;

	public AuthService(IAuthRepository authRepository)
	{
		this.authRepository = authRepository;
	}

	public async Task<UserInfoDto> LoginUserAsync(LoginInputData loginData)
	{
		var user = await this.authRepository.GetUserByEmail(loginData.Email);

		//MTODO: remove hardcoded strings and think of better exceptions
		if (user == null)
			throw new ApplicationArgumenNullException($"User with email '{loginData}' was not found.");

		if (!VerifyPasswordHash(user, loginData))
			throw new WrongPasswordException("Invalid credentials.");

		var userDto = new UserInfoDto()
		{
			Id = user.Person.Id,
			FirstName = user.Person.FirstName,
			LastName = user.Person.LastName,
			Email = user.Person.Email,
			JobTitle = user.Person.JobTitle,
			Salary = user.Person.Salary,
			Department = user.Person.Department,
			Roles = user.UserRoles.Select(ur => new GetRoleDto()
			{
				Name = ur.Role.Name,
			}).ToList()
		};

		return userDto;
	}

	public async Task RegisterUserAsync(RegisterInputData registerData)
	{
		if (!VerifyPasswordConfirmation(registerData.Password, registerData.ConfirmPassword))
			throw new InvalidOperationException("Password and confirm password do not match");

		CreateHash(registerData.Password, out byte[] passwordHash, out byte[] passwardSalt);

		ApplicationUser user = new()
		{
			PasswordHash = Convert.ToBase64String(passwordHash),
			PasswordSalt = Convert.ToBase64String(passwardSalt),
			Person = new Person()
			{
				FirstName = registerData.FirstName,
				LastName = registerData.LastName,
				Email = registerData.Email,
				JobTitle = registerData.JobTitle,
				Salary = registerData.Salary,
				Department = registerData.Department,
			},
		};

		await authRepository.CreateUserAsync(user);
	}

	private bool VerifyPasswordHash(ApplicationUser user, LoginInputData dto)
	{
		var passwordSaltByte = Convert.FromBase64String(user.PasswordSalt);
		using (var hmac = new HMACSHA512(passwordSaltByte))
		{
			var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(dto.Password));
			return computedHash.SequenceEqual(Convert.FromBase64String(user.PasswordHash));
		}
	}

	private bool VerifyPasswordConfirmation(string password, string confirmedPassword)
	{
		return password.Equals(confirmedPassword);
	}

	private static void CreateHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
	{
		using (var hmac = new HMACSHA512())
		{
			passwordSalt = hmac.Key;
			passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
		}
	}
}
