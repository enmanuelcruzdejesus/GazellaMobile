
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

using Acr.UserDialogs;
using Newtonsoft.Json;
using SQLite;

using GazellaMobile.Models;
using GazellaMobile.Helpers;
using GazellaMobile.Views;
using GazellaMobile.Interfaces;

namespace GazellaMobile
{
    public partial class App : Application
    {
        #region FIELDS       
        static string SERVER_NAME = "http://{0}/";
        static string BASE_URL = "gazellamobileapi/api/{0}/{1}";
        static string _uri;
        static DataServiceHelper _serviceClient = null;
        static GDSServiceClient _serviceClient2 = null;
        static User _currentUser = null;
        static bool _isLogin = false;
        static bool _allowKeepLog = false;
        static SQLiteConnection _dbConnection = null;
        static AppSettings _settings = null;
    

        #endregion

        #region ATTRIBUTES
        public static string Uri
        {
            get
            {
                return _uri;
            }
            set
            {
                _uri = value;
            }
        }
        public static string DbName { get { return "GazellaMobile"; } }
        public static string DbFileName { get { return "GazellaMobile.db3"; } }       
        public static DataServiceHelper ServiceClient
        {
            get
            {
                if (_serviceClient == null)
                {
                    _serviceClient = new DataServiceHelper(Uri);
                    return _serviceClient;
                }
                return _serviceClient;
            }
        }
        public static SQLiteConnection DbConnection
        {
            get
            {
                if (_dbConnection == null)
                {
                    _dbConnection = DependencyService.Get<ISQLConnection>().GetConnection();
                    return _dbConnection;
                }
                return _dbConnection;
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
        public static AppSettings Settings
        {
            get { return _settings; }
            set
            {
                _settings = value;
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

        #region Static Methods
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
            _serviceClient2 = new GDSServiceClient(Uri);

            var response = await _serviceClient2.Post<User>("Login", user);
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
        public async static void LogOut()
        {
            UserDialogs.Instance.ShowLoading("Cerrando Sesión", MaskType.Black);                  
            _serviceClient = null;
            _serviceClient2 = null;
            _currentUser = null;
            _isLogin = false;
            _allowKeepLog = false;
            // Saving some data
            await Task.Delay(2000);
            //Re-initializing params
            InitializingParams();
            UserDialogs.Instance.HideLoading();
            PresentLoginPage();

        }
        #endregion

        #region CTOR
        static App()
        {
            //Initializing static variables
            InitializingParams();
                    
            
        }
        public App()
        {
            InitializeComponent();
            PresentLoginPage();

        }
        #endregion
 
        private static void InitializingParams()
        {
            var db = DependencyService.Get<ISQLConnection>().GetConnection();
            _settings = db.Table<AppSettings>().FirstOrDefault();
            SERVER_NAME = string.Format(SERVER_NAME, _settings.Server).Trim();          
            _uri = SERVER_NAME + BASE_URL;            
            _allowKeepLog = _settings.AllowKeepLog;
            db.Dispose();
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
