using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MinimalAPISampleProject.Data;
using MinimalAPISampleProject.Dtos;
using MinimalAPISampleProject.Models;
using System.Windows.Input;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add SQL Service
var sqlConnBuilder = new SqlConnectionStringBuilder();
sqlConnBuilder.ConnectionString = builder.Configuration.GetConnectionString("SqlDbConnection");
sqlConnBuilder.UserID = builder.Configuration["UserId"];
sqlConnBuilder.Password = builder.Configuration["Password"];
builder.Services.AddDbContext<AppDbContext>(o => o.UseSqlServer(sqlConnBuilder.ConnectionString));
// Add ICommandRepo Service
builder.Services.AddScoped<ICommandRepo, CommandRepo>();
// Add Automapper service
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateTime.Now.AddDays(index),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast");

// Get Commands 
app.MapGet("api/v1/commands", async(ICommandRepo repo , IMapper mapper) =>
{
    var commands = await repo.GetAllCommands();
    return Results.Ok(mapper.Map<IEnumerable<CommandReadDto>>(commands));
});

// Get Command By Id 
app.MapGet("api/v1/commands/{id}", async (ICommandRepo repo, IMapper mapper ,[FromRoute] int id) => {
    
    var command = await repo.GetCommandById(id);
    if(command == null)
    {
        return Results.NotFound();
    }
    return Results.Ok(mapper.Map<CommandReadDto>(command));
});

// Create Command
app.MapPost("api/v1/commands", async (ICommandRepo repo, IMapper mapper, CommandCreateDto cmdCreateDto) => {
    // cmd -> command
    var cmdModel = mapper.Map<Command>(cmdCreateDto);
    await repo.CreateCommand(cmdModel);
    await repo.SaveChanges();
    var cmdReadDto = mapper.Map<CommandReadDto>(cmdModel);
    return Results.Created($"api/v1/commnads/{cmdReadDto.Id}", cmdReadDto);
});

// Update Command 
app.MapPut("api/v1/commands/{Id}", async (ICommandRepo repo, IMapper mapper,int id, CommandUpdateDto cmdUpdateDto) => {

    var command = await repo.GetCommandById(id);
    if (command == null)
    {
        return Results.NotFound();
    }
    mapper.Map(cmdUpdateDto, id);

    await repo.SaveChanges();
    return Results.NoContent();
});

// Delete Command 
app.MapDelete("api/v1/commands/{Id}", async (ICommandRepo repo, IMapper mapper, int id) => {

    var command = await repo.GetCommandById(id);
    if (command == null)
    {
        return Results.NotFound();
    }
    repo.DeleteCommand(command);
    await repo.SaveChanges();
    return Results.NoContent();
});

app.Run();

internal record WeatherForecast(DateTime Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}