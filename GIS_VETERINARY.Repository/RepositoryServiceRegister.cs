using GIS_VETERINARY.Abstractions.IRepository;
using GIS_VETERINARY.Repository.User;
using Microsoft.Extensions.DependencyInjection;

namespace GIS_VETERINARY.Repository
{
    public static class RepositoryServiceRegister
    {
        public static IServiceCollection AddRepositoryServices(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            return services;
        }
    }
}
