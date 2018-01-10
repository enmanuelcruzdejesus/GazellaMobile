using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;
using GazellaMobile.ViewModels;
using GazellaMobile.Views.Helpers;

namespace GazellaMobile.Views
{
    public class ReportParamPage : ContentPage
    {
        ReportParamViewModel _vm;
        List<View> _listOfControls;
        dynamic[] _parameters;
        public ReportParamPage(ReportParamViewModel viewModel)
        {
            this.Title = "Parametros";
            this.Padding = Device.OnPlatform<Thickness>(
               iOS:new Thickness(0, 20, 0, 0),
               Android: new Thickness(5, 5, 0, 0),
               WinPhone: new Thickness(5, 5, 0, 0));
            _vm = viewModel;
            _listOfControls = new List<View>();           
            
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            _parameters = await _vm.Data;
            RenderParams();
        }

        private  void RenderParams()
        {
            var visualParameters = _parameters.Where(r => r.Visible.ToString() == "Y").ToList();
            StackLayout mainStackLayout = new StackLayout();

            foreach (var parameter in visualParameters)
            {
                Label labelObject = new Label()
                {
                    Text = parameter.Caption.ToString(),
                    Style = (Style)App.Current.Resources["titleLabel2Style"]
                };
                View control = GDSViewHelper.CreateControl(parameter);
                mainStackLayout.Children.Add(labelObject);
                mainStackLayout.Children.Add(control);
                _listOfControls.Add(control);

            }
            Button btnPreview = new Button() { Text = "Vista Previa", Style = (Style)App.Current.Resources["buttonStyle"]  };


            mainStackLayout.Children.Add(btnPreview);

            this.Content = new ScrollView() 
            {
                Content = mainStackLayout
            }; 


        }

    }
}