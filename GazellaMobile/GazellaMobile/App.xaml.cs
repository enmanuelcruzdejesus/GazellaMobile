
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;
using GazellaMobile.Helpers;
using ClubNaco.Models;
using Acr.UserDialogs;
using System.Threading.Tasks;

namespace GazellaMobile
{
    public partial class App : Application
    {
        #region FIELDS
        static string BASE_URL = "http://10.0.0.8:60590/api/{0}/{1}";
        static GDSServiceClient _serviceClient = null;
        static User _currentUser = null;
        static bool _isLogin = false;
        static bool _allowKeepLog = false;
        #endregion

        #region ATTRIBUTES
        public static string DbFileName { get { return "GazellaMobile.sqlite"; } }
       
        public static GDSServiceClient ServiceClient
        {
            get
            {
                if (_serviceClient == null)
                {
                    _serviceClient = new GDSServiceClient(BASE_URL);
                    return _serviceClient;
                }
                return _serviceClient;
            }
        }
        public static User CurrentUser
        {
            get
            {
                if (_currentUser == null)
                {
                    _currentUser = new User();
                    return _currentUser;
                }
                return _currentUser;
            }
        }
        public static bool isLogin
        {
            get
            {
                return _isLogin;
            }
        }
        public static bool AllowKeepLog
        {
            get { return _allowKeepLog; }
            set
            {
                if (value != _allowKeepLog)
                    _allowKeepLog = value;
            }
        }

        public static void ToastDialog(string msg, double time = 2000)
        {
            ToastConfig.DefaultBackgroundColor = System.Drawing.Color.Red;
            ToastConfig.DefaultActionTextColor = System.Drawing.Color.White;
            UserDialogs.Instance.Toast(msg, TimeSpan.FromMilliseconds(time));
        }
        public static async Task<PromptResult> PromptDialog(string msg, InputType input, string cancelText, bool cancel = true)
        {
            var promptConfig = new PromptConfig();
            promptConfig.InputType = InputType.Email;
            promptConfig.IsCancellable = true;
            promptConfig.Message = msg;
            promptConfig.SetCancelText(cancelText);
            var result = await UserDialogs.Instance.PromptAsync(promptConfig);
            return result;
        }
        public static async Task<LoginResult> LoginDialog(string title)
        {
            LoginConfig logConfig = new LoginConfig();
            logConfig.Title = title;
            var logResult = await UserDialogs.Instance.LoginAsync(logConfig);
            return logResult;
        }


        public App()
        {
            InitializeComponent();

            MainPage = new GazellaMobile.MainPage();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
