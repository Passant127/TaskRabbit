using Microsoft.AspNetCore.Mvc;
using Shared.DTOS;
using Payroll.Contracts;


namespace Payroll.Controllers
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
