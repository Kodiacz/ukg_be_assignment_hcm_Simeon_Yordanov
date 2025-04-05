namespace HumanCapitalManagement.Domain.Entities;

public class ApplicationUser
{
	public ApplicationUser()
	{
		this.UserRoles = new List<UserRoles>();
	}

	public int Id { get; set; }

	public string PasswardHash { get; set; } = null!;

	public string PasswardSalt { get; set; } = null!;

	public ICollection<UserRoles> UserRoles { get; set; }

	public int PersonId { get; set; }
	public Person Person { get; set; }
}