using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;
using GazellaMobile.ViewModels;
using GazellaMobile.Views.Helpers;
using GazellaMobile.Views.CustomControls;

namespace GazellaMobile.Views
{
    public class ReportParamPage : ContentPage
    {
        ReportParamViewModel _vm;
        dynamic[] _parameters;
        List<View> _listOfControls;
        public ReportParamPage(ReportParamViewModel viewModel)
        {
            this.Title = "Parametros";
            this.Padding = new Thickness(5, 20, 5, 0);
            _vm = viewModel;
            _listOfControls = new List<View>();

        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            _parameters = await _vm.Data;
            RenderParams();
        }

        private void RenderParams()
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
            Button btnPreview = new Button() { Text = "Vista Previa", Style = (Style)App.Current.Resources["buttonStyle"] };
            btnPreview.Clicked += async (s, e) =>
            {
                //Getting data from parameters
                string data = App.Cia.ToString() + ',';
                var novisualParameters = _parameters.Where(p => p.Visible.ToString() == "N");
                for (int i = 0; i < _listOfControls.Count; i++)
                {

                    if (_listOfControls[i] is ButtonEntry)
                    {
                        ButtonEntry entry = (ButtonEntry)_listOfControls[i];
                        var param = (dynamic)entry.BindingContext;
                        var iniLine = Convert.ToInt32(param.IniLine);
                        if (iniLine > 0)
                        {
                            if (string.IsNullOrEmpty(entry.Text))
                            {
                                string value1 = " ";
                                if (param.DataType.ToString() == "N")
                                    value1 = "0";

                                var value2 = novisualParameters.Single(p => p.Sequence == iniLine).DefaultValue.ToString();
                                data += value1 + ',' + value2;
                            }
                            else
                            {
                                var value1 = entry.CustomText;
                                var value2 = entry.CustomText;
                                data += value1 + ',' + value2;
                            }

                        }
                        else
                        {
                            data += entry.Text;
                        }
                    }
                    else if (_listOfControls[i] is Entry)
                    {

                        Entry entry = (Entry)_listOfControls[i];
                        var param = (dynamic)entry.BindingContext;
                        var iniLine = Convert.ToInt32(param.IniLine);
                        if (iniLine > 0)
                        {
                            if (string.IsNullOrEmpty(entry.Text))
                            {
                                string value1 = " ";
                                if (param.DataType.ToString() == "N")
                                    value1 = "0";

                                var value2 = novisualParameters.Single(p => p.Sequence == iniLine).DefaultValue.ToString();
                                data += value1 + ',' + value2;
                            }
                            else
                            {
                                var value1 = entry.Text;
                                var value2 = entry.Text;
                                data += value1 + ',' + value2;
                            }

                        }
                        else
                        {
                            data += entry.Text;
                        }

                    }
                    else if (_listOfControls[i] is DatePicker)
                    {
                        DatePicker date = (DatePicker)_listOfControls[i];
                        data += date.Date.ToString();
                    }
                    else if (_listOfControls[i] is Picker)
                    {
                        Picker pick = (Picker)_listOfControls[i];
                        data += pick.SelectedItem.ToString();
                    }
                    else if (_listOfControls[i] is Switch)
                    {
                        Switch sw = (Switch)_listOfControls[i];
                        data += sw.IsToggled.ToString();
                    }

                    //adding comma if we didn't reach the end of the array
                    if (!(i == _listOfControls.Count - 1))
                    {
                        data += ',';
                    }
                }

                await DisplayAlert("Preview", _listOfControls.Count.ToString(), "OK");
            };

            mainStackLayout.Children.Add(btnPreview);

            this.Content = new ScrollView()
            {
                Content = mainStackLayout
            };



        }


    }
}