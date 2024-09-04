namespace Shared.DTOS;

public class PayrollRequestDto
{
    public int EmployeeId { get; set; }
    public decimal Salary { get; set; }
    public DateTime Date { get; set; }
}
