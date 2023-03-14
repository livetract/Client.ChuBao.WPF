using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Windows;

namespace UI.Client.ChuBao
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static IHost? AppHost { get; private set; }
        public App()
        {
            var builder = Host.CreateApplicationBuilder();


            var startup = new Startup(builder.Configuration);
            startup.ConfigureServices(builder.Services);

            AppHost= builder.Build();
            

            AppHost.RunAsync();
        }


        protected override async void OnStartup(StartupEventArgs e)
        {
            await AppHost!.StartAsync();
            try
            {
                var start = AppHost!.Services.GetRequiredService<MainWindow>();
                if (start == null)
                {
                    return;
                }
                start.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            base.OnStartup(e);
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            await AppHost!.StopAsync();
            base.OnExit(e);
        }
    }
}
