using Microsoft.Extensions.DependencyInjection;
using Systeme.CarDriver.Interfaces;

namespace Systeme.CarDriver
{
    public static class CarDriverServices
    {
        public static IServiceCollection AddCarDriverService(this IServiceCollection services) => services
        .AddTransient<ICarDriverTask, CarDriverTask>()
         ;
    }
}
