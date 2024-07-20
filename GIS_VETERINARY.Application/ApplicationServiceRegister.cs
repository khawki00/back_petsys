using GIS_VETERINARY.Abstractions.IApplication;
using GIS_VETERINARY.Application.User;
using Microsoft.Extensions.DependencyInjection;

namespace GIS_VETERINARY.Application
{
    public static class ApplicationServiceRegister
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IUserApplication, UserApplication>();
            return services;
        }

    }

}
