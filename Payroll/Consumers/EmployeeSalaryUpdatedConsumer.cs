using MassTransit;
using Shared.Events;
using System.Threading.Tasks;
using Payroll.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Payrolls.Consumers
{
    public class EmployeeSalaryUpdatedConsumer : IConsumer<SalaryUpdatedEvent>
    {
        private readonly PayrollContext _payrollContext;

        public EmployeeSalaryUpdatedConsumer(PayrollContext payrollContext)
        {
            _payrollContext = payrollContext;
        }

        public async Task Consume(ConsumeContext<SalaryUpdatedEvent> context)
        {
            var message = context.Message;

            // Find all payroll records associated with the employee
            var payrolls = await _payrollContext.Payrolls
                .Where(p => p.EmployeeId == message.EmployeeId)
                .ToListAsync();

            // Update the salary for each payroll record
            foreach (var payroll in payrolls)
            {
                payroll.Salary = message.NewSalary;
            }

            // Save the changes to the database
            await _payrollContext.SaveChangesAsync();
        }
    }
}
