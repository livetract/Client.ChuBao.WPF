using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows.Controls;
using UI.Client.ChuBao.ViewModels;

namespace UI.Client.ChuBao.Views
{
    /// <summary>
    /// Interaction logic for LinkDetailComponent.xaml
    /// </summary>
    public partial class LinkDetailView : UserControl
    {
        public LinkDetailView()
        {
            this.DataContext = App.AppHost!.Services.GetRequiredService<LinkDetailViewModel>();
            InitializeComponent();
        }
    }
}
