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


        public string DescripStatus
        {
            get
            {
                return _auth.DescripStatus.ToString();

            }
        }

        public string Detalle
        {
            get
            {
                return _auth.AuthDetails.ToString();

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
                  Convert.ToInt32(AuthId),
                  true,
                  ""
                );
            var responseMessage = await App.ServiceClient.AuthConfirmationResponse(auth);
            UserDialogs.Instance.ShowSuccess(responseMessage);

        }
        private async void OnCancel()
        {       AuthConfirmation auth = new AuthConfirmation
                (
                  App.CurrentUser.UserId,
                  Convert.ToInt32(AuthId),
                  false,
                  ""
                );
            var responseMessage = await App.ServiceClient.AuthConfirmationResponse(auth);
            UserDialogs.Instance.ShowSuccess(responseMessage);
        }



        
    }
}
