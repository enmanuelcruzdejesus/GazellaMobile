using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using GazellaMobile.ViewModels;
using Rg.Plugins.Popup.Services;
using GazellaMobile.Views.CustomControls;
using Polly;

namespace GazellaMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        LoginPageViewModel vm;
        public LoginPage()
        {
            InitializeComponent();
            vm = new LoginPageViewModel();
            this.BindingContext = vm;

        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            this.userNameEntry.Focus();
        }

    }
}