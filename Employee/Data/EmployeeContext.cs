using Microsoft.EntityFrameworkCore;
using Employee.Models;

namespace Employee.Data;

public class EmployeeContext:DbContext
{
    public EmployeeContext(DbContextOptions dbContext):base(dbContext)
    {
        
    }
    public DbSet<EmployeeModel> Employees { get; set; }
  
}
