namespace HumanCapitalManagement.Application.Interfaces.Repositories;

public interface IAuthRepository
{
	Task CreateUserAsync(ApplicationUser registerData);
	Task<ApplicationUser?> GetUserByEmail(string email);
}
