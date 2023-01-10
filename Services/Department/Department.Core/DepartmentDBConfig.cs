using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Department.Core
{
    public class DepartmentDBConfig
    {
        public string Database_Name { get; set; }
        public string Employees_Collection_Name { get; set; }
        public string Departments_Collection_Name { get; set; }
        public string Connection_String { get; set; }
    }
}
