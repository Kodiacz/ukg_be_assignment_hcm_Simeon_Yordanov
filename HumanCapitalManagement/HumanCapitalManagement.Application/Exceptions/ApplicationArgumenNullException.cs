namespace HumanCapitalManagement.Application.Exceptions;

[ExceptionHttpStatusCode(HttpStatusCode.NotFound)]
public class ApplicationArgumenNullException : ArgumentNullException, ICustomExceptionMessage
{
	public ApplicationArgumenNullException(string message) : base(message)
	{
		this.CustomMessage = message;
	}

	public string CustomMessage { get; private set; }
}
