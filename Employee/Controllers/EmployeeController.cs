using Microsoft.AspNetCore.Mvc;
using Task1.Contracts;
using Task1.DTOS;
using Task1.Models;

namespace Task1.Controllers
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
        public IActionResult UpdateEmployee(int id, [FromBody] EmployeeRequestDto employeeRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var employee = new EmployeeModel
            {
                Id = id,
                Name = employeeRequest.Name,
               
                Salary = employeeRequest.Salary
            };

            var updatedEmployee = _employeeService.UpdateEmployee(employee);
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
