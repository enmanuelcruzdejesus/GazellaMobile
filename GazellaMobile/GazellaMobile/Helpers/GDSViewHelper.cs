using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
namespace GazellaMobile.Helpers
{
   public class GDSViewHelper
   {
      public static View CreateControl(dynamic sender)
      {
          View control;
          if(sender.ObjectType == "TextBox")
          {
                if(sender.DataType == "N")
                {
                    control = new Entry()
                    {
                        Keyboard = Keyboard.Numeric,
                        Style = (Style)App.Current.Resources["entryStyle"]
                    };
                }
                else
                {
                    control = new Entry() 
                    {
                        Style = (Style)App.Current.Resources["entryStyle"]
                    };
                }
          }
          else if(sender.ObjectType == "DateBox")
          {
                control = new DatePicker()
                {
                    Date = DateTime.Now,
                    Format = "D"
                };

          }else if(sender.ObjectType == "CheckBox")
          {
                bool isChecked = sender.ObjectValue == "S";
                control = new Switch()
                {
                    HorizontalOptions = LayoutOptions.Start,
                   IsToggled = isChecked
                   
                };
          }
          else
          {
                var comboValues = sender.ObjectValue.ToString().Split(',');
                control = new Picker()
                {
                    SelectedIndex = 0,
                    ItemsSource  = comboValues

                };
          }


            return control;
      }
   }
}
