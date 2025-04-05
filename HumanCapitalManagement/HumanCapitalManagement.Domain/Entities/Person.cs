namespace HumanCapitalManagement.Domain.Entities;

public class Person
{
	// Usually I go for Guid but for simplicity I'll use int
	public int Id { get; set; }

	public string FirstName { get; set; } = null!;

	public string LastName { get; set; } = null!;

	public string Email { get; set; } = null!;

	public string JobTitle { get; set; } = null!;

	public decimal Salary { get; set; }

	public string Department { get; set; } = null!;
}