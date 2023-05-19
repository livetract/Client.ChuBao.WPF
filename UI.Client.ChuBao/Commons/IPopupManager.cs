using System.Windows.Controls;

namespace UI.Client.ChuBao.Commons
{
    public interface IPopupManager
    {
        void CreatePopup<TView>() where TView : UserControl;
    }
}