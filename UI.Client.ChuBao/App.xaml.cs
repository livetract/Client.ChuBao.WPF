using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Windows;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace UI.Client.ChuBao
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static IHost? AppHost { get; private set; }
        public static string AccessToken { get; set; } = string.Empty;
        public App()
        {
            var builder = Host.CreateDefaultBuilder()
                .ConfigureAppConfiguration(ConfigureAppConfiguration)
                .ConfigureServices(ConfigureServices);

            AppHost= builder.Build();
            AppHost.RunAsync();
        }

        private void ConfigureAppConfiguration(HostBuilderContext context, IConfigurationBuilder config)
        {
            var env = context.HostingEnvironment;

            config.Sources.Clear();

            config.SetBasePath(Directory.GetCurrentDirectory());
            config
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true, true);
            config.AddEnvironmentVariables();

            config.Build();
        }

        private void ConfigureServices(HostBuilderContext context, IServiceCollection services)
        {
            services.ConfigureViews();
            services.ConfigureViewModels();
            services.ConfigureCustomServices(context.Configuration);
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            await AppHost!.StartAsync();
            try
            {
                //var start = AppHost!.Services.GetRequiredService<LoginWindow>();
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
