using Microsoft.AspNetCore.Mvc;
using Task1.Contracts;
using Task1.DTOS;

namespace Task1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PayrollController : ControllerBase
    {
        private readonly IPayrollService _payrollService;

        public PayrollController(IPayrollService payrollService)
        {
            _payrollService = payrollService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPayrolls()
        {
            var payrolls = await _payrollService.GetAllPayrollsAsync();
            return Ok(payrolls);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPayrollById(int id)
        {
            var payroll = await _payrollService.GetPayrollByIdAsync(id);
            if (payroll == null)
                return NotFound();

            return Ok(payroll);
        }

        [HttpPost]
        public async Task<IActionResult> AddPayroll([FromBody] PayrollRequestDto payrollRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var createdPayroll = await _payrollService.PayrollAsync(payrollRequest);
            return CreatedAtAction(nameof(GetPayrollById), new { id = createdPayroll }, createdPayroll);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePayroll(int id, [FromBody] PayrollRequestDto payrollRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var updatedPayroll = await _payrollService.UpdatePayrollAsync(id, payrollRequest);
            if (updatedPayroll == null)
                return NotFound();

            return Ok(updatedPayroll);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePayroll(int id)
        {
            var deleted = await _payrollService.DeletePayrollAsync(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}
