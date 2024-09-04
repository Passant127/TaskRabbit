using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Events;

public class SalaryUpdatedEvent
{
    public int EmployeeId { get; set; }
    public decimal NewSalary { get; set; }
}
