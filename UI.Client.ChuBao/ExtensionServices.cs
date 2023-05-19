using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Headers;
using System;
using UI.Client.ChuBao.ViewModels;
using UI.Client.ChuBao.Views;
using Microsoft.Extensions.Configuration;
using UI.Client.ChuBao.Commons;
using Access.Client.ChuBao.Services;
using UI.Client.ChuBao.Views.Dialogs;

namespace UI.Client.ChuBao
{
    public static class ExtensionServices
    {
        public static void ConfigureViews(this IServiceCollection services)
        {
            services.AddSingleton<MainWindow>();
            services.AddSingleton<LoginWindow>();
            services.AddSingleton<PopupWindow>();

            services.AddScoped<ContactView>();
            services.AddScoped<DashboardView>();
            services.AddScoped<LinkDetailView>();

            services.AddScoped<AddLinkItemForm>();
            services.AddScoped<EditLinkItemDialog>();
            services.AddScoped<EditLinkMarkDialog>();
        }
        public static void ConfigureViewModels(this IServiceCollection services)
        {
            services.AddSingleton<MainViewModel>();
            services.AddSingleton<LoginViewModel>();

            services.AddScoped<ContactViewModel>();
            services.AddScoped<DashboardViewModel>();
            services.AddScoped<LinkDetailViewModel>();

            services.AddScoped<AddLinkViewModel>();
            services.AddScoped<EditLinkViewModel>();
        }

        public static void ConfigureCustomServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(typeof(MapperProfile));
            services.AddTransient<IPopupManager, PopupManager>();


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
        public static void AddFormFactory<TForm>(this IServiceCollection services)
            where TForm : class
        {
            services.AddTransient<TForm>();
            services.AddSingleton<Func<TForm>>(x => () => x.GetRequiredService<TForm>());
            services.AddSingleton<IAbstractFactory<TForm>, AbstractFactory<TForm>>();
        }
    }
}
