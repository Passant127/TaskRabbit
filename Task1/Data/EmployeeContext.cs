using Microsoft.EntityFrameworkCore;
using Task1.Models;

namespace Task1.Data;

public class EmployeeContext:DbContext
{
    protected readonly IConfiguration Configuration;
    public EmployeeContext(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseSqlServer(Configuration.GetConnectionString("EmployeeConnection"));
    }
    public DbSet<Employee> Employees { get; set; }
  
}
