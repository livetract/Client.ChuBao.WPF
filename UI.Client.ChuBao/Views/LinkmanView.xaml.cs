using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows.Controls;
using UI.Client.ChuBao.ViewModels;

namespace UI.Client.ChuBao.Views
{
    /// <summary>
    /// Interaction logic for LinkmanView.xaml
    /// </summary>
    public partial class LinkmanView : UserControl
    {
        public LinkmanView()
        {
            this.DataContext = App.AppHost!.Services.GetRequiredService<LinkmanViewModel>();
            InitializeComponent();
        }
    }
}
