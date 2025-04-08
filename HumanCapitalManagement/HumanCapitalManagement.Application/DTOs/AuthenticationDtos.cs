namespace HumanCapitalManagement.Application.DTOs;

public class JwtToken
{

	public string AccessToken { get; set; } = null!;

	public UserInfoDto User { get; set; } = null!;
}

public class UserInfoDto
{
	public int Id { get; set; }

	public string FirstName { get; set; } = null!;

	public string LastName { get; set; } = null!;

	public string Email { get; set; } = null!;

	public string JobTitle { get; set; } = null!;

	public decimal Salary { get; set; }

	public string Department { get; set; } = null!;

	public ICollection<GetRoleDto> Roles { get; set; } = null!;
}

public class LoginInputData
{
	[Required]
	public string Email { get; set; } = null!;

	[Required]
	public string Password { get; set; } = null!;
}

public class RegisterInputData : LoginInputData
{
	[Required]
	public string ConfirmPassword { get; set; } = null!;

	[Required]
	public string FirstName { get; set; } = null!;

	[Required]
	public string LastName { get; set; } = null!;

	[Required]
	public decimal Salary { get; set; }

	[Required]
	public string JobTitle { get; set; } = null!;

	[Required]
	public string Department { get; set; } = null!;
}