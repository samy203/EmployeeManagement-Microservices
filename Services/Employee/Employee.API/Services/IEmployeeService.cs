namespace Employee.API.Services
{
    public interface IEmployeeService
    {
        Models.Employee Add(Models.Employee employee);
        Models.Employee Update(Models.Employee employee);
        Models.Employee Get(int employeeId);
        List<Models.Employee> GetAll();
        bool Delete(int employeeId);
    }
}
