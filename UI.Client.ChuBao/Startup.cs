using Access.Client.ChuBao;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;
using System.Net.Http.Headers;
using UI.Client.ChuBao.Commons;
using UI.Client.ChuBao.Components;
using UI.Client.ChuBao.Dialogs;
using UI.Client.ChuBao.ViewModels;
using UI.Client.ChuBao.Views;

namespace UI.Client.ChuBao
{
    public class Startup
    {

        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<MainWindow> ();
            services.AddTransient<MainViewModel> ();
            services.AddScoped<DialogWindow>();
            services.AddScoped<IDialogHandler,DialogHandler>();

            services.AddScoped<ContactView>();
            services.AddScoped<ContactViewModel>();
            services.AddScoped<DashboardView>();

            services.AddScoped<AddLinkItemDialog>();
            services.AddScoped<EditLinkItemDialog>();
            services.AddScoped<LinkDetailComponent>();
            services.AddScoped<DefaultBlankViewComponent>();

            //var contactEndpoint = Configuration.GetSection("Endpoints:Contact").Value;

            services.AddHttpClient<ILinkService,LinkService>(
                http =>
                {
                    http.BaseAddress = new Uri(Configuration.GetSection("Endpoints:Contact").Value ?? "");
                    http.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue ("application/json"));
                    http.DefaultRequestHeaders.UserAgent.TryParseAdd("wpf-client-chubao");
                });
        }

        public void ConfigureApplication(IHostEnvironment env, IConfigurationBuilder builder)
        {
            builder.Sources.Clear();

            builder.SetBasePath(Directory.GetCurrentDirectory());
            builder
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true, true);
            builder.AddEnvironmentVariables();

            builder.Build();
        }
    }
}