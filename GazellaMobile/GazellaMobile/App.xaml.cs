
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

using Acr.UserDialogs;
using Newtonsoft.Json;

using GazellaMobile.Models;
using GazellaMobile.Helpers;
using GazellaMobile.Views;

namespace GazellaMobile
{
    public partial class App : Application
    {
        #region FIELDS
        static string BASE_URL = "http://10.0.0.8:50931/api/{0}/{1}";   
        static DataServiceHelper _serviceClient = null;
        static GDSServiceClient _service = null;
        static User _currentUser = null;
        static bool _isLogin = false;
        static bool _allowKeepLog = false;
        #endregion

        #region ATTRIBUTES
        public static string DbFileName { get { return "GazellaMobile.sqlite"; } }       
        public static DataServiceHelper ServiceClient
        {
            get
            {
                if (_serviceClient == null)
                {
                    _serviceClient = new DataServiceHelper(BASE_URL);
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
        #endregion

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
        public static async Task<LoginStatus> isLoginSuccesFulAsync(User user)
        {
            _service = new GDSServiceClient(BASE_URL);
            
            var response = await _service.Post<User>("Login", user);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var status = JsonConvert.DeserializeObject<LoginStatus>(content);
                _isLogin = true;
                _currentUser = user;
                return status;
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                var content = await response.Content.ReadAsStringAsync();
                var status = JsonConvert.DeserializeObject<LoginStatus>(content);
                return status;
            }
            else
            {
                //WHEN THE SERVER RAISE AN EXCEPTION
                var content = response.Content.ReadAsStringAsync().Result;
                dynamic ex = JsonConvert.DeserializeObject(content);
                return new LoginStatus(false, ex.message);
            }
            
         
        }
        public static void PresentMainPage()
        {
            if (_isLogin)
                Current.MainPage = new MainPage();
        }
        public static void PresentLoginPage()
        {
            if (_allowKeepLog)
            {
                _isLogin = true;
                PresentMainPage();
                return;
            }

            Current.MainPage = new LoginPage();
        }


        public App()
        {
            InitializeComponent();            

            PresentLoginPage();

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
