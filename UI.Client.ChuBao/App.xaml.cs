using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Windows;
using Serilog;
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
        public App()
        {
            var builder = Host.CreateDefaultBuilder();
            builder.UseSerilog((host,loggerConfiguration) =>
            {
                loggerConfiguration
                .WriteTo
                .File(path: "Logs/log", rollingInterval: RollingInterval.Day);
            });
            builder.ConfigureServices(ConfigureServices);
            builder.ConfigureAppConfiguration(ConfigureAppConfiguration);


            AppHost= builder.Build();
            

            AppHost.RunAsync();
        }

        private void ConfigureAppConfiguration(HostBuilderContext context, IConfigurationBuilder builder)
        {
            var env = context.HostingEnvironment;

            builder.Sources.Clear();

            builder.SetBasePath(Directory.GetCurrentDirectory());
            builder
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true, true);
            builder.AddEnvironmentVariables();

            builder.Build();
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
