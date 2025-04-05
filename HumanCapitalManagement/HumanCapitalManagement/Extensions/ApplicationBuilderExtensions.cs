using HumanCapitalManagement.Infrastructure;

namespace HumanCapitalManagement.API.Extensions;

public static class ApplicationBuilderExtensions
{
	public static async Task SeedDatabase(this IApplicationBuilder app)
	{
		using (var scope = app.ApplicationServices.CreateScope())
		{
			var dataSeeder = scope.ServiceProvider.GetRequiredService<DataSeeder>();
			await dataSeeder.SeedData();
		}
	}
}
