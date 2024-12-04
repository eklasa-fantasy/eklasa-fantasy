using Results.API.Data;
using Results.API.Interfaces;
using Results.API.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

DotNetEnv.Env.Load();

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddDbContext<ResultsDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("res-sqldata")));

    builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular", policy =>{
        policy.WithOrigins("http://localhost:65018")
        .AllowAnyHeader()
        .AllowAnyMethod()
        .AllowCredentials();
    });
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IFootballApiService, FootballApiService>();
builder.Services.AddScoped<IResultsService, ResultsService>();
builder.Services.AddScoped<ITableService, TableService>();

var app = builder.Build();

app.MapDefaultEndpoints();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowAngular");

app.UseAuthorization();

app.MapControllers();

app.Run();
