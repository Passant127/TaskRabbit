namespace Shared.DTOS;

public class PayrollResponseDto
{
    public int Id { get; set; }
    public int EmployeeId { get; set; }
    public decimal Salary { get; set; }
    public DateTime Date { get; set; }
}
