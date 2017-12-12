using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;
using GazellaMobile.ViewModels;
using GazellaMobile.Helpers;
using System.Diagnostics;

namespace GazellaMobile.Views
{
    public class ReportParamPage : ContentPage
    {
        ReportParamViewModel _vm;        
        public ReportParamPage(ReportParamViewModel viewModel)
        {
            this.Title = "Parametros";
            this.Padding = Device.OnPlatform<Thickness>(
               iOS:new Thickness(0, 20, 0, 0),
               Android: new Thickness(5, 5, 0, 0),
               WinPhone: new Thickness(5, 5, 0, 0));
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
                Label labelObject = new Label()
                {
                    Text = item.Caption.ToString() ,
                    Style = (Style)App.Current.Resources["titleLabel2Style"]
                };              
                mainStakLayout.Children.Add(labelObject);
                View control = GDSViewHelper.CreateControl(item);
                mainStakLayout.Children.Add(control);
            }
            Button btnPreView = new Button();
            btnPreView.Text = "Preview";          
            btnPreView.Style = (Style)App.Current.Resources["buttonStyle"];
            btnPreView.Clicked += (sender,e) =>
            {

            };
            mainStakLayout.Children.Add(btnPreView);
            this.Content = new ScrollView()
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                Content  = mainStakLayout
            };
        
        }






    }
}