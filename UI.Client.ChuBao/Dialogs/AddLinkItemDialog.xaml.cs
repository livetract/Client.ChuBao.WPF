using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows.Controls;
using UI.Client.ChuBao.ViewModels;

namespace UI.Client.ChuBao.Dialogs
{
    /// <summary>
    /// Interaction logic for AddLinkItemDialog.xaml
    /// </summary>
    public partial class AddLinkItemDialog : UserControl
    {
        public AddLinkItemDialog()
        {
            this.DataContext = App.AppHost!.Services.GetRequiredService<ContactViewModel>();
            InitializeComponent();
        }
    }
}
