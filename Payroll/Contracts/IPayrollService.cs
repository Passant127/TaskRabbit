using Task1.DTOS;

namespace Task1.Contracts;

public interface IPayrollService
{
    Task<IEnumerable<PayrollResponseDto>> GetAllPayrollsAsync();
    Task<PayrollResponseDto> GetPayrollByIdAsync(int payrollId);
    Task<PayrollResponseDto> PayrollAsync(PayrollRequestDto payroll);
    Task<PayrollResponseDto> UpdatePayrollAsync(int payrollId, PayrollRequestDto payroll);
    Task<bool> DeletePayrollAsync(int payrollId);
}
