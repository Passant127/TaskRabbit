using Microsoft.AspNetCore.Mvc;
using Shared.DTOS;
using Employee.Contracts;
using Employee.Models;

namespace Employee.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet]
        public IActionResult GetAllEmployees()
        {
            var employees = _employeeService.GetEmployeeList();
            return Ok(employees);
        }

        [HttpGet("{id}")]
        public IActionResult GetEmployeeById(int id)
        {
            var employee = _employeeService.GetEmployeeById(id);
            if (employee == null)
                return NotFound();

            return Ok(employee);
        }

        [HttpPost]
        public IActionResult AddEmployee([FromBody] EmployeeRequestDto employeeRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var employee = new EmployeeModel
            {
                Name = employeeRequest.Name,
                Salary = employeeRequest.Salary
            };

            var createdEmployee = _employeeService.AddEmployee(employee);
            return Ok(createdEmployee); 
        }

        [HttpPut("{id}")]
        public IActionResult UpdateEmployee(int id, [FromBody] SalaryUpdateRequestDto employeeRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

         
            var updatedEmployee = _employeeService.UpdateEmployeeSalary(id, employeeRequest);
            if (updatedEmployee == null)
                return NotFound();

            return Ok(updatedEmployee);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteEmployee(int id)
        {
            var deleted = _employeeService.DeleteEmployee(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}
