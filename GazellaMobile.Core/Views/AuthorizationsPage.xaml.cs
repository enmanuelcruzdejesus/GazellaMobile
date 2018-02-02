using Acr.UserDialogs;
using GazellaMobile.Helpers;
using GazellaMobile.ViewModels;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

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


        private async void authorizationListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            dynamic item = (dynamic)e.Item;

            await Navigation.PushAsync(new AuthorizationDetails(new AuthorizationDetailsViewModel(item)));


        }


        private async void OnAccept(object sender, EventArgs e)
        {
            var item = (MenuItem)sender;
            var selectedCell = (dynamic)item.CommandParameter;
            var result = await App.PromptDialog("Comments", Acr.UserDialogs.InputType.Name, "", false);
            AuthConfirmation auth = new AuthConfirmation
                (
                  App.CurrentUser.UserId,
                  Convert.ToInt32(selectedCell.AuthId),
                  true,
                  result.Text
                );
            var responseMessage = await App.ServiceClient.AuthConfirmationResponse(auth);
            UserDialogs.Instance.ShowSuccess(responseMessage);

            //Updating 
            this.authorizationListView.ItemsSource = await vm.Data;
        }

        private async void OnCancel(object sender, EventArgs e)
        {
            var item = (MenuItem)sender;
            var selectedCell = (dynamic)item.CommandParameter;
            var result = await App.PromptDialog("Cancel Comments", Acr.UserDialogs.InputType.Name, "", false);
            AuthConfirmation auth = new AuthConfirmation
                (
                  App.CurrentUser.UserId,
                  Convert.ToInt32(selectedCell.AuthId),
                  false,
                  result.Text
                );
            var responseMessage = await App.ServiceClient.AuthConfirmationResponse(auth);
            UserDialogs.Instance.ShowSuccess(responseMessage);

            //Updating 
            this.authorizationListView.ItemsSource = await vm.Data;
        }
    }
}