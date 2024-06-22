using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using TaskManager.Server.Data;
using TaskManager.Server.Extensions;

var builder = WebApplication.CreateBuilder(args);



// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var allowedUrls = builder.Configuration["ALLOWED_URLS"] ?? "";
Console.WriteLine(allowedUrls);

builder.Services.AddCors(options =>
{
    options.AddPolicy("addApi", policy =>
    {
        policy.WithOrigins(allowedUrls?.Split(";"));
        policy.AllowAnyMethod();
        policy.AllowAnyHeader();
        policy.AllowCredentials();
    });
});

// if (args.ToList().Contains("--RunMigrations"))
// {
//     var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>().UseSqlite("Data Source=./sqlite.db");
//     await using var dbContext = new AppDbContext(optionsBuilder.Options);
//     await dbContext.Database.MigrateAsync();
// }

builder.Services.AddDbContext<AppDbContext>(options =>
options.UseSqlite("Data Source=./sqlite.db"));


builder.Services.AddScoped<IDbStore, DbStore>();


var app = builder.Build();

if (args.ToList().Contains("--RunMigrations"))
{
    using var scope = app.Services.CreateScope();
    await using var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    await dbContext.Database.MigrateAsync();
}


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast")
.WithOpenApi();

app.UseCors("addApi");
app.MapTaskItemEndpoints();

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
