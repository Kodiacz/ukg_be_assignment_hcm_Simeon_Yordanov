namespace HumanCapitalManagement.Application.Services;

public class AdminService : IAdminService
{
	private readonly IApplicationUserRepository userRepo;

	public AdminService(IApplicationUserRepository userRepo)
	{
		this.userRepo = userRepo;
	}

	public async Task<PagedResult<UserGetDto>> GetAllUsers(UserInputDto inputDto)
	{
		IQueryable<ApplicationUser> query = this.userRepo.BuildQuery(inputDto);

		int totalCount = await this.userRepo.CountUsersAsync(query);

		List<ApplicationUser> users = await this.userRepo.GetPagedUsersAsync(query, inputDto);

		List<UserGetDto> userDtos = users.Select(u => new UserGetDto
		{
			FirstName = u.Person.FirstName,
			LastName = u.Person.LastName,
			Email = u.Person.Email,
			JobTitle = u.Person.JobTitle,
			Salary = u.Person.Salary,
			Department = u.Person.Department,
			Roles = u.UserRoles.Select(r => new GetRoleDto { Name = r.Role.Name }).ToList(),
		}).ToList();

		return new PagedResult<UserGetDto>
		{
			Items = userDtos,
			TotalCount = totalCount,
			PageNumber = inputDto.PageNumber,
			PageSize = inputDto.PageSize,
		};
	}

	public Task UpdateUserRoleAsync(UserRoleUpdateInputDto input)
	{
		throw new NotImplementedException();
	}
}
