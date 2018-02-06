using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

using Acr.UserDialogs;
using Rg.Plugins.Popup.Services;

using GazellaMobile.Models;
using GazellaMobile.Views;
using GazellaMobile.Views.CustomControls;
using GazellaMobile.Utils.Services;
using System.Net.Http;

namespace GazellaMobile.ViewModels
{
    public class LoginPageViewModel
    {
        private string _userId = string.Empty;
        private string _password = string.Empty;
        ICommand _signInCommand;
        InputDialogBase<bool> _popup;

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
            else if(App.IsServerConfigured)
            {
                UserDialogs.Instance.ShowLoading("Loading", MaskType.Black);
                var logStatus = await App.isLoginSuccesFulAsync(new User(UserId, Password));
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
                
                await UserDialogs.Instance.AlertAsync("Servidor no configurado","Warning","OK");
                ServerSettingsDialogViewModel vm = new ServerSettingsDialogViewModel();
                ServerSettingsDialog dialog = new ServerSettingsDialog(vm);

                //Creating the Transparent Popup Page
                //of type string since we need a string return
                _popup = new InputDialogBase<bool>(dialog);

                vm.ClickEventHandler += async delegate
                {
                    if (string.IsNullOrEmpty(vm.Server) || string.IsNullOrEmpty(vm.UserId) || string.IsNullOrEmpty(vm.Password))
                    {
                        App.ToastDialog("Debe llenar los campos requeridos");
                        return;
                    }
                    else
                    {

                        var uri = string.Format("http://{0}/", vm.Server.Trim());
                        uri += "gazellamobileapi/api/{0}/{1}";
                        var serviceClient = new GDSServiceClient(uri);
                        try
                        {
                            var response = await serviceClient.Post<User>("Login", new User(vm.UserId, vm.Password));
                            _popup.PageClosedComplitionSource.SetResult(response.IsSuccessStatusCode);
                        }
                        catch (HttpRequestException ex)
                        {
                            _popup.PageClosedComplitionSource.SetResult(false);
                        }
                       
                     
                       

                    }
                };



                // Push the page to Navigation Stack
                await PopupNavigation.PushAsync(_popup);

                // await for the user to enter the text input
                var result = await _popup.PageClosedTask;

                //IF RESULT IS SUCCESFULL THEN SAVE SERVER SETTINGS
                if(result)
                {
                    App.Settings.Server = vm.Server;
                    App.Settings.UserId = vm.UserId;
                    App.Settings.Password = vm.Password;
                    App.DbConnection.Insert(App.Settings);
                }
                else
                {
                    App.ToastDialog("Fallo en conexion. Verifique e intente nuevamente",5000);
                }


                // Pop the page from Navigation Stack
                await PopupNavigation.PopAsync();



            

                
            }
           

        }


    }
}
