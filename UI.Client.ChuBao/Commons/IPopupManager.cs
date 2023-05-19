using System.Windows;
using System.Windows.Controls;

namespace UI.Client.ChuBao.Commons
{
    public interface IPopupManager
    {
        void CreatePopup<TView>() where TView : UserControl;
        void CreatePopup<TView>(Window? owner) where TView : UserControl;
    }
}