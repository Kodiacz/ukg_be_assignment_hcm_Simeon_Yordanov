namespace HumanCapitalManagement.Application.Exceptions;

[ExceptionHttpStatusCode(HttpStatusCode.Unauthorized)]
public class NotAuthorizedException : Exception, ICustomExceptionMessage
{
	public NotAuthorizedException(string message) : base(message)
	{
		this.CustomMessage = message;
	}

	public string CustomMessage { get; private set; }
}
