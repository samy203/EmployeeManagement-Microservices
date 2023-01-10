using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.Messages
{
    public class EmployeePromotionFailed
    {
        public int EmployeeId { get; set; }
        public int RollbackRole { get; set; }
    }
}
