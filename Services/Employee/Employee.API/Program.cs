using Employee.API;
using Employee.API.Enums;
using Employee.API.Services;
using EventBus;
using EventBus.Messages;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "OfficeManagement.EmployeeAPI", Version = "v1" });
});

builder.Services.AddDbContext<Employee.API.EmployeeDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

builder.Services.AddTransient<IEmployeeService, EmployeeService>();
builder.Services.AddTransient<IEventBus, EventBus.EventBus>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

//run migrations
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<EmployeeDbContext>();
    if (context.Database.GetPendingMigrations().Any())
    {
        context.Database.Migrate();
    }
}

//subscribe to message bus
// should have better infrastructure, but for this demo, this will suffice
var bus = app.Services.GetService<IEventBus>();

bus.Subscribe<EmployeePromotionFailed>("employee-promoted-failed-sub-id", msg =>
{
    using var scope = app.Services.CreateScope();

    var service = scope.ServiceProvider.GetService<IEmployeeService>();
    var employee = service.Get(msg.EmployeeId);
    if (employee == null) return;

    employee.Role = (Role)msg.RollbackRole;
    service.Update(employee);

    var context = scope.ServiceProvider.GetService<EmployeeDbContext>();
    context.SaveChanges();
});

app.Run();
