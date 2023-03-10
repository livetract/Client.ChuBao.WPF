using Access.Client.ChuBao;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Core.Client.ChuBao.Dtos;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.ObjectModel;
using System.Windows.Forms;
using UI.Client.ChuBao.Commons;
using UI.Client.ChuBao.Components;
using UI.Client.ChuBao.Dialogs;

namespace UI.Client.ChuBao.ViewModels
{
    public class ContactViewModel : ObservableObject
    {
        private readonly ILinkService _linkService;
        private readonly IDialogHandler _dialogHandler;

        public ContactViewModel(
            ILinkService linkService,
            IDialogHandler dialogHandler
            )
        {
            this._linkService = linkService;
            this._dialogHandler = dialogHandler;
            this.LinkNewDto = new LinkCreateDto();
            this.LinkItem = new LinkDto();
            this.LinkDetailView = App.AppHost!.Services.GetRequiredService<DefaultBlankViewComponent>();

            ExecuteLoadLinkListAsync();

            PopUpAddLinkCommand = new RelayCommand<string>(title =>
            _dialogHandler.CreateDialog<AddLinkItemDialog>("新增联系人"));
            PopUpEditLinkCommand = new RelayCommand<LinkDto>(l => ExecuteCreateEditLinkDialog(l));


            SubmitNewLinkItemCommand = new RelayCommand(ExcuteSubmitNewLinkItemAsync);
            SubmitEditLinkItemCommand = new RelayCommand(ExcuteSubmitEditLinkItemAsync);

            ShowLinkDetailViewCommand = new RelayCommand<LinkDto>(model => ExecuteShowLinkDetailView(model));
        }

        private void ExecuteShowLinkDetailView(LinkDto? model)
        {
            LinkItem = model!;
            var view = App.AppHost!.Services.GetRequiredService<LinkDetailComponent>();
            LinkDetailView = view;
        }

        private void ExecuteCreateEditLinkDialog(LinkDto model)
        {
            LinkItem = model!;

            _dialogHandler.CreateDialog<EditLinkItemDialog>($"正在修改{model!.Name}的信息");
        }


        #region Implements

        private async void ExcuteSubmitEditLinkItemAsync()
        {
            if (LinkItem != null)
            {
                await _linkService.ModifyLinkAsync(LinkItem);
            }
            
            ExecuteLoadLinkListAsync();
        }

        private async void ExcuteSubmitNewLinkItemAsync()
        {
            var result = await _linkService.AddLinkAsync(LinkNewDto!);
            if (!result)
            {
                MessageBox.Show("出事了");
            }
            LinkNewDto = null;
            ExecuteLoadLinkListAsync();
        }
        private async void ExecuteLoadLinkListAsync()
        {
            var items = await _linkService.LoadLinkListAsync();
            LinkList = new ObservableCollection<LinkDto>(items);

            LinkDetailView = App.AppHost!.Services.GetRequiredService<DefaultBlankViewComponent>();
        }

        #endregion

        #region Commands
        public RelayCommand<LinkDto> ShowLinkDetailViewCommand { get; set; }

        public RelayCommand<string> PopUpAddLinkCommand { get; set; }
        public RelayCommand<LinkDto> PopUpEditLinkCommand { get; set; }
        public RelayCommand SubmitNewLinkItemCommand { get; set; }
        public RelayCommand SubmitEditLinkItemCommand { get; set; }

        #endregion


        #region ObservableObject

        private ObservableCollection<LinkDto> _linkList;

        public ObservableCollection<LinkDto> LinkList { get => _linkList; set => SetProperty(ref _linkList, value); }

        private LinkCreateDto? _linkNewDto;

        public LinkCreateDto? LinkNewDto { get => _linkNewDto; set => SetProperty(ref _linkNewDto, value); }

        private LinkDto? _linkItem;

        public LinkDto? LinkItem { get => _linkItem; set => SetProperty(ref _linkItem, value); }


        private object? _linkDetaiView;

        public object? LinkDetailView { get => _linkDetaiView; set => SetProperty(ref _linkDetaiView, value); }

        #endregion
    }
}
