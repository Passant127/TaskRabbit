using Microsoft.EntityFrameworkCore;
using Task1.Models;

namespace Task1.Data;

public class EmployeeContext:DbContext
{
    public EmployeeContext(DbContextOptions dbContext):base(dbContext)
    {
        
    }
    public DbSet<EmployeeModel> Employees { get; set; }
  
}
