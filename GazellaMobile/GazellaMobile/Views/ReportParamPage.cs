using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;
using Rg.Plugins.Popup.Services;
using GazellaMobile.ViewModels;
using GazellaMobile.Helpers;
using GazellaMobile.Views.CustomControls;

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

            //Setting up the Grid Columnns
            Grid gridLayout = new Grid();
            gridLayout.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
            gridLayout.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(50, GridUnitType.Absolute) });
            //Setting up the Grid Rows
            for (int i = 0; i < visualParameters.Count(); i++)
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
                            Text = visualParameters[i].Caption.ToString(),
                            Style = (Style)App.Current.Resources["titleLabel2Style"]
                        };
                        View control = GDSViewHelper.CreateControl(visualParameters[i]);
                        StackLayout stack = new StackLayout();
                        _listOfControls.Add(control);
                        stack.Children.Add(labelObject);
                        stack.Children.Add(control);

                        gridLayout.Children.Add(stack, x, i);
                    }
                    else
                    {
                        //Rendering the second Column with the search
                        if (visualParameters[i].ListId > 0)
                        {
                            Image searchImg = new Image();
                            searchImg.Source = "iconsearchblue.png";
                            searchImg.BindingContext = _listOfControls[i];
                            var tapGestureRecognizer = new TapGestureRecognizer();
                            tapGestureRecognizer.Tapped += async (s, e) =>
                            {
                                // handle the tap
                                View control = ((Image)s).BindingContext as View;
                                dynamic param = control.BindingContext;
                                int listId = Convert.ToInt32(param.ListId);
                                var searchData = await App.ServiceClient.GetSearchList(listId);
                                //creating the Search Dialog
                                var searchList = new SearchListDialog(searchData);

                                //Creating the Transparent Popup Page
                                //of type string since we need a string return
                                var popup = new InputDialogBase<string>(searchList);

                                //Suscribing to the SearchListDialog's Tapped Event
                                searchList.TappedEvent += (sender, args) =>
                               {
                                   SearchListDialog obj = (SearchListDialog)sender;
                                   dynamic item = (dynamic)args.Item;

                                   obj.Result = Convert.ToString(item.Title);

                                    ///Updating the page completion source
                                    popup.PageClosedComplitionSource.SetResult(obj.Result);

                                   obj.TappedEvent = null;


                               };
                                //Pushing the page to the navigation stack
                                await PopupNavigation.PushAsync(popup);

                                //awaiting for the result 
                                var result = await popup.PageClosedTask;

                                //Poping the page from Navigation Stack
                                await PopupNavigation.PopAsync();

                                //Initializing the control with the result
                                Entry entry = (Entry)control;
                                entry.Text = result;



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
                var novisualParameters = _parameters.Where(p => p.Visible.ToString() == "N");
                for (int i = 0; i < _listOfControls.Count; i++)
                {
                    if (_listOfControls[i] is Entry)
                    {
                        Entry entry = (Entry)_listOfControls[i];
                        var param = (dynamic)entry.BindingContext;
                        var iniLine = Convert.ToInt32(param.IniLine);
                        if(iniLine > 0)
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
                        DatePicker datePicker = (DatePicker)_listOfControls[i];
                        data += datePicker.Date.ToString();
                    }
                    else if (_listOfControls[i] is Picker)
                    {
                        Picker picker = (Picker)_listOfControls[i];
                        data += picker.SelectedItem.ToString();
                    }
                    else if (_listOfControls[i] is Switch)
                    {
                        Switch swtich = (Switch)_listOfControls[i];
                        data += swtich.IsToggled.ToString();
                    }

                    if (i != _listOfControls.Count - 1)
                    {
                        data += ',';
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