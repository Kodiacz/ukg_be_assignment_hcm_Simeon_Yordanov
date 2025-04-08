namespace HumanCapitalManagement.API.Extensions;

public static class FilterExtensions
{
	public static FilterCollection AddApplicationFilters(this FilterCollection filterCollection)
	{
		filterCollection.Add(typeof(ExceptionHandler));

		return filterCollection;
	}
}