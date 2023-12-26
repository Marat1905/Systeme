using AppWpf.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace AppWpf.Services
{
    static class ViewModelRegistrator
    {
        public static IServiceCollection AddViewModels(this IServiceCollection services) => services
            .AddSingleton<MainWindowViewModel>()
            .AddTransient<SecondWindowViewModel>()
            ;
    }
}
