using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using GazellaMobile.Droid;
using GazellaMobile.Views.CustomControls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(ButtonEntry), typeof(ButtonEntryRenderer))]
namespace GazellaMobile.Droid
{
    public class ButtonEntryRenderer : ViewRenderer<ButtonEntry, ButtonEditText>
    {

        protected override void OnElementChanged(ElementChangedEventArgs<ButtonEntry> e)
        {


            base.OnElementChanged(e);

            if (Control == null)
            {

                SetNativeControl(new ButtonEditText(Context));


            }

            if (e.NewElement != null)
            {
                Control.InputType = Android.Text.InputTypes.TextFlagNoSuggestions;
                SetText();
                SetRightImage();
                SetLeftImage();
                Control.RightClick += RightClickHandler;
                Control.LeftClick += LeftClickHandler;
                Control.TextChanged += Control_TextChanged;

            }
            else
            {
                Control.RightClick -= RightClickHandler;
                Control.LeftClick -= LeftClickHandler;
                Control.TextChanged -= Control_TextChanged;
            }



        }

        private void Control_TextChanged(object sender, Android.Text.TextChangedEventArgs e)
        {
            ButtonEditText editText = (ButtonEditText)sender;
            Element.CustomText = editText.Text;
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == ButtonEntry.CustomTextProperty.PropertyName && Element.CustomText != Control.Text)
            {
                SetText();
            }

            if (e.PropertyName == ButtonEntry.RightImageProperty.PropertyName)
            {
                SetRightImage();
            }

            if (e.PropertyName == ButtonEntry.LeftImageProperty.PropertyName)
            {
                SetLeftImage();
            }


        }


        void SetText()
        {
            Control.Text = Element.CustomText;
        }

        void SetRightImage()
        {

            if (!string.IsNullOrEmpty(Element.RightImage))
            {
                int rightImage = Resources.GetIdentifier(Element.RightImage, "drawable", Context.PackageName);
                Drawable rDrawable = Resources.GetDrawable(rightImage);
                var drawables = Control.GetCompoundDrawables();
                var lDrawable = drawables[0];
                Control.SetCompoundDrawablesWithIntrinsicBounds(lDrawable, null, rDrawable, null);

            }
        }

        void SetLeftImage()
        {

            if (!string.IsNullOrEmpty(Element.LeftImage))
            {
                int leftImage = Resources.GetIdentifier(Element.LeftImage, "drawable", Context.PackageName);
                Drawable lDrawable = Resources.GetDrawable(leftImage);
                var drawables = Control.GetCompoundDrawables();
                var rDrawable = drawables[2];
                Control.SetCompoundDrawablesWithIntrinsicBounds(lDrawable, null, rDrawable, null);
            }


        }


        void RightClickHandler(object sender, EventArgs e)
        {
            if (Element.RightClick != null)
                Element.RightClick(this.Element, EventArgs.Empty);
        }

        void LeftClickHandler(object sender, EventArgs e)
        {
            if (Element.LeftClick != null)
                Element.LeftClick(this.Element, EventArgs.Empty);
        }
    }
}