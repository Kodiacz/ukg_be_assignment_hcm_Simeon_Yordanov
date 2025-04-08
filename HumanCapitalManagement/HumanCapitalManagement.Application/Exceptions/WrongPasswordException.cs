namespace HumanCapitalManagement.Application.Exceptions;

[ExceptionHttpStatusCode(HttpStatusCode.BadRequest)]
public class WrongPasswordException : ArgumentException, ICustomExceptionMessage
{
	public WrongPasswordException(string message) : base(message)
	{
		this.CustomMessage = message;
	}

	public string CustomMessage { get; private set; }
}
