using System;
using CoreGraphics;
using UIKit;
using Xamarin.Forms.Platform.iOS;
using Xamarin.Forms;
using GazellaMobile.Views.CustomControls;
using GazellaMobile.iOS;

[assembly:ExportRenderer(typeof(ButtonEntry),typeof(ButtonEntryRenderer))]
namespace GazellaMobile.iOS
{
    
    public class ButtonEntryRenderer: EntryRenderer
    {
       
        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Entry> e)
        {
            base.OnElementChanged(e);
            
            if(Control!= null){

                var element = (ButtonEntry)this.Element;
                UIButton overlayButton = UIButton.FromType(UIButtonType.System);
                overlayButton.Frame = new CGRect(0, 0, 25, 25);
                overlayButton.SetImage(UIImage.FromFile(element.Image), UIControlState.Normal);
                overlayButton.TouchUpInside += (sender, args) =>
                {
                   element.Command.Execute(null); 
                };
                this.Control.RightViewMode = UITextFieldViewMode.Always;
                this.Control.RightView = overlayButton; 
            }


        }

    }
}
