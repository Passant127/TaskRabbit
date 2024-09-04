
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Payroll.Contracts;
using Payroll.Data;
using Payroll.Services;
using Payrolls.Consumers;

namespace Payrolls;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);


        builder.Services.AddDbContext<PayrollContext>(options =>
               options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

        builder.Services.AddScoped<IPayrollService, PayrollService>();

        // Configure MassTransit with RabbitMQ
        builder.Services.AddMassTransit(x =>
        {
            x.AddConsumer<EmployeeAddedConsumer>();
            x.AddConsumer<EmployeeSalaryUpdatedConsumer>();

            x.UsingRabbitMq((context, cfg) =>
            {
             
                    cfg.Host("localhost", "/", h =>
                    {
                        h.Username("guest");
                        h.Password("guest");
                    });


                    cfg.ReceiveEndpoint("employee-added-queue", e =>
                    {
                        e.ConfigureConsumer<EmployeeAddedConsumer>(context);
                    });

                    cfg.ReceiveEndpoint("employee-salary-updated-queue", e =>
                    {
                        e.ConfigureConsumer<EmployeeSalaryUpdatedConsumer>(context);
                    });
                
              
            });
        });

        builder.Services.AddMassTransitHostedService();

        // Configure Swagger/OpenAPI
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddControllers();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();

        app.Run();
    }
}
