using Employee.API.Requests;
using Employee.API.Services;
using EventBus;
using EventBus.Messages;
using Microsoft.AspNetCore.Mvc;

namespace Employee.API.Controllers
{
    [Route("[controller]")]
    public class EmoloyeeController : Controller
    {
        private IEmployeeService _employeeService;
        private EmployeeDbContext _dbContext;
        private IEventBus _eventBus;

        public EmoloyeeController(IEmployeeService employeeService, EmployeeDbContext dbContext, IEventBus eventBus)
        {
            _employeeService = employeeService;
            _dbContext = dbContext;
            _eventBus = eventBus;
        }


        [HttpPost()]
        public JsonResult AddEmployee([FromBody] CreateEmployeeRequest request)
        {
            // can use auto mapper
            var employee = new Models.Employee
            {
                Name = request.Name,
                Age = request.Age,
                Salary = request.Salary,
                DepartmentId = request.DepartmentId,
                Role = request.Role,
            };

            var result = _employeeService.Add(employee);

            // could use unit of work pattern
            _dbContext.SaveChanges();

            _eventBus.Publish(new EmployeeHired(result.Id, result.DepartmentId));

            return Json(result);
        }

        [HttpPut("{id}")]
        public JsonResult UpdateEmployee(int id, [FromBody] UpdateEmployeeRequest request)
        {
            // can use auto mapper
            var employee = new Models.Employee
            {
                Id = id,
                Name= request.Name,
                Age = request.Age,
                Salary = request.Salary,
                DepartmentId = request.DepartmentId,
                Role = request.Role,
            };

            var result = _employeeService.Update(employee);

            // could use unit of work pattern
            _dbContext.SaveChanges();

            return Json(result);
        }
        [HttpGet("{id}")]
        public JsonResult GetEmployee(int id)
        {
            return Json(_employeeService.Get(id));
        }

        [HttpGet()]
        public JsonResult GetEmployees()
        {
            return Json(_employeeService.GetAll());
        }

        [HttpDelete("{id}")]
        public JsonResult DeleteEmoployee(int id)
        {
            var deleted = _employeeService.Delete(id);

            // could use unit of work pattern
            _dbContext.SaveChanges();

            return Json(deleted);
        }

        [HttpPut("{id}/promote")]
        public JsonResult PromoteEmployee(int id)
        {
            var employee = _employeeService.Get(id);

            if (employee.Role == Enums.Role.Manager)
                return Json("Employee is a manager already.");

            var previousRole = employee.Role;
            employee.Role = Enums.Role.Manager;

           var updatedEmployee = _employeeService.Update(employee);

            _eventBus.Publish(new EmployeePromoted() { EmployeeId = employee.Id, PreviousRole = (int)previousRole, DepartmentId = employee.DepartmentId});

            _dbContext.SaveChanges();

            return Json(updatedEmployee);
        }
    }
}
