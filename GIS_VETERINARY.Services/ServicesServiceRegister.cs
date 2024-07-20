using GIS_VETERINARY.Abstractions.IRepository;
using GIS_VETERINARY.Abstractions.IServices;
using GIS_VETERINARY.Services.User;
using Microsoft.Extensions.DependencyInjection;

namespace GIS_VETERINARY.Services
{
    public static class ServicesServiceRegister
    {
        public static IServiceCollection AddServiceServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            return services;
        }
    }
}
