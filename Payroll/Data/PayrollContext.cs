using Microsoft.EntityFrameworkCore;
using Payrolls.Models;

namespace Payroll.Data;

public class PayrollContext : DbContext
{
    public PayrollContext(DbContextOptions dbContext) : base(dbContext)
    {

    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PayrollModel>()
            .Property(p => p.Salary)
            .HasPrecision(18, 2);
    }


    public DbSet<PayrollModel> Payrolls { get; set; }

}
