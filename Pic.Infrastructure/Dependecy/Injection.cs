using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Pic.Infrastructure;

public static class Injection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
            services.AddScoped<ISendEmailService, SendEmailService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IValueRepository, ValueRepository>();
            services.AddScoped<IConsultService, ConsultService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddDbContext<Context>(opt =>
            opt.UseMySql(configuration.GetConnectionString("DefaultConnection"),
                new MySqlServerVersion(new Version(8, 0, 3))));
            
            services.AddScoped<PublishDomainEventInterceptor>();
            return services;
    }
}
