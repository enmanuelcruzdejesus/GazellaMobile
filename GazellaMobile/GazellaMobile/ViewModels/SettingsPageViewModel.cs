using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace GazellaMobile.ViewModels
{
    public class SettingsPageViewModel
    {
        private ICommand _logOutCommand;
        public ICommand LogOutCommand
        {
            get
            {
                 _logOutCommand = _logOutCommand ?? new Command(new Action(OnLogOut));
                return _logOutCommand;
            }
        }

        private void OnLogOut()
        {
            App.LogOut();
        }
    }
}
