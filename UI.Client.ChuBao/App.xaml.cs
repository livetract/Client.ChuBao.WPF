using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Windows;
using Microsoft.Extensions.Configuration;
using System.IO;
using Data.Client.ChuBao.Contexts;
using Microsoft.EntityFrameworkCore;

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
            /*
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true)
                .Build();
            */
            AppHost = Host.CreateDefaultBuilder()
                .ConfigureServices(ConfigureServices)
                .Build();

            AppHost.RunAsync();
        }

        private void ConfigureServices(HostBuilderContext context, IServiceCollection services)
        {
            services.ConfigureViews();
            services.ConfigureViewModels();
            services.ConfigureCustomServices(context.Configuration);

            services.AddDbContext<AppdbContext>(options =>
                options.UseSqlServer(context.Configuration.GetConnectionString("AppDb"),
                b =>
                {
                    b.EnableRetryOnFailure(
                    maxRetryCount: 5,
                    maxRetryDelay: TimeSpan.FromSeconds(30),
                    errorNumbersToAdd: new int[] { 2 });
                    b.MigrationsAssembly(this.GetType().Assembly.FullName);
                }));
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            await AppHost!.StartAsync();

            //var start = AppHost!.Services.GetRequiredService<LoginWindow>();
            var start = AppHost!.Services.GetRequiredService<MainWindow>();
            start.Show();

            base.OnStartup(e);
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            await AppHost!.StopAsync();
            base.OnExit(e);
        }
    }
}
