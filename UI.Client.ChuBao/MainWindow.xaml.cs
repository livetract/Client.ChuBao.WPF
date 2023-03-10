using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using System.Windows.Input;
using UI.Client.ChuBao.ViewModels;

namespace UI.Client.ChuBao
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow(IConfiguration config)
        {
            var result = config
                .GetSection("Introduce")
                .GetSection("Title")
                .Value;
            this.Title = result;

            this.DataContext = App.AppHost!.Services.GetRequiredService<MainViewModel>();

            InitializeComponent();
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }
    }
}
