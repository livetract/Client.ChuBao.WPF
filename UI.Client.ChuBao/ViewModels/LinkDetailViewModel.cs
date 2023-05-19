using Access.Client.ChuBao.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Messaging.Messages;
using Core.Client.ChuBao.Dtos;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace UI.Client.ChuBao.ViewModels
{
    public class LinkDetailViewModel : ObservableRecipient
    {
        private readonly ILinkService _linkService;

        public LinkDetailViewModel(ILinkService linkService)
        {
            IsActive = true;
            SubmitNewRecordCommand = new RelayCommand(ExecuteAddNewRecordAsync);
            _linkService = linkService;
        }

        #region Executions


        #endregion
        private async void ExecuteAddNewRecordAsync()
        {
            if (string.IsNullOrEmpty(RecordContent))
                return;

        }

        private async void ExecuteLoadLinkMarkRecord(Guid id)
        {
            var mark = await _linkService.GetMarkAsync(id);
            var rs = await _linkService.GetRecordListAsync(id);
            var records = rs.OrderByDescending(x => x.AddTime).ToList();
            Mark = mark;
            Records = new ObservableCollection<RecordDto>(records);
        }

        #region Messenger

        protected override void OnActivated()
        {
            WeakReferenceMessenger.Default.Register<ValueChangedMessage<LinkDto>, string>(this, "ToLinkDetailView", (r, m) => Link = m.Value);

            base.OnActivated();
        }

        protected override void OnDeactivated()
        {
            WeakReferenceMessenger.Default.UnregisterAll(this, "ToLinkDetailView");
            base.OnDeactivated();
        }


        #endregion

        #region Commands

        public RelayCommand SubmitNewRecordCommand { get; set; }

        #endregion


        #region Notification Properties

        private LinkDto? _link;
        public LinkDto? Link
        {
            get => _link;
            set => SetProperty(ref _link, value);
        }
        private string? _recordContent;
        public string? RecordContent { get => _recordContent; set => SetProperty(ref _recordContent, value); }

        private MarkDto? _mark;
        public MarkDto? Mark { get => _mark; set => SetProperty(ref _mark, value); }

        private ObservableCollection<RecordDto>? _records;
        public ObservableCollection<RecordDto>? Records { get => _records; set => SetProperty(ref _records, value); }

        #endregion

    }
}
