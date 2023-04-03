using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using UI.Client.ChuBao.ViewModels;

namespace UI.Client.ChuBao
{
    /// <summary>
    /// Interaction logic for LoginDialog.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            this.DataContext = App.AppHost!.Services.GetRequiredService<LoginViewModel>();
            InitializeComponent();
        }
    }
}
