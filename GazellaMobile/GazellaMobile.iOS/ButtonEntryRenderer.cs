using System;
using CoreGraphics;
using GazellaMobile.iOS;
using GazellaMobile.Views.CustomControls;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;


[assembly: ExportRenderer(typeof(ButtonEntry), typeof(ButtonEntryRenderer))]
namespace GazellaMobile.iOS
{
    public class ButtonEntryRenderer : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                var element = (ButtonEntry)this.Element;


                if (!string.IsNullOrEmpty(element.RightImage))
                {
                    UIButton rightButton = UIButton.FromType(UIButtonType.System);
                    rightButton.Frame = new CGRect(0, 0, 25, 25);
                    rightButton.SetImage(UIImage.FromFile(element.RightImage), UIControlState.Normal);
                    rightButton.TouchUpInside += (sender, args) =>
                    {
                        var textfield = (ButtonEntry)this.Element;
                        if (textfield.RightClick != null)
                            textfield.RightClick(textfield, EventArgs.Empty);
                    };


                    this.Control.RightViewMode = UITextFieldViewMode.Always;
                    this.Control.RightView = rightButton;
                }

                if (!string.IsNullOrEmpty(element.LeftImage))
                {
                    UIButton leftButton = UIButton.FromType(UIButtonType.System);
                    leftButton.Frame = new CGRect(0, 0, 25, 25);
                    leftButton.SetImage(UIImage.FromFile(element.LeftImage), UIControlState.Normal);
                    leftButton.TouchUpInside += (sender, args) =>
                    {
                        var textfield = (ButtonEntry)this.Element;
                        if (textfield.LeftClick != null)
                            textfield.LeftClick(textfield, EventArgs.Empty);
                    };


                    this.Control.LeftViewMode = UITextFieldViewMode.Always;
                    this.Control.LeftView = leftButton;

                }

            }
        }
    }
}
