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
using UI.Client.ChuBao.Components;
using UI.Client.ChuBao.Dialogs;

namespace UI.Client.ChuBao.ViewModels
{
    public class ContactViewModel : ObservableRecipient
    {
        private readonly ILinkService _linkService;
        private readonly IDialogHandler _dialogHandler;
        private readonly IMapper _mapper;

        public ContactViewModel(
            ILinkService linkService,
            IDialogHandler dialogHandler,
            IMapper mapper
            )
        {
            this._linkService = linkService;
            this._dialogHandler = dialogHandler;
            this._mapper = mapper;

            NewLink = new LinkCreateDto();
            EditLink = new LinkDto();

            this.LinkDetailView =  App.AppHost!.Services.GetRequiredService<DefaultBlankViewComponent>();

            ExecuteLoadLinkListAsync();

            PopUpAddLinkCommand = new RelayCommand<string>(title =>
            _dialogHandler.CreateDialog<AddLinkItemDialog>("新增联系人", App.Current.MainWindow));

            PopUpEditLinkCommand = new RelayCommand<LinkListDto>(model => ExecuteCreateEditLinkDialog(model));
            PopUpEditLinkMarkCommand = new RelayCommand<MarkDto>(title =>
            _dialogHandler.CreateDialog<EditLinkMarkDialog>($"标签管理", App.Current.MainWindow));


            SubmitNewLinkItemCommand = new RelayCommand(ExcuteSubmitNewLinkItemAsync);
            SubmitEditLinkItemCommand = new RelayCommand(ExcuteSubmitEditLinkItemAsync);

            ShowLinkDetailViewCommand = new RelayCommand<LinkListDto>(model => ExecuteShowLinkDetailView(model));


        }



        #region Implements

        private void ExecuteShowLinkDetailView(LinkListDto? model)
        {
            if (model == null)
            {
                return;
            }
            var link = _mapper.Map<LinkDto>(model);
            LinkDetailView = App.AppHost!.Services.GetRequiredService<LinkDetailComponent>();
            WeakReferenceMessenger.Default.Send(new ValueChangedMessage<LinkDto>(link));
        }

        private void ExecuteCreateEditLinkDialog(LinkListDto? model)
        {
            if (model.Id == Guid.Empty)
            {
                return;
            }
            EditLink = _mapper.Map<LinkDto>(model);

            _dialogHandler.CreateDialog<EditLinkItemDialog>($"修改信息", App.Current.MainWindow);
        }

        private async void ExcuteSubmitEditLinkItemAsync()
        {
            var result = await _linkService.ModifyLinkAsync(EditLink);
            if (result)
            {
                EditLink = null;

                ExecuteLoadLinkListAsync();
            }
        }

        private async void ExcuteSubmitNewLinkItemAsync()
        {
            if (NewLink.Name == null)
                return;
            var result = await _linkService.AddLinkAsync(NewLink);
            if (result)
            {
                NewLink = null;
                ExecuteLoadLinkListAsync();
            }
        }
        private async void ExecuteLoadLinkListAsync()
        {
            var items = await _linkService.LoadLinkListAsync();
            LinkList = new ObservableCollection<LinkListDto>(items);

            LinkDetailView = App.AppHost!.Services.GetRequiredService<DefaultBlankViewComponent>();
        }

        #endregion

        #region Commands
        public RelayCommand SubmitNewLinkRecordCommand { get; set; }
        public RelayCommand<LinkListDto> ShowLinkDetailViewCommand { get; set; }

        public RelayCommand<string> PopUpAddLinkCommand { get; set; }
        public RelayCommand<LinkListDto> PopUpEditLinkCommand { get; set; }
        public RelayCommand SubmitNewLinkItemCommand { get; set; }
        public RelayCommand SubmitEditLinkItemCommand { get; set; }
        public RelayCommand<string> CheckLinkMarkCommand { get; set; }
        public RelayCommand<MarkDto> PopUpEditLinkMarkCommand { get; set; }

        #endregion


        #region ObservableObject

        private ObservableCollection<LinkListDto>? _linkList;

        public ObservableCollection<LinkListDto>? LinkList { get => _linkList; set => SetProperty(ref _linkList, value); }

        private LinkCreateDto? _newLink;
        public LinkCreateDto? NewLink { get => _newLink; set=> SetProperty(ref _newLink, value); }

        private LinkDto? _editLink;
        public LinkDto? EditLink { get => _editLink; set => SetProperty(ref _editLink, value); }

        private object? _linkDetaiView;

        public object? LinkDetailView { get => _linkDetaiView; set => SetProperty(ref _linkDetaiView, value); }




        #endregion
    }
}
