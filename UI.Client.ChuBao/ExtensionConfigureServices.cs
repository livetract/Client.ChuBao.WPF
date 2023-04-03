using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Headers;
using System;
using UI.Client.ChuBao.Components;
using UI.Client.ChuBao.Dialogs;
using UI.Client.ChuBao.ViewModels;
using UI.Client.ChuBao.Views;
using Microsoft.Extensions.Configuration;
using UI.Client.ChuBao.Commons;
using Access.Client.ChuBao.Services;

namespace UI.Client.ChuBao
{
    public static class ExtensionConfigureServices
    {
        public static void ConfigureViewModels(this IServiceCollection services)
        {
            services.AddTransient<MainViewModel>();
            services.AddScoped<DialogWindow>();

            services.AddScoped<ContactViewModel>();
            services.AddScoped<DashboardViewModel>();
            services.AddScoped<LinkDetailViewModel>();
            services.AddScoped<LoginViewModel>();
        }

        public static void ConfigureViews(this IServiceCollection services)
        {
            services.AddTransient<MainWindow>();
            services.AddScoped<ContactView>();
            services.AddScoped<DashboardView>();

            services.AddScoped<DefaultBlankViewComponent>();
            services.AddScoped<LinkDetailComponent>();

            services.AddScoped<AddLinkItemDialog>();
            services.AddScoped<EditLinkItemDialog>();
            services.AddScoped<EditLinkMarkDialog>();
            services.AddScoped<LoginWindow>();
        }

        public static void ConfigureCustomServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IDialogHandler,DialogHandler>();
            services.AddAutoMapper(typeof(MapperProfile));


            services.AddHttpClient<ILinkService, LinkService>(
                http =>
                {
                    http.BaseAddress = new Uri(configuration.GetSection("Endpoints:Contact").Value ?? "");
                    http.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    http.DefaultRequestHeaders.UserAgent.TryParseAdd("wpf-client-chubao");
                    //http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(scheme:"Bearer", parameter: App.AccessToken);
                    http.DefaultRequestHeaders.Add("Authorization", $"Bearer {App.AccessToken}");
                });

            services.AddHttpClient<IAuthService, AuthService>(
                http =>
                {
                    http.BaseAddress = new Uri(configuration.GetSection("Endpoints:Account").Value ?? "");
                    http.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    http.DefaultRequestHeaders.UserAgent.TryParseAdd("wpf-client-chubao");
                }
                );
        }
    }
}
