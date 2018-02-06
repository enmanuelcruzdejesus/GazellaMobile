using System;
using System.Windows.Input;

namespace GazellaMobile.ViewModels
{
    public class ServerSettingsDialogViewModel
    {

        ICommand _testConnectionCommand;
      
        public string Server { get; set; }
        public string UserId { get; set; }
        public string Password { get; set; }
       

        public ICommand TestConnectionCommand
        {
            get
            {
                return _testConnectionCommand;
            }
            set
            {
                _testConnectionCommand = value;
            }
        }


        public EventHandler ClickEventHandler { get; set; }
    }
}
