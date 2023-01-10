using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.Messages
{
    public class EmployeeHired
    {
        public int EmployeeId { get; set; }
        public string EmployeeDepartmentId { get; set; }

        public EmployeeHired(int employeeId, string employeeDepartmentId)
        {
            EmployeeId = employeeId;
            EmployeeDepartmentId = employeeDepartmentId;
        }
    }
}
