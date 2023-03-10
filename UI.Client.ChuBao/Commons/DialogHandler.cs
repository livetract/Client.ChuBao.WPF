using System;

namespace UI.Client.ChuBao.Commons
{
    public class DialogHandler : IDialogHandler
    {
        public DialogHandler()
        {

        }
        public void ShowDialog()
        {
            var dialog = new DialogWindow();


            dialog.ShowDialog();
        }

        public void ShowDialog(string name)
        {
            var dialog = new DialogWindow();

            var type = Type.GetType($"UI.Client.ChuBao.{name}");

            dialog.Content = Activator.CreateInstance(type);

            dialog.ShowDialog();
        }

        public void CreateDialog<TView>()
        {
            var dialog = new DialogWindow();

            var content = Activator.CreateInstance(typeof(TView));

            dialog.Content = content;

            dialog.ShowDialog();
        }

        public void CreateDialog<TView>(string Title)
        {
            var dialog = new DialogWindow();
            var content = Activator.CreateInstance(typeof(TView));
            dialog.Content = content;
            dialog.Title = Title;
            dialog.ShowDialog();
        }
    }
}
