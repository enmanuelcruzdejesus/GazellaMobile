using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GazellaMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : TabbedPage
    {
        public MainPage()
        {
            InitializeComponent();
            NavigationPage navQueryPage = new NavigationPage( new QueryPage());
            navQueryPage.Title = "Queries";

            NavigationPage navAuthorizationPage = new NavigationPage(new AuthorizationPage());
            navAuthorizationPage.Title = "Authorization";


            NavigationPage navSettingsPage = new NavigationPage(new SettingsPage());
            navSettingsPage.Title = "Settings";

            this.Children.Add(navQueryPage);
            this.Children.Add(navAuthorizationPage);
            this.Children.Add(navSettingsPage);


        }
    }
}