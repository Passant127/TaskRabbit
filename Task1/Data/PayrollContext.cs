using Microsoft.EntityFrameworkCore;
using Task1.Models;

namespace Task1.Data;

public class PayrollContext : DbContext
{
    protected readonly IConfiguration Configuration;
    public PayrollContext(IConfiguration configuration)
    {
        Configuration = configuration;
    }
    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseSqlServer(Configuration.GetConnectionString("PayrollConnection"));
    }

    public DbSet<Payroll> Payrolls { get; set; }

}
