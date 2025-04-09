namespace HumanCapitalManagement.Application.Exceptions;

[ExceptionHttpStatusCode(HttpStatusCode.BadRequest)]
public class RoleAlreadyAssignedException : Exception
{
	public RoleAlreadyAssignedException(string role)
		: base($"User already has the role '{role}'.") { }
}
