using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Logging;
using UI.Client.ChuBao.Components;
using UI.Client.ChuBao.Views;

namespace UI.Client.ChuBao.ViewModels
{
    public class MainViewModel : ObservableObject
    {
        private readonly ILogger<MainViewModel> _logger;

        public MainViewModel(ILogger<MainViewModel> logger)
        {
            CurrentView = new DefaultBlankViewComponent();

            NavigateToContactListCommand = new RelayCommand(ExecuteToContactList);
            NavigateToDashboardCommand = new RelayCommand(() =>{
                CurrentView = new DashboardView();
            });
            this._logger = logger;
        }


        #region Implements

        private void ExecuteToContactList()
        {
            _logger.LogInformation("打开联系人列表");
            CurrentView = new ContactView();
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
