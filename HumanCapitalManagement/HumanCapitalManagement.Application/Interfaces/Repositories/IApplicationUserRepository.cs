

namespace HumanCapitalManagement.Application.Interfaces.Repositories;

public interface IApplicationUserRepository
{
	// General Methods
	Task<ApplicationUser> GetByIdAsync(int id);
	Task<ApplicationUser> GetByIdAsyncAsReadOnly(int id);
	Task<ApplicationUser> GetByPersonIdAsync(int personId);
	Task<ApplicationUser> GetByPersonIdAsyncAsReadOnly(int personId);
	Task AddAsync(ApplicationUser user);
	Task UpdateAsync(ApplicationUser user);
	Task DeleteAsync(int id);

	// Admin-Specific Methods
	IQueryable<ApplicationUser> BuildQuery(UserInputDto input);
	Task<int> CountUsersAsync(IQueryable<ApplicationUser> query);
	Task<List<ApplicationUser>> GetPagedUsersAsync(IQueryable<ApplicationUser> query, UserInputDto input);
}
