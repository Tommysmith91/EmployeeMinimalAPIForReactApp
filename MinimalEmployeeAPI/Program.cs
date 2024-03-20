using EmployeeAPI;
using EmployeeAPI.Abstractions;
using EmployeeAPI.Concrete;
using EmployeeAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Configuration;
using Serilog;

Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(new ConfigurationBuilder().AddJsonFile("appsettings.json").Build()).CreateLogger();
var builder = WebApplication.CreateBuilder(args);


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Host.UseSerilog();


builder.Services.AddDbContext<EmployeeDb>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IEmployeeRepositary,EmployeeRepositary>();
builder.Services.AddScoped<IEmployeeService,EmployeeService>();
builder.Services.AddScoped<ISeedFaker,SeedFaker>();

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



app.MapGet("/employees", async (IEmployeeService employeeService) =>
{
    var result = await employeeService.GetEmployees();    
    return result.Success ? Results.Ok(result.Data) : Results.BadRequest(result);  
        
});
app.MapPost("/employees", async (IEmployeeService employeeService, Employee employee) =>
{
    var result = await employeeService.CreateEmployee(employee);
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
app.MapGet("/employees/{Id}", async (IEmployeeService employeeService, int Id) =>
{
    var result = await employeeService.GetEmployee(Id);    
    return result.Success ? Results.Ok(result.Data) : Results.NotFound();
});
app.MapPut("/employees/{Id}", async (IEmployeeService employeeService, Employee updatedEmployee, int Id) =>
{
    var result = await employeeService.UpdateEmployee(updatedEmployee,Id);
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

