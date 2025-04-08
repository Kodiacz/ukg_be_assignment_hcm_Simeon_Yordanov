namespace HumanCapitalManagement.Application.Interfaces.Services;

public interface IAuthService
{
	Task<UserInfoDto> LoginUserAsync(LoginInputData email);
	Task RegisterUserAsync(RegisterInputData registerData);
}
