namespace HumanCapitalManagement.Application.Attributes;

public class ExceptionHttpStatusCodeAttribute : Attribute
{
	public HttpStatusCode HttpStatusCode { get; }

	public ExceptionHttpStatusCodeAttribute(HttpStatusCode httpStatusCode)
	{
		HttpStatusCode = httpStatusCode;
	}
}

