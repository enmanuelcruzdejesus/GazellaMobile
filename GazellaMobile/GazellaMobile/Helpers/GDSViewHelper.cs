using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using GazellaMobile.Utils.System;

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
                        Text = sender.DefaultValue.ToString(),
                        Keyboard = Keyboard.Numeric,
                        Style = (Style)App.Current.Resources["entryStyle"]
                    };                    
                }
                else
                {
                    control = new Entry() 
                    {
                        Text = sender.DefaultValue.ToString(),
                        Style = (Style)App.Current.Resources["entryStyle"]
                    };
                }
          }
          else if(sender.ObjectType == "DateBox")
          {
                if(sender.ObjectValue == "BEGINDATE")
                {
                    control = new DatePicker()
                    {
                        Date = DateTime.Now.FirstDayOfMonth(),
                        Format = "D"
                    };
                    
                }else if(sender.ObjectValue == "ENDDATE")
                {
                    control = new DatePicker()
                    {
                        Date = DateTime.Now.LastDayOfMonth(),
                        Format = "D"
                    };
                }
                else
                {
                    control = new DatePicker()
                    {
                        Date = DateTime.Now,
                        Format = "D"
                    };
                }

            }
            else if(sender.ObjectType == "CheckBox")
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
                Picker p = new Picker();
                p.ItemsSource = comboValues;
                p.SelectedIndex = 0;
                p.SelectedItem = p.Items[p.SelectedIndex];
                control = p;
                
                
          }

            control.BindingContext = sender;
            return control;
      }
   }
}
