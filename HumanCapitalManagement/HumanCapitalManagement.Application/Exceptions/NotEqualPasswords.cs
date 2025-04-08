namespace HumanCapitalManagement.Application.Exceptions;

[ExceptionHttpStatusCode(HttpStatusCode.BadRequest)]
public class NotEqualPasswordsException : ArgumentException, ICustomExceptionMessage
{
	public NotEqualPasswordsException(string message) : base(message)
	{
		this.CustomMessage = message;
	}

	public string CustomMessage { get; private set; }
}
