// Services/EmployeeService.cs
using Task1.Contracts;
using Task1.DTOS;
using Task1.Models;
using Microsoft.EntityFrameworkCore;
using Task1.Data;
using Task1.RabitMQ;

namespace Task1.Services
{
    public class EmployeeService : IEmployeeService
    {
 
        private readonly EmployeeContext _employeeContext;
        private readonly RabitMQProducer _salaryUpdateProducer;

        public EmployeeService(EmployeeContext employeeContext, RabitMQProducer salaryUpdateProducer)
        {
            _employeeContext = employeeContext;
            _salaryUpdateProducer = salaryUpdateProducer;
        }

      
        public EmployeeRequestDto AddEmployee(Employee employee)
        {
            if (employee == null)
            {
                throw new ArgumentNullException(nameof(employee));
            }

            _employeeContext.Employees.Add(employee);
            _employeeContext.SaveChanges();

            return new EmployeeRequestDto
            {
                Name = employee.Name,

                Salary = employee.Salary
            };
        }


        public EmployeeRequestDto UpdateEmployee(Employee employee)
        {
            var existingEmployee = _employeeContext.Employees.Find(employee.Id);
            if (existingEmployee == null)
            {
                return null;
            }

            existingEmployee.Name = employee.Name;

            if (existingEmployee.Salary != employee.Salary)
            {
                existingEmployee.Salary = employee.Salary;


                var salaryUpdateMessage = new
                {
                    EmployeeId = existingEmployee.Id,
                    NewSalary = existingEmployee.Salary
                };

                _salaryUpdateProducer.SendMessage(salaryUpdateMessage);
            }

            _employeeContext.SaveChanges();

            return new EmployeeRequestDto
            {
                Name = existingEmployee.Name,

                Salary = existingEmployee.Salary
            };
        }
        public bool DeleteEmployee(int id)
        {
            var employee = _employeeContext.Employees.Find(id);
            if (employee == null)
            {
                return false;
            }

            _employeeContext.Employees.Remove(employee);
            _employeeContext.SaveChanges();

            return true;
        }

        public EmployeeResponseDto GetEmployeeById(int id)
        {
            var employee = _employeeContext.Employees.Find(id);
            if (employee == null)
            {
                return null;
            }

            return new EmployeeResponseDto
            {
                Id = employee.Id,
                Name = employee.Name,
                Salary = employee.Salary
            };
        }

        public IEnumerable<EmployeeResponseDto> GetEmployeeList()
        {
            return _employeeContext.Employees
                .Select(e => new EmployeeResponseDto
                {
                    Id = e.Id,
                    Name = e.Name,
                    Salary = e.Salary
                })
                .ToList();
        }

      
    }
}
