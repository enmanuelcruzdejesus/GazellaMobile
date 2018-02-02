using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using GazellaMobile.ViewModels;

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
        protected override void OnAppearing()
        {
            base.OnAppearing();
            this.userNameEntry.Focus();
        }

    }
}