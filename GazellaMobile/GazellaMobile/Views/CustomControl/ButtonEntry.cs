using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace GazellaMobile.Views.CustomControls    
{
    public class ButtonEntry : Entry
    {
        public static BindableProperty ImageProperty = BindableProperty.Create(nameof(Image), typeof(string), typeof(ButtonEntry),string.Empty);

        public static BindableProperty CommandProperty = BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(ButtonEntry), null);

        public string Image 
        {
            get
            {
                return GetValue(ImageProperty).ToString();   
            }
            set
            {
               SetValue(ImageProperty,value);   
            }
        }

        public ICommand Command
        {
            get
            {
                return (Command)GetValue(CommandProperty);   
            }

            set
            {
                SetValue(CommandProperty,value);    
            }
        }




        public ButtonEntry()
        {
           
        }
    }
}
