namespace Task1.Models;

public class Employee :BaseEntity
{
    public string Name { get; set; }
    public decimal Salary { get; set; }
    public ICollection<Payroll> Payrolls { get; set; } = new HashSet<Payroll>();
}
