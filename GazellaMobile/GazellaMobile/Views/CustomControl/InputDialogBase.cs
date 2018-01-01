using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Rg.Plugins.Popup.Pages;
using Xamarin.Forms;

namespace GazellaMobile.Views.CustomControls
{
    public class InputDialogBase<T>: PopupPage
    {
        //the awaitable task
       public Task<T> PageClosedTask { get
            {
                return PageClosedComplitionSource.Task;
            } }
       //the task completion source
       public TaskCompletionSource<T> PageClosedComplitionSource { get; set; }
        public InputDialogBase(View contentBody)
        {
            Content = contentBody;
            //this.BackgroundColor = new Color(0, 0, 0, 0.4);
            this.PageClosedComplitionSource = new TaskCompletionSource<T>();

        }
        protected override Task OnAppearingAnimationEnd()
        {
            return Content.FadeTo(1);

        }
        protected override Task OnDisappearingAnimationBegin()
        {
            return Content.FadeTo(1);
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }

        protected override bool OnBackgroundClicked()
        {
            return false;
        }
    }
}
