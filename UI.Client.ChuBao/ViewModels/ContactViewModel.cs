using Access.Client.ChuBao.Services;
using AutoMapper;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Messaging.Messages;
using Core.Client.ChuBao.Dtos;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.ObjectModel;
using UI.Client.ChuBao.Commons;
using UI.Client.ChuBao.Views;
using UI.Client.ChuBao.Views.Dialogs;

namespace UI.Client.ChuBao.ViewModels
{
    public class ContactViewModel : ObservableRecipient
    {
        private readonly ILinkService _linkService;
        private readonly IPopupManager _popupManager;
        private readonly IMapper _mapper;

        public ContactViewModel(
            ILinkService linkService,
            IPopupManager popupManager,
            IMapper mapper
            )
        {
            this._linkService = linkService;
            this._popupManager = popupManager;
            this._mapper = mapper;


            ExecuteLoadLinkListAsync();

            PopUpAddLinkCommand = new RelayCommand(()=>_popupManager.CreatePopup<AddLinkItemForm>());
            PopUpEditLinkCommand = new RelayCommand<LinkListDto>(dto => ExecutePopUpEditLinkDialog(dto));
            //PopUpEditLinkMarkCommand = new RelayCommand<MarkDto>(_popupManager.CreatePopup<e>);


            DisplayLinkDetailViewCommand = new RelayCommand<LinkListDto>(model => ExecuteDisplayLinkDetailView(model));


        }



        #region Executions


        private async void ExecuteLoadLinkListAsync()
        {
            var items = await _linkService.LoadLinkListAsync();
            LinkList = new ObservableCollection<LinkListDto>(items);

        }
        private void ExecuteDisplayLinkDetailView(LinkListDto? model)
        {
            if (model == null)
            {
                return;
            }
            var link = _mapper.Map<LinkDto>(model);
            LinkDetailView = App.AppHost!.Services.GetRequiredService<Views.LinkDetailView>();
            WeakReferenceMessenger.Default.Send(new ValueChangedMessage<LinkDto>(link), "ToLinkDetailView");
        }

        private void ExecutePopUpEditLinkDialog(LinkListDto? dto)
        {
            if (dto is null)
            {
                throw new ArgumentNullException(nameof(dto));
            }

            var link = _mapper.Map<LinkDto> (dto);
            WeakReferenceMessenger.Default.Send(new ValueChangedMessage<LinkDto>(link), "ToLinkEditForm");
            _popupManager.CreatePopup<EditLinkItemDialog>();
        }

        #endregion

        #region Commands

        public RelayCommand<LinkListDto> DisplayLinkDetailViewCommand { get; set; }

        public RelayCommand PopUpAddLinkCommand { get; set; }
        public RelayCommand<LinkListDto> PopUpEditLinkCommand { get; set; }
        public RelayCommand<string> CheckLinkMarkCommand { get; set; }
        public RelayCommand<MarkDto> PopUpEditLinkMarkCommand { get; set; }

        #endregion


        #region Notification Properties

        private ObservableCollection<LinkListDto>? _linkList;

        public ObservableCollection<LinkListDto>? LinkList { get => _linkList; set => SetProperty(ref _linkList, value); }

        private object? _linkDetaiView;

        public object? LinkDetailView { get => _linkDetaiView; set => SetProperty(ref _linkDetaiView, value); }

        #endregion
    }
}
