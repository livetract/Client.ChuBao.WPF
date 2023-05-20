using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Messaging.Messages;
using Data.Client.ChuBao.DTOS;
using Data.Client.ChuBao.Services;
using System;

namespace UI.Client.ChuBao.ViewModels
{
    public class LinkAddViewModel : ObservableObject
    {
        private readonly ILinkLocalService _linkService;

        public LinkAddViewModel(ILinkLocalService linkService)
        {
            this._linkService = linkService;
            SubmitNewLinkCommand = new RelayCommand(() => ExecuteSubmitNewLink());
            NewLink = new LinkNewDto();
        }

        #region Executions

        #endregion
        private async void ExecuteSubmitNewLink()
        {
            if (NewLink == null)
            {
                throw new ArgumentNullException(nameof(NewLink));
            }
            var result = await _linkService.AddAsync(NewLink);
            if (result > 0)
            {
                WeakReferenceMessenger.Default.Send(new ValueChangedMessage<bool>(true), "ToRefreshLinkmanView");
            }
        }

        #region Commands
        public RelayCommand SubmitNewLinkCommand { get; set; } 

        #endregion

        #region Notification Properties

        private LinkNewDto? _newLink;
        public LinkNewDto? NewLink { get => _newLink; set => SetProperty(ref _newLink, value); }

        #endregion
    }
}
