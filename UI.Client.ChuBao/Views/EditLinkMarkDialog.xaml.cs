using Microsoft.Extensions.DependencyInjection;
using System.Windows.Controls;
using UI.Client.ChuBao.ViewModels;

namespace UI.Client.ChuBao.Views.Dialogs
{
    /// <summary>
    /// Interaction logic for EditLinkMarkDialog.xaml
    /// </summary>
    public partial class EditLinkMarkDialog : UserControl
    {
        public EditLinkMarkDialog()
        {

            this.DataContext = App.AppHost!.Services.GetRequiredService<ContactViewModel>();
            InitializeComponent();
        }
    }
}
