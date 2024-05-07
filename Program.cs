using Microsoft.EntityFrameworkCore;
using Planetas_StarWars.Data;
using Planetas_StarWars.Endpoints;
using Planetas_StarWars.Services;

var builder = WebApplication.CreateBuilder(args);

string? connectionString = builder.Configuration.GetConnectionString("PlanetsContext");
builder.Services.AddDbContext<PlanetsContext>(options => options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<HttpClient>();
builder.Services.AddScoped<MovieAppearanceService>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var dbContext = services.GetRequiredService<PlanetsContext>();
    dbContext.Database.Migrate();
}


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.RegisterPlanetsEndpoints();

app.Run();