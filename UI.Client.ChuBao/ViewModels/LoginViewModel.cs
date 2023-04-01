using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.ComponentModel.DataAnnotations;

namespace UI.Client.ChuBao.ViewModels
{
    public class LoginViewModel : ObservableObject
    {
        
        public LoginViewModel()
        {
            
            LoginCommand = new RelayCommand (ExecuteLogin);
        }

        private void ExecuteLogin()
        {
            var username = Username;
            var password = Password;

            if (username != null && password != null) 
            {
                
            }


            throw new NotImplementedException();
        }




        public RelayCommand LoginCommand { get; set; }


        private string _username;

        [Required(ErrorMessage ="用户名不能为空")]
        public string Username { get => _username; set => SetProperty(ref _username, value); }

        private string _password;

        [Required(ErrorMessage ="密码不能为空哦")]
        public string Password { get => _password; set => SetProperty(ref _password, value);}

    }
}
