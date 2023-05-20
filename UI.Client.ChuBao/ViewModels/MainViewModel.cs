using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using UI.Client.ChuBao.Views;

namespace UI.Client.ChuBao.ViewModels
{
    public class MainViewModel : ObservableObject
    {

        public MainViewModel()
        {
            NavigateToContactListCommand = new RelayCommand(ExecuteToContactList);
            NavigateToDashboardCommand = new RelayCommand(() =>{
                CurrentView = new DashboardView();
            });
        }


        #region Implements

        private void ExecuteToContactList()
        {
            CurrentView = App.AppHost!.Services.GetRequiredService<LinkmanView>();
        }

        #endregion


        #region Commands

        public RelayCommand NavigateToDashboardCommand { get; set; }
        public RelayCommand NavigateToContactListCommand { get; set; }

        #endregion

        #region ObservableObject

        private object? _currentView;
        public object? CurrentView { get => _currentView; set => SetProperty(ref _currentView, value); }



        #endregion

    }
}
