using Task1.Contracts;
using Task1.DTOS;
using Task1.Models;
using Microsoft.EntityFrameworkCore;
using Task1.Data;

namespace Task1.Services
{
    public class PayrollService : IPayrollService
    {
        private readonly PayrollContext _payrollContext;

        public PayrollService(PayrollContext payrollContext)
        {
            _payrollContext = payrollContext;
        }

        public async Task<bool> DeletePayrollAsync(int Id)
        {
            var payroll = await _payrollContext.Payrolls.FindAsync(Id);
            if (payroll == null)
            {
                return false;
            }

            _payrollContext.Payrolls.Remove(payroll);
            await _payrollContext.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<PayrollResponseDto>> GetAllPayrollsAsync()
        {
            var payrolls = await _payrollContext.Payrolls.ToListAsync();
            return payrolls.Select(p => new PayrollResponseDto
            {
                Id = p.Id,
                EmployeeId = p.EmployeeId,
                Salary = p.Salary,
                Date = p.PayDate
            }).ToList();
        }

        public async Task<PayrollResponseDto> GetPayrollByIdAsync(int Id)
        {
            var payroll = await _payrollContext.Payrolls.FindAsync(Id);
            if (payroll == null)
            {
                return null;
            }

            return new PayrollResponseDto
            {
                Id = payroll.Id,
                EmployeeId = payroll.EmployeeId,
                Salary = payroll.Salary,
                Date = payroll.PayDate
            };
        }

        public async Task<PayrollResponseDto> PayrollAsync(PayrollRequestDto payrollRequest)
        {
            var payroll = new PayrollModel
            {
                EmployeeId = payrollRequest.EmployeeId,
                Salary = payrollRequest.Salary,
                PayDate = payrollRequest.Date
            };

            _payrollContext.Payrolls.Add(payroll);
            await _payrollContext.SaveChangesAsync();

            return new PayrollResponseDto
            {
                Id = payroll.Id,
                EmployeeId = payroll.EmployeeId,
                Salary = payroll.Salary,
                Date = payroll.PayDate
            };
        }
        public async Task<PayrollResponseDto> UpdatePayrollAsync(int Id, PayrollRequestDto payrollRequest)
        {
            var payroll = await _payrollContext.Payrolls.FindAsync(Id);
            if (payroll == null)
            {
                return null;
            }

            payroll.Salary = payrollRequest.Salary;
            payroll.PayDate = payrollRequest.Date;

            _payrollContext.Payrolls.Update(payroll);
            await _payrollContext.SaveChangesAsync();

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
