using Acr.UserDialogs;
using GazellaMobile.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using GazellaMobile.Views;
namespace GazellaMobile.ViewModels
{
    public class SettingsPageViewModel
    {
        private ICommand _logOutCommand;
        private ICommand _saveCommand;      
        private AppSettings _settings;


        public ICommand LogOutCommand
        {
            get
            {
                _logOutCommand = _logOutCommand ?? new Command(new Action(OnLogOut));
                return _logOutCommand;
            }
        }
        public ICommand SaveCommand
        {
            get
            {
                _saveCommand = _saveCommand ?? new Command(new Action(OnSave));
                return _saveCommand;
            }
        }
        public AppSettings Settings
        {
            get
            {
                return _settings;
            }
            set
            {
                _settings = value;
            }
        }
      
        public SettingsPageViewModel()
        {           
           _settings = App.DbConnection.Table<AppSettings>().First();          
        }


        private void OnLogOut()
        {
            App.LogOut();
        }
       
        private async void OnSave()
        {
            //Saving some data            
            UserDialogs.Instance.ShowLoading("Guardando", MaskType.Black);
            App.DbConnection.Update(_settings);
            await Task.Delay(2000);
            UserDialogs.Instance.HideLoading();
            //if the server section has changed
            //then loging out will be required
            if (App.Settings.Server != this.Settings.Server)
                App.LogOut();



        }


    }
}
