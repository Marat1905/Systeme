using Microsoft.Extensions.DependencyInjection;
using Systeme.DAL.Interfaces;
using Systeme.DAL.Repositories;
using Systeme.Domain.Entityes;

namespace Systeme.DAL.Services
{
    public static class RepositoryRegistrator
    {
        public static IServiceCollection AddRepositoriesInDB(this IServiceCollection services) => services
           .AddTransient<IRepository<Car>, DBRepository<Car>>()
           .AddTransient<IRepository<Driver>, DBRepository<Driver>>()
           ;

    }
}
