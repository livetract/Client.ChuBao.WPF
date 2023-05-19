using Access.Client.ChuBao.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Messaging.Messages;
using Core.Client.ChuBao.Dtos;
using System;

namespace UI.Client.ChuBao.ViewModels
{
    public class EditLinkViewModel : ObservableRecipient
    {
        private readonly ILinkService _linkService;

        public EditLinkViewModel(ILinkService linkService)
        {
            this.IsActive = true;
            this._linkService = linkService;
            SubmitEditedCommand = new RelayCommand(ExecuteSubmitedited);
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

        private LinkDto? _link;
        public LinkDto? Link 
        { 
            get => _link;
            set => SetProperty(ref _link, value);
        }

        #endregion

        #region Messenger
        protected override void OnActivated()
        {
            WeakReferenceMessenger.Default.Register<ValueChangedMessage<LinkDto>,string>(this,"ToEditLinkForm", (r, m) => Link = m.Value);
            base.OnActivated();
        }

        protected override void OnDeactivated()
        {
            WeakReferenceMessenger.Default.UnregisterAll<string>(this, "ToEditLinkForm");
            base.OnDeactivated();
        }

        #endregion

    }
}
