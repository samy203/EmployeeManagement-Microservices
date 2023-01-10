using Department.Core;
using Department.Core.Services;
using EventBus;
using EventBus.Messages;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "OfficeManagement.DepartmentAPI", Version = "v1" });
});

builder.Services.AddOptions();
builder.Services.Configure<DepartmentDBConfig>(builder.Configuration);
builder.Services.AddTransient<IDepartmentService, DepartmentService>();
builder.Services.AddTransient<IEventBus, EventBus.EventBus>();
builder.Services.AddSingleton<IDbClient, DbClient>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "DepartmentAPI v1"));
}


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();


//subscribe to message bus
// should have better infrastructure, but for this demo, this will suffice
var bus = app.Services.GetService<IEventBus>();

bus.Subscribe<EmployeeHired>("employee-hired", async msg =>
{
    var service = app.Services.GetService<IDepartmentService>();
    var department = await service.Get(msg.EmployeeDepartmentId);
    if (department == null) return;
    department.EmployeesCount = department.EmployeesCount + 1;
    service.Update(department);
});

bus.Subscribe<EmployeePromoted>("employee-promoted", async msg =>
{
    var service = app.Services.GetService<IDepartmentService>();
    var department = await service.Get(msg.DepartmentId);
    if (department == null) return;

    //department already has manager
    if (department.ManagerId != 0)
    {
        bus.Publish(new EmployeePromotionFailed() { EmployeeId = msg.EmployeeId, RollbackRole = msg.PreviousRole });
        return;
    }

    department.ManagerId = msg.EmployeeId;
    service.Update(department);
});




app.Run();
