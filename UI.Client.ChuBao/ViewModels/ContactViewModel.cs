using Access.Client.ChuBao;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Core.Client.ChuBao.Dtos;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.ObjectModel;
using System.Linq;
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
            this.NewRecord = new RecordCreateDto();
            this.LinkMark = new MarkDto();

            this.LinkDetailView = App.AppHost!.Services.GetRequiredService<DefaultBlankViewComponent>();

            //ExecuteLoadLinkListAsync();
            ExecuteLoadLinkAndMarkListAsync();

            PopUpAddLinkCommand = new RelayCommand<string>(title =>
            _dialogHandler.CreateDialog<AddLinkItemDialog>("新增联系人"));

            PopUpEditLinkCommand = new RelayCommand<ContactAMark>(model => ExecuteCreateEditLinkDialog(model!));
            PopUpEditLinkMarkCommand = new RelayCommand<MarkDto>(title =>
            _dialogHandler.CreateDialog<EditLinkMarkDialog>($"{LinkItem!.Name} 的标签管理"));


            SubmitNewLinkItemCommand = new RelayCommand(ExcuteSubmitNewLinkItemAsync);
            SubmitEditLinkItemCommand = new RelayCommand(ExcuteSubmitEditLinkItemAsync);

            ShowLinkDetailViewCommand = new RelayCommand<ContactAMark>(model => ExecuteShowLinkDetailView(model));
            SubmitNewLinkRecordCommand = new RelayCommand(ExcuteSubmitNewLinkRecordAsync);

            CheckLinkMarkCommand = new RelayCommand<string>(x => ExecuteEditLinkMarkAsync(x));

        }



        #region Implements
        private async void ExecuteLoadLinkAndMarkListAsync()
        {
            var result = await _linkService!.LoadLinkAndMarkListAsync();

            LinkAndMarkList = new ObservableCollection<ContactAMark>(result);
        }
        private async void ExecuteEditLinkMarkAsync(string? markItem = null)
        {
            var result = await _linkService!.UpdateLinkMarkAsync(LinkMark!);
            
            ExcuteLoadLinkRecordListAsync(LinkItem!.Id);
        }

        private async void ExcuteSubmitNewLinkRecordAsync()
        {
            // dev时是写死的，后面要获取登录的用户名来替代
            NewRecord!.Booker = "hyd";
            NewRecord.ContactId = LinkItem!.Id;

            await _linkService.AddLinkRecordAsync(NewRecord);

            NewRecord = null;
            ExcuteLoadLinkRecordListAsync(LinkItem!.Id);
        }

        private async void ExcuteLoadLinkRecordListAsync(Guid id)
        {
            var records = (await _linkService.GetRecordListAsync(id))
                .OrderByDescending(x => x.AddTime);

            Records = new ObservableCollection<RecordDto>(records);
        }
        private async void ExecuteShowLinkDetailView(ContactAMark? model)
        {
            if (model == null)
            {
                return;
            }
            LinkItem = new LinkDto
            {
                Id = model.ContactId,
                Name = model.Name,
                Phone = model.Phone,
                Complex = model.Complex,
                Door = model.Door
            };
            LinkMark = await _linkService.GetLinkMarkAsync(LinkItem.Id);
            LinkDetailView = App.AppHost!.Services.GetRequiredService<LinkDetailComponent>();
            ExcuteLoadLinkRecordListAsync(LinkItem.Id);
        }

        private void ExecuteCreateEditLinkDialog(ContactAMark model)
        {
            LinkItem = new LinkDto
            {
                Id = model.ContactId,
                Name = model.Name,
                Phone = model.Phone,
                Complex = model.Complex,
                Door = model.Door
            };

            _dialogHandler.CreateDialog<EditLinkItemDialog>($"正在修改{model!.Name}的信息");
        }

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
            //if (LinkNewDto == null) return;

            LinkNewDto!.Booker = "hyd";

            await _linkService.AddLinkAsync(LinkNewDto!);

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
        public RelayCommand SubmitNewLinkRecordCommand { get; set; }
        public RelayCommand<ContactAMark> ShowLinkDetailViewCommand { get; set; }

        public RelayCommand<string> PopUpAddLinkCommand { get; set; }
        public RelayCommand<ContactAMark> PopUpEditLinkCommand { get; set; }
        public RelayCommand SubmitNewLinkItemCommand { get; set; }
        public RelayCommand SubmitEditLinkItemCommand { get; set; }
        public RelayCommand<string> CheckLinkMarkCommand { get; set; }
        public RelayCommand<MarkDto> PopUpEditLinkMarkCommand { get; set; }

        #endregion


        #region ObservableObject

        private ObservableCollection<LinkDto>? _linkList;

        public ObservableCollection<LinkDto>? LinkList { get => _linkList; set => SetProperty(ref _linkList, value); }

        private LinkCreateDto? _linkNewDto;

        public LinkCreateDto? LinkNewDto { get => _linkNewDto; set => SetProperty(ref _linkNewDto, value); }

        private LinkDto? _linkItem;

        public LinkDto? LinkItem { get => _linkItem; set => SetProperty(ref _linkItem, value); }


        private object? _linkDetaiView;

        public object? LinkDetailView { get => _linkDetaiView; set => SetProperty(ref _linkDetaiView, value); }

        private ObservableCollection<RecordDto>? _records;

        public ObservableCollection<RecordDto>? Records { get => _records; set => SetProperty(ref _records, value); }

        private RecordCreateDto? _newRecord;

        public RecordCreateDto? NewRecord { get => _newRecord; set => SetProperty(ref _newRecord, value); }

        private MarkDto? _linkMark;
        public MarkDto? LinkMark { get => _linkMark; set => SetProperty(ref _linkMark, value);}
        private ObservableCollection<ContactAMark>? _linkAndMarkList;
        public ObservableCollection<ContactAMark>? LinkAndMarkList { get => _linkAndMarkList; set =>  SetProperty(ref _linkAndMarkList, value); }


        #endregion
    }
}
