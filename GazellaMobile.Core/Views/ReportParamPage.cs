﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;
using GazellaMobile.ViewModels;
using GazellaMobile.Views.Helpers;
using GazellaMobile.Views.CustomControls;
using GazellaMobile.Utils.System;

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
            btnPreview.Clicked += btnPreviewClickHandler;

            btnPreview.Margin = new Thickness(0, 20, 0, 0);
            mainStackLayout.Children.Add(btnPreview);

            this.Content = new ScrollView()
            {
                Content = mainStackLayout
            };



        }


        async void btnPreviewClickHandler(object sender, EventArgs e)
        {
            List<string> paramValues = new List<string>();
            paramValues.Add(App.Cia.ToString());
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
                            paramValues.Add(value1);
                            paramValues.Add(value2);
                        }
                        else
                        {
                            paramValues.Add(entry.CustomText);
                            paramValues.Add(entry.CustomText);
                        }

                    }
                    else
                    {
                        paramValues.Add(entry.Text);
                       
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
                            paramValues.Add(value1);
                            paramValues.Add(value2);
                        }
                        else
                        {
                            paramValues.Add(entry.Text);
                            paramValues.Add(entry.Text);
                        }

                    }
                    else
                    {
                        paramValues.Add(entry.Text);
                      
                      
                    }

                }
                else if (_listOfControls[i] is DatePicker)
                {
                    DatePicker date = (DatePicker)_listOfControls[i];
                    paramValues.Add(date.Date.ToString("yyyy/MM/dd/HH:mm:ss"));
                 
                
                }
                else if (_listOfControls[i] is Picker)
                {
                    Picker pick = (Picker)_listOfControls[i];
                    var key = pick.SelectedItem.ToString().Split('.');
                    paramValues.Add(key[0]);

                }
                else if (_listOfControls[i] is Switch)
                {
                    Switch sw = (Switch)_listOfControls[i];
                    var value = sw.IsToggled ? 0 : 1;
                    paramValues.Add(value.ToString());
                }


            }

            for (int i = 0; i < paramValues.Count(); i++)
            {
                paramValues[i] = string.Format("Param{0}={1}", i, paramValues[i]);
            }

            var valuesWithCSVFormat = string.Join(",",paramValues);
            //Displaying Report
            var reportId = _vm.ReportId;
            var requestData = string.Format("VALIDTIME=13/03/2019,ReportId={1},CIA={2},User={3}",DateTime.Now.ToString("dd/MM/yyyy"),reportId,App.Cia,App.CurrentUser.UserId.ToUpper());
            Console.WriteLine(requestData);
            Device.OpenUri(new Uri(string.Format("http://gazella.ddns.net/webreport/gowebReport.exe?RequestData={0}&RequestParam={1}",requestData.EncodeBase64(),valuesWithCSVFormat.EncodeBase64())));

        }

    }
}