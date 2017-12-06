using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;
using GazellaMobile.ViewModels;

namespace GazellaMobile.Views
{
    public class ReportParamPage : ContentPage
    {
        ReportParamViewModel _vm;

        public ReportParamPage(ReportParamViewModel viewModel)
        {
            _vm = viewModel;        
            
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            RenderParams();
        }

        private async void RenderParams()
        {
            var data = await _vm.Data;
            StackLayout mainStakLayout = new StackLayout();              
            foreach (var item in data)
            {
                Label labelObject = new Label() { Text = item.Caption.ToString() , FontAttributes = FontAttributes.Bold };
                Entry entryObject = new Entry();
                mainStakLayout.Children.Add(labelObject);
                mainStakLayout.Children.Add(entryObject);
            }
            this.Content = new ScrollView()
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                Content  = mainStakLayout
            };
        }






    }
}