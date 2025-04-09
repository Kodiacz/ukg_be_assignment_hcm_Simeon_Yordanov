namespace HumanCapitalManagement.Infrastructure.Repositories;

public class ApplicationUserRepository : IApplicationUserRepository
{
	private readonly HcmDbContext dbContext;

	public ApplicationUserRepository(HcmDbContext dbContext)
	{
		this.dbContext = dbContext;
	}

	#region Create

	public Task AddAsync(ApplicationUser user)
	{
		throw new NotImplementedException();
	}

	#endregion

	#region Delete

	public Task DeleteAsync(int id)
	{
		throw new NotImplementedException();
	}

	#endregion

	#region Read

	public IQueryable<ApplicationUser> BuildQuery(UserInputDto input)
	{
		var query = this.dbContext.ApplicationUsers
				.Include(x => x.Person)
				.Include(x => x.UserRoles)
				.ThenInclude(x => x.Role)
				.AsQueryable();

		if (!string.IsNullOrEmpty(input.SearchTerm))
			query = query.Where(u => u.Person.FirstName.Contains(input.SearchTerm) ||
									 u.Person.LastName.Contains(input.SearchTerm) ||
									 u.Person.Email.Contains(input.SearchTerm) ||
									 u.Person.JobTitle.Contains(input.SearchTerm) ||
									 u.Person.Department.Contains(input.SearchTerm));

		if (input.MinSalary.HasValue)
			query = query.Where(u => u.Person.Salary >= input.MinSalary.Value);

		if (input.MaxSalary.HasValue)
			query = query.Where(u => u.Person.Salary <= input.MaxSalary.Value);

		if (input.SortBy.HasValue)
		{
			var sortOrder = input.SortOrder ?? SortOrder.Ascending;

			switch (input.SortBy.Value)
			{
				case SortBy.FirstName:
					query = sortOrder == SortOrder.Ascending
						? query.OrderBy(u => u.Person.FirstName)
						: query.OrderByDescending(u => u.Person.FirstName);
					break;
				case SortBy.LastName:
					query = sortOrder == SortOrder.Ascending
						? query.OrderBy(u => u.Person.LastName)
						: query.OrderByDescending(u => u.Person.LastName);
					break;
				case SortBy.Email:
					query = sortOrder == SortOrder.Ascending
						? query.OrderBy(u => u.Person.Email)
						: query.OrderByDescending(u => u.Person.Email);
					break;
				case SortBy.JobTitle:
					query = sortOrder == SortOrder.Ascending
						? query.OrderBy(u => u.Person.JobTitle)
						: query.OrderByDescending(u => u.Person.JobTitle);
					break;
				case SortBy.Salary:
					query = sortOrder == SortOrder.Ascending
						? query.OrderBy(u => u.Person.Salary)
						: query.OrderByDescending(u => u.Person.Salary);
					break;
				case SortBy.Department:
					query = sortOrder == SortOrder.Ascending
						? query.OrderBy(u => u.Person.Department)
						: query.OrderByDescending(u => u.Person.Department);
					break;
				default:
					break;
			}
		}

		return query;
	}

	public async Task<int> CountUsersAsync(IQueryable<ApplicationUser> query)
	{
		return await query.CountAsync();
	}

	public async Task<List<ApplicationUser>> GetPagedUsersAsync(IQueryable<ApplicationUser> query, UserInputDto input)
	{
		return await query
			.Skip((input.PageNumber - 1) * input.PageSize)
			.Take(input.PageSize)
			.ToListAsync();
	}

	public Task<ApplicationUser> GetByEmailAsync(int id)
	{
		throw new NotImplementedException();
	}

	#endregion

	public Task<ApplicationUser> GetByEmailAsyncAsReadOnly(int id)
	{
		throw new NotImplementedException();
	}

	public Task<ApplicationUser> GetByIdAsync(int personId)
	{
		throw new NotImplementedException();
	}

	public Task<ApplicationUser> GetByIdAsyncAsReadOnly(int personId)
	{
		throw new NotImplementedException();
	}

	#region Update

	public Task UpdateAsync(ApplicationUser user)
	{
		throw new NotImplementedException();
	}

	#endregion
}
