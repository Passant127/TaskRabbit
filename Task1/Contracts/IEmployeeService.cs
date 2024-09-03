using Task1.DTOS;
using Task1.Models;

namespace Task1.Contracts;

public interface IEmployeeService
{
    public IEnumerable<EmployeeResponseDto> GetEmployeeList();
    public EmployeeResponseDto GetEmployeeById(int id);
    public EmployeeRequestDto AddEmployee(Employee employee);
    public EmployeeRequestDto UpdateEmployee(Employee employee);
    public bool DeleteEmployee(int Id);
}
