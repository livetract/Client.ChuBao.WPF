using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows.Controls;
using UI.Client.ChuBao.ViewModels;

namespace UI.Client.ChuBao.Views
{
    /// <summary>
    /// Interaction logic for EditLinkItemDialog.xaml
    /// </summary>
    public partial class EditLinkForm : UserControl
    {
        public EditLinkForm()
        {
            this.DataContext = App.AppHost!.Services.GetRequiredService<EditLinkViewModel>();
            InitializeComponent();
        }
    }
}
