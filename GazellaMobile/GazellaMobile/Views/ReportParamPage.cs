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
        List<View> _listOfControls;
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
                    //Rendering the first Column
                    if (x == 0)
                    {
                        Label labelObject = new Label()
                        {
                            Text = parameters[i].Caption.ToString(),
                            Style = (Style)App.Current.Resources["titleLabel2Style"]
                        };
                        View control = GDSViewHelper.CreateControl(parameters[i]);
                        StackLayout stack = new StackLayout();
                        _listOfControls.Add(control);
                        stack.Children.Add(labelObject);
                        stack.Children.Add(control);
                        
                        gridLayout.Children.Add(stack, x, i);
                    }
                    else
                    {
                        //Rendering the second Column with the search
                        if (parameters[i].ListId > 0)
                        {
                            Image searchImg = new Image();
                            searchImg.Source = "iconsearchblue.png";
                            var tapGestureRecognizer = new TapGestureRecognizer();
                            tapGestureRecognizer.Tapped += async (s, e) => {
                                // handle the tap
                                await DisplayAlert("TapGesture","Hello World!!", "OK");
                            };
                            searchImg.GestureRecognizers.Add(tapGestureRecognizer);
                            gridLayout.Children.Add(searchImg, x, i);
                        }
                    }
                }
            }
            StackLayout mainStackLayout = new StackLayout();
            mainStackLayout.Children.Add(gridLayout);
            Button btnPreview = new Button() { Text = "Vista Previa", Style = (Style)App.Current.Resources["buttonStyle"] };
            btnPreview.Clicked += async (sender, e) => 
            {
                //Getting data from parameters
                string data = string.Empty;
                for (int i = 0; i < _listOfControls.Count; i++)
                {
                    if (_listOfControls[i] is Entry)
                    {
                        //Skipping comma at the end, If we are in the last iteration.
                        if (i == _listOfControls.Count - 1)
                        {
                            Entry entry = (Entry)_listOfControls[i];
                            data += entry.Text;

                        }
                        else
                        {
                            Entry entryObject = (Entry)_listOfControls[i];
                            data += entryObject.Text + ",";
                        }


                    }
                    else if (_listOfControls[i] is DatePicker)
                    {
                        //Skipping comma at the end, If we are in the last iteration.
                        if (i == _listOfControls.Count - 1)
                        {
                            DatePicker datePicker = (DatePicker)_listOfControls[i];
                            data += datePicker.Date.ToString();
                        }
                        else
                        {
                            DatePicker datePickerObject = (DatePicker)_listOfControls[i];
                            data += datePickerObject.Date.ToString() + ",";
                        }
                        
                    }
                    else if(_listOfControls[i] is Picker)
                    {
                        //Skipping comma at the end, If we are in the last iteration.
                        if (i == _listOfControls.Count - 1)
                        {
                            Picker picker = (Picker)_listOfControls[i];
                            data += picker.SelectedItem.ToString();                         
                        }
                        else
                        {
                            Picker pickerObject = (Picker)_listOfControls[i];
                            data += pickerObject.SelectedItem.ToString() + ",";
                        }
                        
                    }
                    else if(_listOfControls[i] is Switch)
                    {
                        //Skipping comma at the end, If we are in the last iteration.
                        if (i == _listOfControls.Count - 1)
                        {
                            Switch swtich = (Switch)_listOfControls[i];
                            data += swtich.IsToggled.ToString();

                        }
                        else
                        {
                            Switch switchObject = (Switch)_listOfControls[i];
                            data += switchObject.IsToggled.ToString() + ",";
                        }

                    }
                }

                await DisplayAlert("Preview", data, "OK");
                
            };
            mainStackLayout.Children.Add(btnPreview);
            this.Content = new ScrollView()
            {
                Content = mainStackLayout
            };
        }






    }
}