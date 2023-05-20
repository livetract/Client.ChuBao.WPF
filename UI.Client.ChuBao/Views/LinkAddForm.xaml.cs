using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows.Controls;
using UI.Client.ChuBao.ViewModels;

namespace UI.Client.ChuBao.Views
{
    /// <summary>
    /// Interaction logic for AddLinkItemDialog.xaml
    /// </summary>
    public partial class LinkAddForm : UserControl
    {
        public LinkAddForm()
        {
            this.DataContext = App.AppHost!.Services.GetRequiredService<LinkAddViewModel>();
            //this.DataContext = App.AppHost!.Services.GetRequiredService<LinkmanViewModel>();
            InitializeComponent();
        }
    }
}
