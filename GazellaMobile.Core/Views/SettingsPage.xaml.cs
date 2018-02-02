using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using GazellaMobile.ViewModels;
using System.Windows.Input;

namespace GazellaMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsPage : ContentPage
    {

        SettingsPageViewModel vm = new SettingsPageViewModel();

        public SettingsPage()
        {
            InitializeComponent();
            this.BindingContext = vm;
            this.settingsTableView.BindingContext = vm;

        }


    }
}