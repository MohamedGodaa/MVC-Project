using Demo.BLL.Interfaces;
using Demo.BLL.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Demo.PL.Extentions
{
    public static class ApplicationServicesExtentions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            return services;
        }
    }
}
