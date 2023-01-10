using Employee.API.Enums;

namespace Employee.API.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public int Salary { get; set; }
        public Role Role { get; set; }
        public string DepartmentId { get; set; }
    }
}
