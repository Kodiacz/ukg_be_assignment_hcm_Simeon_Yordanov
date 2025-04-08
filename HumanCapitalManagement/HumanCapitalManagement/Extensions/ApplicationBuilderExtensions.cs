namespace HumanCapitalManagement.API.Extensions;

public static class ApplicationBuilderExtensions
{
	public static async Task SeedDatabase(this IApplicationBuilder builder)
	{
		using (var scope = builder.ApplicationServices.CreateScope())
		{
			var dataSeeder = scope.ServiceProvider.GetRequiredService<DataSeeder>();
			await dataSeeder.SeedData();
		}
	}

	public static async Task PersistData(this WebApplication app)
	{

		app.Lifetime.ApplicationStopped.Register(async () =>
		{
			using (var scope = app.Services.CreateScope())
			{
				var dataPersister = scope.ServiceProvider.GetRequiredService<DataPersister>();
				await dataPersister.PersistDataAsync();
			}
		});
	}

	public static IServiceCollection AddApplicationServices(this IServiceCollection services)
	{
		#region Repositories

		services.AddScoped<IApplicationUserRepository, ApplicationUserRepository>();
		services.AddScoped<IApplicationUserRepository, ApplicationUserRepository>();
		services.AddScoped<IAuthRepository, AuthRepository>();

		#endregion

		#region Services

		services.AddScoped<DataSeeder>();
		services.AddScoped<DataPersister>();
		services.AddScoped<IAdminService, AdminService>();
		services.AddScoped<IAuthService, AuthService>();
		services.AddScoped<IJwtProvider, JwtProvider>();

		#endregion

		return services;
	}

	public static IServiceCollection AddApplicationAuthentication(this IServiceCollection services, WebApplicationBuilder builder)
	{
		var jwtSettings = builder.Configuration;

		services
			.AddAuthentication(opt =>
			{
				opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
				opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
			})
			.AddJwtBearer(opt =>
			{
				opt.RequireHttpsMetadata = false;
				opt.SaveToken = true;
				opt.TokenValidationParameters = new TokenValidationParameters
				{
					ValidateIssuer = false,
					ValidateAudience = false,
					ValidateIssuerSigningKey = true,
					IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Jwt:Secret"]!)),
					RoleClaimType = ClaimTypes.Role,
				};
			});
		services.ConfigureOptions<JwtOptionsSetup>();
		services.AddAuthorization();

		return services;
	}

	public static IServiceCollection ConfigureSwagger(this IServiceCollection services)
	{
		return services.AddSwaggerGen(c =>
		{
			c.SwaggerDoc("v1", new OpenApiInfo { Title = "Human Capital Management API", Version = "v1" });
			c.UseInlineDefinitionsForEnums();

			c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
			{
				In = ParameterLocation.Header,
				Name = "Authorization",
				Type = SecuritySchemeType.ApiKey,
				Scheme = "Bearer",
				BearerFormat = "JWT",
				Description = "Enter your Bearer token in the format 'Bearer {token}'"
			});

			c.AddSecurityRequirement(new OpenApiSecurityRequirement()
			{
				{
					new OpenApiSecurityScheme
					{
						Reference = new OpenApiReference
						{
							Type = ReferenceType.SecurityScheme,
							Id = "Bearer"
						}
					},
					new string[] { }
				}
			});

		});
	}

}

public static class ExceptionStatusCodeMappings
{
	public static Dictionary<Type, int> ExceptionStatusCode { get; } = new();

	public static void AddExceptionStatusCodeMappings(this IServiceCollection services)
	{
		var targetAssembly = AppDomain.CurrentDomain
			.GetAssemblies()
			.FirstOrDefault(a => a.GetName().Name == "HumanCapitalManagement.Application");

		var exceptionTypes = targetAssembly.GetTypes()
			.Where(t =>
				t is { IsClass: true, IsAbstract: false } &&
				typeof(Exception).IsAssignableFrom(t) &&
				t.Namespace == "HumanCapitalManagement.Application.Exceptions" &&
				t.GetCustomAttribute<ExceptionHttpStatusCodeAttribute>() is not null
			);

		foreach (var exceptionType in exceptionTypes)
		{
			var attribute = exceptionType.GetCustomAttribute<ExceptionHttpStatusCodeAttribute>()!;
			ExceptionStatusCode[exceptionType] = (int)attribute.HttpStatusCode;
		}
	}
}
