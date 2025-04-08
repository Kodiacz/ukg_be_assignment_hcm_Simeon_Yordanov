var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(configure =>
{
	configure.Filters.AddApplicationFilters();
})
.AddJsonOptions(options =>
{
	options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.ConfigureSwagger();

builder.Services.AddDbContext<HcmDbContext>(options =>
	options.UseInMemoryDatabase("HcmDatabase"));
builder.Services.AddApplicationServices();
builder.Services.AddApplicationAuthentication(builder);
builder.Services.AddExceptionStatusCodeMappings();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();

	await app.SeedDatabase();
	await app.PersistData();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.Run();
