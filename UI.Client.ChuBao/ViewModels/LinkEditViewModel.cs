using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Messaging.Messages;
using Data.Client.ChuBao.DTOS;
using Data.Client.ChuBao.Services;
using System;

namespace UI.Client.ChuBao.ViewModels
{
    public class LinkEditViewModel : ObservableRecipient
    {
        private readonly ILinkLocalService _linkService;

        public LinkEditViewModel(ILinkLocalService linkService)
        {
            this.IsActive = true;
            this._linkService = linkService;
            SubmitEditedCommand = new RelayCommand(ExecuteSubmitedited);
            EditLink = new LinkDto();
        }


        #region Executions

        private void ExecuteSubmitedited()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Commands

        public RelayCommand SubmitEditedCommand { get; set; }

        #endregion


        #region Notification Properties

        private LinkDto? _editLink;
        public LinkDto? EditLink 
        { 
            get => _editLink;
            set => SetProperty(ref _editLink, value);
        }

        #endregion

        #region Messenger
        protected override void OnActivated()
        {
            WeakReferenceMessenger.Default.Register<ValueChangedMessage<LinkDto>,string>(this, "ToLinkEditForm", (r, m) => EditLink = m.Value);
            base.OnActivated();
        }

        protected override void OnDeactivated()
        {
            WeakReferenceMessenger.Default.UnregisterAll<string>(this, "ToLinkEditForm");
            base.OnDeactivated();
        }

        #endregion

    }
}
