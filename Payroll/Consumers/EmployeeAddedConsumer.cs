using MassTransit;
using Payrolls.Models;
using Shared.Events;
using Payroll.Data;


namespace Payrolls.Consumers;

    public class EmployeeAddedConsumer : IConsumer<EmployeeAddedEvent>
    {
        private readonly PayrollContext _payrollContext;

        public EmployeeAddedConsumer(PayrollContext payrollContext)
        {
            _payrollContext = payrollContext;
        }

        public async Task Consume(ConsumeContext<EmployeeAddedEvent> context)
        {
            var message = context.Message;

            var payroll = new PayrollModel
            {
                EmployeeId = message.EmployeeId,
                Salary = message.Salary,
                PayDate = DateTime.Now 
            };

            _payrollContext.Payrolls.Add(payroll);
            await _payrollContext.SaveChangesAsync();
        }
    }
