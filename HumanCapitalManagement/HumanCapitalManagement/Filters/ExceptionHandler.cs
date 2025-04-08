using HumanCapitalManagement.Application.Interfaces.Common;
using Microsoft.Extensions.Logging;
using static HumanCapitalManagement.API.Extensions.ApplicationBuilderExtensions;
using System.Net;

namespace HumanCapitalManagement.API.Filters;

public class ExceptionHandler : IExceptionFilter
{
	public void OnException(ExceptionContext context)
	{
		ProblemDetails problemDeitals = new()
		{
			Title = TryGetCustomErrorMessage(context.Exception),
			Status = GetStatusCodeForException(context.Exception),
			Detail = context.Exception.InnerException?.Message,
		};

		context.Result = new ObjectResult(problemDeitals);

		context.ExceptionHandled = true;
	}


	/// <summary>
	/// Determines the HTTP status code to associate with a given exception.
	/// If a specific status code mapping exists for the exception type in the 
	/// <see cref="ExceptionStatusCodeMappings.ExceptionStatusCode"/> dictionary, that status code is returned.
	/// Otherwise, the default status code <see cref="HttpStatusCode.BadRequest"/> (400) is returned.
	/// </summary>
	/// <param name="exception">The exception for which to retrieve the corresponding status code.</param>
	/// <returns>The mapped status code if found; otherwise, <see cref="HttpStatusCode.BadRequest"/>.</returns>
	private int GetStatusCodeForException(Exception exception)
	{
		Type exceptionType = exception.GetType();

		if (ExceptionStatusCodeMappings.ExceptionStatusCode.TryGetValue(exceptionType, out int statusCode))
			return statusCode;

		return (int)HttpStatusCode.BadRequest;
	}

	/// <summary>
	/// Attempts to extract a custom error message from exceptions that implement the <see cref="ICustomExceptionMessage"/> interface.
	/// If the exception does not implement this interface, the default exception message is returned.
	/// </summary>
	/// <param name="exception">The exception from which to extract the message.</param>
	/// <returns>The custom exception message if available; otherwise, the default exception message.</returns>
	private string TryGetCustomErrorMessage(Exception exception)
	=> exception is ICustomExceptionMessage customExceptionMessage
		? customExceptionMessage.CustomMessage
		: exception.Message;
}
