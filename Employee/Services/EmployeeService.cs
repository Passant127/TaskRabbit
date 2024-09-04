using Employee.Contracts;
using Employee.Models;
using Microsoft.EntityFrameworkCore;
using Employee.Data;
using MassTransit;
using Shared.DTOS;
using Shared.Events;

namespace Employee.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly EmployeeContext _employeeContext;
        private readonly IPublishEndpoint _publishEndpoint;

        public EmployeeService(EmployeeContext employeeContext, IPublishEndpoint publishEndpoint)
        {
            _employeeContext = employeeContext;
            _publishEndpoint = publishEndpoint;
        }

        public async Task<EmployeeResponseDto> AddEmployee(EmployeeModel employee)
        {
            if (employee == null) throw new ArgumentNullException(nameof(employee));

            _employeeContext.Employees.Add(employee);
             _employeeContext.SaveChanges();

            var employeeAddedEvent = new EmployeeAddedEvent
            {
                EmployeeId = employee.Id,
                Name = employee.Name,
                Salary = employee.Salary
            };
             _publishEndpoint.Publish(employeeAddedEvent);

            return new EmployeeResponseDto
            {
                Name = employee.Name,
                Salary = employee.Salary
            };
        }

        public EmployeeResponseDto UpdateEmployeeSalary(int id,SalaryUpdateRequestDto employee)
        {
            var existingEmployee = _employeeContext.Employees.Find(id);
            if (existingEmployee == null) return null;

          

            if (existingEmployee.Salary != employee.NewSalary)
            {
                existingEmployee.Salary = employee.NewSalary;

                // Publish SalaryUpdatedEvent to MassTransit
                var salaryUpdatedEvent = new SalaryUpdatedEvent
                {
                    EmployeeId = existingEmployee.Id,
                    NewSalary = existingEmployee.Salary
                };
                _publishEndpoint.Publish(salaryUpdatedEvent);
            }

            _employeeContext.SaveChanges();

            return new EmployeeResponseDto
            {
                Id = id,
                Name = existingEmployee.Name,
                Salary = existingEmployee.Salary
            };
        }

        public bool DeleteEmployee(int id)
        {
            var employee = _employeeContext.Employees.Find(id);
            if (employee == null) return false;

            _employeeContext.Employees.Remove(employee);
            _employeeContext.SaveChanges();

            return true;
        }

        public EmployeeResponseDto GetEmployeeById(int id)
        {
            var employee = _employeeContext.Employees.Find(id);
            if (employee == null) return null;

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
