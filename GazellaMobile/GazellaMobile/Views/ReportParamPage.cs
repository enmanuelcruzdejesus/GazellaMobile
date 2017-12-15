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
            var parameters = await _vm.Data;

            Grid gridLayout = new Grid();
            gridLayout.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
            gridLayout.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(50, GridUnitType.Absolute) });
            for (int i = 0; i < parameters.Count(); i++)
            {
                gridLayout.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Auto) });
            }

            for (int i = 0; i < gridLayout.RowDefinitions.Count; i++)
            {
                for (int x = 0; x < gridLayout.ColumnDefinitions.Count; x++)
                {
                    if (x == 0)
                    {
                        Label labelObject = new Label()
                        {
                            Text = parameters[i].Caption.ToString(),
                            Style = (Style)App.Current.Resources["titleLabel2Style"]
                        };
                        View control = GDSViewHelper.CreateControl(parameters[i]);
                        StackLayout stack = new StackLayout();
                        stack.Children.Add(labelObject);
                        stack.Children.Add(control);
                        gridLayout.Children.Add(stack, x, i);
                    }
                    else
                    {
                        if (parameters[i].ListId > 0)
                        {
                            Image searchImg = new Image();
                            searchImg.Source = "iconsearchblue.png";
                            gridLayout.Children.Add(searchImg, x, i);
                        }
                    }


                }
            }

            StackLayout mainStackLayout = new StackLayout();
            mainStackLayout.Children.Add(gridLayout);
            Button btnPreview = new Button() { Text = "Vista Previa", Style = (Style)App.Current.Resources["buttonStyle"] };
            btnPreview.Clicked += (sender, e) => { };
            mainStackLayout.Children.Add(btnPreview);
            this.Content = new ScrollView()
            {
                Content = mainStackLayout
            };
        }






    }
}