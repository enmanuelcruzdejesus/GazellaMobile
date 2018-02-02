using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Acr.UserDialogs;
using GazellaMobile;
using GazellaMobile.Helpers;
using GazellaMobile.Models;

namespace GazellaMobile.ViewModels
{
    public class LoginPageViewModel
    {
        private string _userId = string.Empty;
        private string _password = string.Empty;
        ICommand _signInCommand;     

        public string UserId
        {
            get
            {
                return _userId;
            }
            set
            {
               
                  _userId = value.Trim();

                
            }
        }
        public string Password
        {
            get
            {
                return _password;
            }
            set
            {
                _password = value.Trim();
            }
        }
        public ICommand SignInCommand
        {
            get
            {
                _signInCommand = _signInCommand ?? new Command(new Action(OnSingIn));
                return _signInCommand;
            }
        }                    
        public LoginPageViewModel()
        {
            
        
        }
        private async void OnSingIn()
        {
            if(string.IsNullOrEmpty(UserId) || string.IsNullOrEmpty(Password))
            {
                App.ToastDialog("El usuario o contraseña son incorrectos");
                return;
            }
        
            //Checking if there's internet
            if (GDSNetworkConnectivity.isConneted())
            {                
                UserDialogs.Instance.ShowLoading("Loading",MaskType.Black);             
                var logStatus = await App.isLoginSuccesFulAsync(new User(UserId,Password));                
                if (!logStatus.IsValid)
                {                    
                    UserDialogs.Instance.HideLoading();             
                    UserDialogs.Instance.Alert(logStatus.Message);                    
                    return;
                }               
                await Task.Delay(2000);
                UserDialogs.Instance.HideLoading();
                App.PresentMainPage();

            }
            else
            {                
                UserDialogs.Instance.Alert("Connection Error");

            }
        }

       

    }
}
