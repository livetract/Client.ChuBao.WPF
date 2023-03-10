using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows.Controls;
using UI.Client.ChuBao.ViewModels;

namespace UI.Client.ChuBao.Dialogs
{
    /// <summary>
    /// Interaction logic for EditLinkItemDialog.xaml
    /// </summary>
    public partial class EditLinkItemDialog : UserControl
    {
        public EditLinkItemDialog()
        {
            this.DataContext = App.AppHost!.Services.GetRequiredService<ContactViewModel>();
            InitializeComponent();
        }
    }
}
