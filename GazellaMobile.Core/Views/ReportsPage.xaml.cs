using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using GazellaMobile.ViewModels;
using GazellaMobile.Helpers;

namespace GazellaMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ReportsPage : ContentPage
    {
        ReportsPageViewModel vm = new ReportsPageViewModel();
        public ReportsPage()
        {
            InitializeComponent();


        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            this.reportsListView.ItemsSource = await vm.Data;
        }

        private async void reportsListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            dynamic item = (dynamic)e.Item;
            int moduleId = Convert.ToInt32(item.ModuleId);
            await this.Navigation.PushAsync(new ReportDetailsPage(new ReportDetailsViewModel(moduleId)));
        }
    }
}