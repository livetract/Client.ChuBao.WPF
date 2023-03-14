using System.Windows;

namespace UI.Client.ChuBao.Commons
{
    public interface IDialogHandler
    {
        void ShowDialog();
        void ShowDialog(string name);
        void CreateDialog<TView>();
        void CreateDialog<TView>(string Title, Window window);
    }
}
