namespace Task1.Models;

public class Payroll :BaseEntity
{
    
    public int EmployeeId { get; set; }
    public decimal Salary { get; set; }
    public DateTime PayDate { get; set; }
}
