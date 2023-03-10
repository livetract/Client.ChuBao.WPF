using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using UI.Client.ChuBao.Views;

namespace UI.Client.ChuBao.ViewModels
{
    public class MainViewModel : ObservableObject
    {
        public MainViewModel()
        {
            CurrentView = null;
            NavigateToContactListCommand = new RelayCommand(ExecuteToContactList);
        }


        #region Implements

        private void ExecuteToContactList()
        {

            CurrentView = App.AppHost!.Services.GetRequiredService<ContactView>();
        }

        #endregion


        #region Commands

        //public RelayCommand SwitchPageCommand { get; set; }
        public RelayCommand NavigateToDashboardCommand { get; set; }
        public RelayCommand NavigateToContactListCommand { get; set; }

        #endregion

        #region ObservableObject

        private object _currentView;

        public object CurrentView
        {
            get => _currentView;
            set => SetProperty(ref _currentView, value);
        }



        #endregion

    }
}
