using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows.Controls;
using UI.Client.ChuBao.ViewModels;

namespace UI.Client.ChuBao.Views
{
    /// <summary>
    /// Interaction logic for EditLinkItemDialog.xaml
    /// </summary>
    public partial class LinkEditForm : UserControl
    {
        public LinkEditForm()
        {
            this.DataContext = App.AppHost!.Services.GetRequiredService<LinkEditViewModel>();
            InitializeComponent();
        }
    }
}
