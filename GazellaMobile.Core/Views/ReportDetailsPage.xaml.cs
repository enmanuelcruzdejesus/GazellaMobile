using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using GazellaMobile.ViewModels;
using Rg.Plugins.Popup.Services;

namespace GazellaMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ReportDetailsPage : ContentPage
    {
        ReportDetailsViewModel _vm;
        dynamic[] _data;
        public ReportDetailsPage(ReportDetailsViewModel viewModel)
        {
            InitializeComponent();
            _vm = viewModel;
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            _data = await _vm.Data;
            this.reportDetailsListView.ItemsSource = _data;
        }

        private async void reportDetailsListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            dynamic item = (dynamic)e.Item;
            int reportId = Convert.ToInt32(item.ReportId);
            await Navigation.PushAsync(new ReportParamPage(new ReportParamViewModel(reportId)));



        }


        private void reportSearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            var keyboard = reportSearchBar.Text;
            var searchResult = _data.Where(r => r.ProgramDescrip.ToLower().Contains(keyboard.ToLower()));
            this.reportDetailsListView.ItemsSource = searchResult;
        }
    }
}