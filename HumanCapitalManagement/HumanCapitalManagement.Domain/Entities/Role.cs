namespace HumanCapitalManagement.Domain.Entities;

public class Role
{
	public Role()
	{
		this.UserRoles = new List<UserRoles>();
	}

	public int Id { get; set; }

	public string Name { get; set; } = null!;

	public ICollection<UserRoles> UserRoles { get; set; }
}
