using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows.Controls;
using UI.Client.ChuBao.ViewModels;

namespace UI.Client.ChuBao.Components
{
    /// <summary>
    /// Interaction logic for LinkDetailComponent.xaml
    /// </summary>
    public partial class LinkDetailComponent : UserControl
    {
        public LinkDetailComponent()
        {
            this.DataContext = App.AppHost!.Services.GetRequiredService<ContactViewModel>();
            InitializeComponent();
        }
    }
}
