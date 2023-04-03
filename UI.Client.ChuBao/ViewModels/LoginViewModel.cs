using Access.Client.ChuBao.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Core.Client.ChuBao.Dtos;
using Microsoft.Extensions.DependencyInjection;
using System.ComponentModel.DataAnnotations;

namespace UI.Client.ChuBao.ViewModels
{
    public class LoginViewModel : ObservableValidator
    {
        private readonly IAuthService _authService;



        public LoginViewModel(IAuthService authService)
        {
            
            LoginCommand = new RelayCommand (ExecuteLogin);
            this._authService = authService;
            IsCloseLoginWindow = false;
        }

        private async void ExecuteLogin()
        {
            var model = new LoginDto { UserName = Username,Password = Password };
            var result = await _authService.Login(model);
            App.AccessToken = result.Token;

            if (result.IsLogin)
            {
                var win = App.AppHost!.Services.GetRequiredService<MainWindow>();
                win.Show();
                if (win.IsActive)
                {
                    IsCloseLoginWindow = true;
                }
            }
        }




        public RelayCommand LoginCommand { get; set; }


        private string? _username;


        [Required(AllowEmptyStrings = false, ErrorMessage = "用户名不能为空")]
        public string? Username { get => _username; 
            set 
            { 
                SetProperty(ref _username, value, validate: true);
                IsLoginBtnEnable = !HasErrors;
            } 
        }

        private string? _password;

        [Required(AllowEmptyStrings = false, ErrorMessage = "密码不能为空哦")]
        public string? Password
        {
            get => _password;
            set
            {
                SetProperty(ref _password, value, true);
                IsLoginBtnEnable = !HasErrors;
            }
        }

        private bool? _isLoginBtnEnable;
        public bool? IsLoginBtnEnable { get => _isLoginBtnEnable; set => SetProperty(ref _isLoginBtnEnable, value); }

        private bool? _isCloseLoginWindow;
        public bool? IsCloseLoginWindow { get => _isCloseLoginWindow; set => SetProperty(ref _isCloseLoginWindow, value); }

    }
}
