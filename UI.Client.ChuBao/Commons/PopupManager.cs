using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows.Controls;

namespace UI.Client.ChuBao.Commons
{
    public class PopupManager : IPopupManager
    {
        public void CreatePopup<TView>()
            where TView : UserControl
        {
            var pop = App.AppHost!.Services.GetRequiredService<PopupWindow>();
            var content = Activator.CreateInstance(typeof(TView));
            pop.Content = content;
            pop.ShowDialog();
        }
    }
}
