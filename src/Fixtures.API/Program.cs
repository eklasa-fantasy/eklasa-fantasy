using Fixtures.API.Data;
using Fixtures.API.Interfaces;
using Fixtures.API.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

DotNetEnv.Env.Load();
// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<FixturesDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("fixt-sqldata")));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IFootballApiService, FootballApiService>(); 
builder.Services.AddScoped<IFixtureService, FixtureService>();

var app = builder.Build();

app.MapDefaultEndpoints();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
