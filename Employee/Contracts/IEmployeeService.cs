using Shared.DTOS;
using Employee.Models;

namespace Employee.Contracts;

public interface IEmployeeService
{
    public IEnumerable<EmployeeResponseDto> GetEmployeeList();
    public EmployeeResponseDto GetEmployeeById(int id);
    public Task<EmployeeResponseDto> AddEmployee(EmployeeModel employee);
    public EmployeeResponseDto UpdateEmployeeSalary(int id, SalaryUpdateRequestDto employee);
    public bool DeleteEmployee(int Id);
}
