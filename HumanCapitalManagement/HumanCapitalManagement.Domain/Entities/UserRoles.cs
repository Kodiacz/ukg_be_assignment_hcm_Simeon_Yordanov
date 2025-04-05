namespace HumanCapitalManagement.Domain.Entities;

public class UserRoles
{
	public int UserId { get; set; }
	public ApplicationUser User { get; set; } = null!;

	public int RoleId { get; set; }
	public Role Role { get; set; } = null!;
}
