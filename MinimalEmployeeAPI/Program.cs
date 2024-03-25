using EmployeeAPI;
using EmployeeAPI.Abstractions;
using EmployeeAPI.Concrete;
using EmployeeAPI.Migrations;
using EmployeeAPI.Models;
using EmployeeAPI.Resources.Commands;
using EmployeeAPI.Resources.Queries;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging.Configuration;
using Serilog;
using System.Reflection;

Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(new ConfigurationBuilder().AddJsonFile("appsettings.json").Build()).CreateLogger();
var builder = WebApplication.CreateBuilder(args);


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Host.UseSerilog();


builder.Services.AddDbContext<EmployeeDb>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddTransient<IValidator, EmployeeValidator>();
builder.Services.AddTransient<IValidator<Employee>, EmployeeValidator>();
builder.Services.AddScoped<IEmployeeQueryRepositary,EmployeeQueryRepositary>();
builder.Services.AddScoped<IEmployeeCommandRepositary, EmployeeCommandRepositary>();
builder.Services.AddScoped<IEmployeeService,EmployeeService>();
builder.Services.AddScoped<ISeedFaker,SeedFaker>();

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowOrigin", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});
var app = builder.Build();
app.UseSerilogRequestLogging();
app.UseCors("AllowOrigin");
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var dbContext = services.GetRequiredService<EmployeeDb>();
    var seedFaker = services.GetRequiredService<ISeedFaker>();
    try
    {
        dbContext.Database.EnsureCreated();
        seedFaker.Initialise();
    }
    catch(Exception ex)
    {
        throw;
    }
}
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.MapGet("/employees", async (IMediator mediator) =>
{
    var result = await mediator.Send(new GetEmployeesQuery());
    var employeeDTOS = result.Data.Select(x => new EmployeeDTO(x));
    return result.Success ? Results.Ok(employeeDTOS) : Results.BadRequest(result);  
        
});
app.MapPost("/employees", async (IMediator mediator, EmployeeDTO employee) =>
{
    var command = new CreateEmployeeCommand()
    {
        Employee = employee
    };

    var result = await mediator.Send(command);
    if (result.Success)
    {
        return Results.Created($"/employee/{result.Data.Id}", result.Data);
    }
    if(result.Success == false)
    {
        return Results.BadRequest(result.Message);
    }
    return Results.StatusCode(StatusCodes.Status500InternalServerError);
});
app.MapGet("/employees/{Id}", async (IMediator mediator, int Id) =>
{
    var query = new GetEmployeeQuery() { Id = Id };
    var result = await mediator.Send(query);
    var employeeDTO = new EmployeeDTO(result.Data);
    return result.Success ? Results.Ok(employeeDTO) : Results.NotFound();
});
app.MapPut("/employees/{Id}", async (IEmployeeService employeeService, EmployeeDTO updatedEmployee, int Id) =>
{
    var dbEntity = new Employee(updatedEmployee);
    var result = await employeeService.UpdateEmployee(dbEntity, Id);
    if(result.Success == false)
    {
        return Results.NotFound();
    }    
    return Results.NoContent();

});
app.MapDelete("employee/{Id}", async (IEmployeeService employeeService, int Id) =>
{
    var result = await employeeService.DeleteEmployee(Id);
    if(result.Success == false)
    {
        return Results.NotFound();
    }
    return Results.Ok();

});

app.Logger.LogInformation("Employee Minimal API Has Started");
app.Run();
//This needs to be here so that the integration tests run with the test containers project.
public partial class Program { }

