using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using GazellaMobile.Utils.System;
using GazellaMobile.Views.CustomControls;
using Rg.Plugins.Popup.Services;
using System.Diagnostics;

namespace GazellaMobile.Views.Helpers
{
    public class GDSViewHelper
    {

        private async static void OnSearch(object sender)
        {
            // handle the tap
            ButtonEntry entry = (ButtonEntry)sender;
            dynamic param = entry.BindingContext;
            int listId = Convert.ToInt32(param.ListId);



            var searchData = await App.ServiceClient.GetSearchList(listId);
            //creating the Search Dialog
            var searchList = new SearchListDialog(searchData);

            //Creating the Transparent Popup Page
            //of type string since we need a string return
            var popup = new InputDialogBase<string>(searchList);

            //Suscribing to the SearchListDialog's Tapped Event
            searchList.TappedEvent += (s, args) =>
            {
                SearchListDialog obj = (SearchListDialog)s;
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
            entry.CustomText = result;

            entry.Focus();


        }

        public static View CreateControl(dynamic sender)
        {
            if (sender.ObjectType == "TextBox")
            {
                //IF THE CONTROL HAS A LIST ASSIGNED
                if (sender.ListId > 0)
                {
                    ButtonEntry control = new ButtonEntry()
                    {
                        CustomText = sender.DefaultValue.ToString(),
                        Style = (Style)App.Current.Resources["entryStyle"],
                        RightImage = "searchicon"
                    };

                
                    control.RightClick += (s, e) => { OnSearch(control); };

                    if (sender.DataType == "N")
                        control.Keyboard = Keyboard.Numeric;

                    control.BindingContext = sender;
                    
                    return control;

                }
                else
                {
                    Entry control = new Entry()
                    {
                        Text = sender.DefaultValue.ToString(),
                        Keyboard = Keyboard.Numeric,
                        Style = (Style)App.Current.Resources["entryStyle"]
                    };
                    if (sender.DataType == "N")
                        control.Keyboard = Keyboard.Numeric;

                    control.BindingContext = sender;
                    return control;


                }


            }
            else if (sender.ObjectType == "DateBox")
            {
                DatePicker control = new DatePicker()
                {
                    Date = DateTime.Now,
                    Format = "D"
                };


                if (sender.ObjectValue == "BEGINDATE")
                    control.Date = DateTime.Now.FirstDayOfMonth();

                if (sender.ObjectValue == "ENDDATE")
                    control.Date = DateTime.Now.LastDayOfMonth();

                control.BindingContext = sender;
                return control;
            }
            else if (sender.ObjectType == "CheckBox")
            {
                bool isChecked = sender.ObjectValue == "S";
                Switch control = new Switch()
                {
                    HorizontalOptions = LayoutOptions.Start,
                    IsToggled = isChecked

                };
                control.BindingContext = sender;
                return control;
            }
            else
            {
                var comboValues = sender.ObjectValue.ToString().Split(',');
                Picker control = new Picker();
                control.ItemsSource = comboValues;
                control.SelectedIndex = 0;
                control.SelectedItem = control.Items[control.SelectedIndex];
                control.BindingContext = sender;
                return control;

            }


        }


    }
}
