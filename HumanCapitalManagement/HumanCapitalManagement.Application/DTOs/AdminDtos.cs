namespace HumanCapitalManagement.Application.DTOs;

public class UserInputDto
{
	public int PageNumber { get; set; } = 1;
	public int PageSize { get; set; } = 10;
	public string? SearchTerm { get; set; }
	public decimal? MinSalary { get; set; }
	public decimal? MaxSalary { get; set; }
	public SortBy? SortBy { get; set; } = DTOs.SortBy.FirstName;
	public SortOrder? SortOrder { get; set; } = DTOs.SortOrder.Ascending;
}

public class UserGetDto
{
	public UserGetDto()
	{
		this.Roles = new List<GetRoleDto>();
	}

	public string FirstName { get; set; } = null!;
	public string LastName { get; set; } = null!;
	public string Email { get; set; } = null!;
	public string JobTitle { get; set; } = null!;
	public string Department { get; set; } = null!;
	public decimal Salary { get; set; }
	public ICollection<GetRoleDto> Roles { get; set; } = null!;
}

public class PagedResult<T>
{
	public PagedResult()
	{
		this.Items = new List<T>();
	}

	public IEnumerable<T> Items { get; set; }
	public int TotalCount { get; set; }
	public int PageNumber { get; set; }
	public int PageSize { get; set; }
}

public class UserRoleUpdateInputDto
{
	public string Email { get; set; }

	public ApplicationRoles Role { get; set; }
}

public class AddUserRoleDto : UserRoleUpdateInputDto;

public enum SortBy
{
	FirstName,
	LastName,
	Email,
	JobTitle,
	Salary,
	Department,
}

public enum SortOrder
{
	Ascending,
	Descending,
}

public enum GroupBy
{
	FirstName,
	LastName,
	JobTitle,
	Salary,
	Department,
}

public enum ApplicationRoles
{
	Admin,
	Manager,
	Employee,
}