using Microsoft.Extensions.DependencyInjection;

namespace AppWpf.ViewModels
{
    internal class ViewModelLocator
    {
        public MainWindowViewModel MainWindowModel => App.Services.GetRequiredService<MainWindowViewModel>();
        public SecondWindowViewModel SecondWindowModel => App.Host.Services.GetRequiredService<SecondWindowViewModel>();

        public ViewModelLocator()
        {
            
        }
    }
}
