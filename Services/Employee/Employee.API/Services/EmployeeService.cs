using Employee.API;
using Employee.API.Services;

namespace Employee.API.Services
{
    public class EmployeeService : IEmployeeService
    {
        private EmployeeDbContext _context;
        public EmployeeService(EmployeeDbContext emoloyeeContext)
        {
            _context = emoloyeeContext;
        }
        public Models.Employee Add(Models.Employee employee)
        {
            var emp = _context.Add(employee);
            return emp.Entity;
        }

        public Models.Employee Update(Models.Employee employee)
        {
            var emp = _context.Update(employee);
            return emp.Entity;
        }

        public bool Delete(int employeeId)
        {
            _context.Employees.Remove(new Models.Employee() { Id = employeeId });
            return true;
        }

        public Models.Employee Get(int employeeId)
        {
            return _context.Employees.Where(x => x.Id == employeeId).FirstOrDefault();
        }

        public List<Models.Employee> GetAll()
        {
            return _context.Employees.ToList();
        }
    }
}
