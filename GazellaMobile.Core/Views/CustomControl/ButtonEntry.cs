using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace GazellaMobile.Views.CustomControls
{
    public class ButtonEntry : Entry
    {

        public EventHandler RightClick;
        public EventHandler LeftClick;

        public static BindableProperty RightImageProperty = BindableProperty.Create(nameof(RightImage), typeof(string), typeof(ButtonEntry), string.Empty);
        public static BindableProperty LeftImageProperty = BindableProperty.Create(nameof(LeftImage), typeof(string), typeof(ButtonEntry), string.Empty);
        public static BindableProperty CustomTextProperty = BindableProperty.Create(nameof(CustomText), typeof(string), typeof(ButtonEntry), string.Empty);

        public ButtonEntry()
        {
            this.TextChanged += (sender, e) =>
            {
                Device.OnPlatform(iOS: () => CustomText = Text);
            };
        }


        public string CustomText
        {
            get
            {
                return Device.OnPlatform<string>(this.Text, GetValue(CustomTextProperty).ToString(), this.Text);

            }

            set
            {
                Device.OnPlatform(iOS: () => this.Text = value, Android: () => SetValue(CustomTextProperty, value));
            }
        }


        public string RightImage
        {
            get
            {
                return GetValue(RightImageProperty).ToString();
            }
            set
            {
                if(RightImage!=value  && !string.IsNullOrEmpty(value))
                SetValue(RightImageProperty, value);
            }
        }


        public string LeftImage
        {
            get
            {
                return GetValue(LeftImageProperty).ToString();
            }
            set
            {
                if (LeftImage != value && !string.IsNullOrEmpty(value))
                    SetValue(LeftImageProperty, value);
            }
        }
    }
}
