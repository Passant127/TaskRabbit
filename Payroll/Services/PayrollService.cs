using Microsoft.EntityFrameworkCore;
using Payroll.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shared.DTOS;
using Payrolls.Models;
using Payroll.Contracts;

namespace Payroll.Services
{
    public class PayrollService : IPayrollService
    {
        private readonly PayrollContext _payrollContext;

        public PayrollService(PayrollContext payrollContext)
        {
            _payrollContext = payrollContext;
        }

        public async Task<bool> DeletePayrollAsync(int id)
        {
            var payroll = await _payrollContext.Payrolls.FindAsync(id);
            if (payroll == null) return false;

            _payrollContext.Payrolls.Remove(payroll);
            await _payrollContext.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<PayrollResponseDto>> GetAllPayrollsAsync()
        {
            return await _payrollContext.Payrolls
                .Select(p => new PayrollResponseDto
                {
                    Id = p.Id,
                    EmployeeId = p.EmployeeId,
                    Salary = p.Salary,
                    Date = p.PayDate
                })
                .ToListAsync();
        }

        public async Task<PayrollResponseDto> GetPayrollByIdAsync(int id)
        {
            var payroll = await _payrollContext.Payrolls.FindAsync(id);
            if (payroll == null) return null;

            return new PayrollResponseDto
            {
                Id = payroll.Id,
                EmployeeId = payroll.EmployeeId,
                Salary = payroll.Salary,
                Date = payroll.PayDate
            };
        }


    }
}
