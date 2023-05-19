using Access.Client.ChuBao.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Core.Client.ChuBao.Dtos;

namespace UI.Client.ChuBao.ViewModels
{
    public class AddLinkViewModel : ObservableObject
    {
        private readonly ILinkService _linkService;

        public AddLinkViewModel(ILinkService linkService)
        {
            this._linkService = linkService;
            SubmitNewLinkCommand = new RelayCommand(() => ExecuteSubmitNewLink());
        }

        #region Executions

        #endregion
        private async void ExecuteSubmitNewLink()
        {
            await _linkService.AddLinkAsync(Link);
        }

        #region Commands
        public RelayCommand SubmitNewLinkCommand { get; set; } 

        #endregion

        #region Notification Properties

        private LinkCreateDto? _link;
        public LinkCreateDto? Link { get => _link; set => SetProperty(ref _link, value); }

        #endregion
    }
}
