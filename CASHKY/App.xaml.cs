using CASHKY.LableSystem;
using Microsoft.Extensions.DependencyInjection;
using System.Configuration;
using System.Data;
using System.Windows;

namespace CASHKY
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private IServiceProvider serviceProvider;
        public App()
        {
            ServiceCollection serviceDescriptors = new();
            serviceDescriptors.AddScoped<MainWindow>();
            serviceDescriptors.AddScoped<MainWindowViewModel>();
            serviceDescriptors.AddLableServices();
            serviceProvider = serviceDescriptors.BuildServiceProvider();
            this.Startup += App_Startup;
        }

        private void App_Startup(object sender, StartupEventArgs e)
        {
            this.serviceProvider.GetService<MainWindow>()?.Show();
        }
    }

}
