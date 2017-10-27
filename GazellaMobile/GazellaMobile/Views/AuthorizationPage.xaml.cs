using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using GazellaMobile.ViewModels;
using System.Diagnostics;

namespace GazellaMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AuthorizationPage : ContentPage
    {
        AuthorizationViewModel vm;
        public AuthorizationPage()
        {
            InitializeComponent();
            vm = new AuthorizationViewModel();
            
        }
              
        protected async override void OnAppearing()
        {
            base.OnAppearing();            
            this.authorizationListView.ItemsSource = await vm.Data;
        }
        
    }
}