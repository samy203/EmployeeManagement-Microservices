using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.Messages
{
    public class EmployeePromoted
    {
        public int EmployeeId { get; set; }
        public int PreviousRole { get; set; }

        public string DepartmentId { get; set; }
    }
}
