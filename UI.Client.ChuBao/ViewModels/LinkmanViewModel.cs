using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Messaging.Messages;
using Data.Client.ChuBao.DTOS;
using Data.Client.ChuBao.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.ObjectModel;
using UI.Client.ChuBao.Commons;
using UI.Client.ChuBao.Views;

namespace UI.Client.ChuBao.ViewModels
{
    public class LinkmanViewModel : ObservableRecipient
    {
        private readonly ILinkLocalService _linkService;
        private readonly IAbstractFactory<PopupWindow> _factory;
        private readonly IPopupManager _popupManager;
        public LinkmanViewModel(
            ILinkLocalService linkService,
            IAbstractFactory<PopupWindow> factory,
            IPopupManager popupManager)
        {
            this.IsActive = true;
            this._linkService = linkService;
            this._factory = factory;
            this._popupManager = popupManager;
            LoadDataAsync();
            PopupLinkAddCommand = new RelayCommand(() => _popupManager.CreatePopup<LinkAddForm>(App.Current.MainWindow));
            PopupLinkEditCommand = new RelayCommand<LinkDto>(dto => ExecutePopupEditForm(dto));
            DisplayLinkDetailCommand = new RelayCommand<LinkDto>(dto => ExecuteDisplayDetailView(dto)); 
            //SubmitNewLinkCommand = new RelayCommand(() => ExecuteSubmitNewLinkAsync());
            //NewLink = new LinkNewDto(); 
            Linkmen = new ObservableCollection<LinkDto>();
            IsRefresh = false;
        }

        //private async void ExecuteSubmitNewLinkAsync()
        //{
        //    if (NewLink == null)
        //    {
        //        return;
        //    }
        //    var result = await _linkService.AddAsync(NewLink);
        //    if (result > 0)
        //    {
        //        LoadDataAsync();
        //    }
        //}


        private void ExecuteDisplayDetailView(LinkDto? dto)
        {
            if (dto == null)
            {
                throw new ArgumentNullException(nameof(dto));
            }
            // 语句顺序会影响能否读取数据
            LinkDetailView = App.AppHost!.Services.GetRequiredService<LinkDetailView>();
            WeakReferenceMessenger.Default.Send(new ValueChangedMessage<LinkDto>(dto), "ToLinkDetailView");
        }

        private void ExecutePopupEditForm(LinkDto? dto)
        {
            if (dto == null)
            {
                throw new ArgumentNullException(nameof(dto));
            }
            _popupManager.CreatePopup<LinkEditForm>(App.Current.MainWindow);
            WeakReferenceMessenger.Default.Send(new ValueChangedMessage<LinkDto>(dto), "ToLinkEditForm");
        }

        private async void LoadDataAsync()
        {
            Linkmen = new ObservableCollection<LinkDto>( await _linkService.GetAllAsync());
        }

        protected override void OnActivated()
        {
            WeakReferenceMessenger.Default.Register<ValueChangedMessage<bool>, string>(this, "ToRefreshLinkmanView", (r, m) => IsRefresh = m.Value);
            base.OnActivated();
        }
        protected override void OnDeactivated()
        {
            WeakReferenceMessenger.Default.UnregisterAll<string>(this, "ToRefreshLinkmanView");
            base.OnDeactivated();
        }



        //public RelayCommand SubmitNewLinkCommand { get; set; }

        public RelayCommand PopupLinkAddCommand { get; set; }
        public RelayCommand<LinkDto> PopupLinkEditCommand { get; set; }
        public RelayCommand<LinkDto> DisplayLinkDetailCommand { get; set; }

        private bool _isRefresh;
        public bool IsRefresh
        {
            get => _isRefresh;
            set
            {
                if(SetProperty(ref _isRefresh, value))
                {
                    if (IsRefresh)
                    {
                        LoadDataAsync();
                    }
                }
            }
        }

        //private LinkNewDto? _newLink;
        //public LinkNewDto? NewLink { get => _newLink; set => SetProperty(ref _newLink, value); }

        private ObservableCollection<LinkDto> _linkmen;
        public ObservableCollection<LinkDto> Linkmen { get => _linkmen; set => SetProperty(ref _linkmen, value); }
        private object _linkDetailView;
        public object LinkDetailView { get => _linkDetailView; set => SetProperty(ref _linkDetailView, value); }
    }
}
