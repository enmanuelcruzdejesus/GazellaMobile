using System;
using System.Collections.Generic;
using GazellaMobile.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GazellaMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ServerSettingsDialog : ContentView
    {
        ServerSettingsDialogViewModel _vm;
        public ServerSettingsDialog(ServerSettingsDialogViewModel ViewModel)
        {
            InitializeComponent();
            _vm = ViewModel;
            this.BindingContext = _vm;

        }

        void Handle_Clicked(object sender, System.EventArgs e)
        {
            if (_vm.ClickEventHandler != null)
                _vm.ClickEventHandler(this, EventArgs.Empty);
        }
    }
}
