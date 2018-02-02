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
    public partial class AuthorizationDetails : ContentPage
    {
        AuthorizationDetailsViewModel _vm;
        public AuthorizationDetails(AuthorizationDetailsViewModel vm)
        {
            InitializeComponent();
            _vm = vm;
            this.BindingContext = _vm;
        }


    }
}