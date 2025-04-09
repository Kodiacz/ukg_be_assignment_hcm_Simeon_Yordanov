namespace HumanCapitalManagement.Application.Interfaces.Services;

public interface IAdminService
{
	Task<PagedResult<UserGetDto>> GetAllUsers(UserInputDto inputDto);
	Task UpdateUserRoleAsync(UserRoleUpdateInputDto input);
	Task AddRoleToUser(AddUserRoleDto addUserRoleDto);
}
