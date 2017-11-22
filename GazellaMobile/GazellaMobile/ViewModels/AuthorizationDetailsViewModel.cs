using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Acr.UserDialogs;
using GazellaMobile.Helpers;
using GazellaMobile.Models;
using Xamarin.Forms;


namespace GazellaMobile.ViewModels
{
    public class AuthorizationDetailsViewModel
    {

        dynamic _auth;
        string _approvalCommments = "";
        ICommand _acceptCommand;
        ICommand _cancelCommand;
       
        public AuthorizationDetailsViewModel(dynamic authorization)
        {
            _auth = authorization;
        }
        
        

        public int AuthId
        {
            get
            {
                return Convert.ToInt32(_auth.AuthId);

            }

        }

        public string CompanyName
        {
            get
            {
                return _auth.CompanyName.ToString();
                
            }
        }

        public string Description
        {
            get
            {
                return _auth.Description.ToString();
                
            }
        }

        public string AuthDetail1
        {
            get
            {
                return _auth.AuthDetail1.ToString();

            }
        }

        public string AuthDetail2
        {
            get
            {
                return _auth.AuthDetail2.ToString();

            }
        }

        public string RequestDate
        {
            get
            {
                return _auth.RequestDate.ToString();
               
            }
        }

        public string RequestBy
        {
            get
            {
                return _auth.RequestBy.ToString();
                
            }
        }

  
        public string Comments
        {
            get
            {
                return _auth.Comments.ToString();   
            }
        }

        public string ApprovalComments
        {
            get
            {
                return _approvalCommments;
            }
            set
            {
                _approvalCommments = value;
            }
           
          
        }


        public ICommand AcceptCommand 
        {
            get
            {
                _acceptCommand = _acceptCommand ?? new Command(OnAccept);
                return _acceptCommand;
            }
            
        }

        public ICommand CancelCommand
        {
            get
            {
                _cancelCommand = _cancelCommand ?? new Command(OnCancel);
                return _cancelCommand;
            }

        }


        private async void OnAccept()
        {

            AuthConfirmation auth = new AuthConfirmation
              (
                App.CurrentUser.UserId,
                AuthId,
                true,
                ApprovalComments
                );

            var responseMessage = await App.ServiceClient.AuthConfirmationResponse(auth);
            UserDialogs.Instance.ShowSuccess(responseMessage);
            await Task.Delay(2000);
            TabbedPage page = (TabbedPage)App.Current.MainPage;
            await page.Children[0].Navigation.PopAsync();



        }
        private async void OnCancel()
        {           
            AuthConfirmation auth = new AuthConfirmation
                (
                  App.CurrentUser.UserId,
                  AuthId,
                  false,
                  ApprovalComments
                );
            var responseMessage = await App.ServiceClient.AuthConfirmationResponse(auth);
            UserDialogs.Instance.ShowSuccess(responseMessage);
            await Task.Delay(2000);
            TabbedPage page = (TabbedPage)App.Current.MainPage;
            await page.Children[0].Navigation.PopAsync();


        }



        
    }
}
