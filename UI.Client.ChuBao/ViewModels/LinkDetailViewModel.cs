﻿using Access.Client.ChuBao.Services;
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
            this.IsActive = true;

            Link = new LinkDto();
            Mark = new MarkDto();
            Records = new ObservableCollection<RecordDto>();

            SubmitNewRecordCommand = new RelayCommand(ExecuteAddNewRecordAsync);

            this._linkService = linkService;

        }

        private async void ExecuteAddNewRecordAsync()
        {
            if (string.IsNullOrEmpty(RecordContent)) return;
            var record = new RecordCreateDto
            {
                ContactId = Link.Id,
                Content = RecordContent,
                Booker = "hyd"
            };
            var result = await _linkService.AddLinkRecordAsync(record);
            if (result)
            {
                var rs = await _linkService.GetRecordListAsync(Link.Id);
                var records = rs.OrderByDescending(x => x.AddTime);
                Records = new ObservableCollection<RecordDto>(records);
                RecordContent = string.Empty;
            }
        }

        private async void ExecuteLoadLinkMarkRecord(Guid id)
        {
            var mark = await _linkService.GetMarkAsync(id);
            var rs = await _linkService.GetRecordListAsync(id);
            var records = rs.OrderByDescending(x => x.AddTime).ToList();
            Mark = mark;
            Records = new ObservableCollection<RecordDto>(records);
        }

        protected override void OnActivated()
        {
            WeakReferenceMessenger.Default.Register<LinkDetailViewModel, ValueChangedMessage<LinkDto>>(this, (r, m) => r.Link = m.Value);

            base.OnActivated();
        }

        protected override void OnDeactivated()
        {
            base.OnDeactivated();
        }



        public RelayCommand SubmitNewRecordCommand { get; set; }



        private LinkDto? _link;
        public LinkDto? Link
        {
            get => _link;
            set
            {
                if (SetProperty(ref _link, value))
                {
                    if (Link.Id != Guid.Empty)
                    {
                        ExecuteLoadLinkMarkRecord(Link.Id);
                    }
                }
            }
        }
        private string? _recordContent;
        public string? RecordContent { get => _recordContent; set => SetProperty(ref _recordContent, value); }

        private MarkDto? _mark;
        public MarkDto? Mark { get => _mark; set => SetProperty(ref _mark, value); }

        private ObservableCollection<RecordDto>? _records;
        public ObservableCollection<RecordDto>? Records { get => _records; set => SetProperty(ref _records, value); }

    }
}