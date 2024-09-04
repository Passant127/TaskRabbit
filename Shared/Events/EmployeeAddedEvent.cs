using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Events;

public class EmployeeAddedEvent
{
    public int EmployeeId { get; set; }
    public string Name { get; set; }
    public decimal Salary { get; set; }
}