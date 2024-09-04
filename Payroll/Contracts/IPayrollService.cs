using Shared.DTOS;


namespace Payroll.Contracts;

public interface IPayrollService
{
    Task<IEnumerable<PayrollResponseDto>> GetAllPayrollsAsync();
    Task<PayrollResponseDto> GetPayrollByIdAsync(int payrollId);
    Task<bool> DeletePayrollAsync(int payrollId);
}
