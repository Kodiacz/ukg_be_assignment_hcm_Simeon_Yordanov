

namespace HumanCapitalManagement.Application.Interfaces.Repositories;

public interface IApplicationUserRepository
{
	// General Methods
	Task<ApplicationUser> GetByEmailAsync(int email);
	Task<ApplicationUser> GetByEmailAsyncAsReadOnly(int email);
	Task<ApplicationUser> GetByIdAsync(int id);
	Task<ApplicationUser> GetByIdAsyncAsReadOnly(int id);
	Task AddAsync(ApplicationUser user);
	Task UpdateAsync(ApplicationUser user);
	Task DeleteAsync(int id);

	// Admin-Specific Methods
	IQueryable<ApplicationUser> BuildQuery(UserInputDto input);
	Task<int> CountUsersAsync(IQueryable<ApplicationUser> query);
	Task<List<ApplicationUser>> GetPagedUsersAsync(IQueryable<ApplicationUser> query, UserInputDto input);
}
