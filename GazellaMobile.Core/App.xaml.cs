
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using Xamarin.Forms;

using Acr.UserDialogs;
using Newtonsoft.Json;
using SQLite;
using Plugin.Connectivity;

using GazellaMobile.Models;
using GazellaMobile.Helpers;
using GazellaMobile.Views;
using GazellaMobile.Interfaces;
using GazellaMobile.Utils.Services;



namespace GazellaMobile
{
    public partial class App : Application
    {
        #region FIELDS       
        static string SERVER_NAME = "http://{0}/";
        static string BASE_URL = "gazellamobileapi/api/{0}/{1}";
        static string _uri;
        static bool _isLogin = false;
        static DataServiceHelper _serviceClient = null;
        static GDSServiceClient _serviceClient2 = null;
        static User _currentUser = null;     
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
        public static bool isLogin
        {
            get
            {
                return _isLogin;
            }
        }

        public static bool IsServerConfigured
        {
            get
            {
               return DbConnection.Table<AppSettings>().Count()>0;
               
            }
        }
     
        public static DataServiceHelper ServiceClient
        {
            get
            {
                if (_serviceClient == null)
                {
                    _serviceClient = new DataServiceHelper(new ResilienceHttpClient(Uri));
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
            get 
            {
                if(_settings == null)
                {
                    _settings = new AppSettings();
                    return _settings;
                }
                return _settings;
            }
            set
            {
                _settings = value;
            }

        }
        public static int Cia { get { return 1; } }
  
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
                var content = await response.Content.ReadAsStringAsync();
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
            if(IsServerConfigured)
            {
                if (_settings.AllowKeepLog)
                {
                    //Load User
                    var jsonString = DependencyService.Get<ISaveAndLoad>().LoadText("temp.txt");
                    _currentUser = JsonConvert.DeserializeObject<User>(jsonString);
                    _isLogin = true;
                    PresentMainPage();
                    return;
                }
                
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
            // Saving some data
            if(App.Settings.AllowKeepLog)
            {
                _settings.AllowKeepLog = false;
                DbConnection.Update(_settings);
            }

            InitializingParams();
            await Task.Delay(2000);
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

            CrossConnectivity.Current.ConnectivityTypeChanged += (sender, args) =>
            {
                Debug.WriteLine($"Connectivity changed to {args.IsConnected} ");
                foreach (var t in args.ConnectionTypes)
                    Debug.WriteLine($"Connection Type {t} ");

                
                if (!args.IsConnected)
                {

                    if (this.MainPage.Navigation.ModalStack.Count() == 0)
                        this.MainPage.Navigation.PushModalAsync(new WhenThereIsNoConnectionPage());

                }
                else
                {
                    this.MainPage.Navigation.PopModalAsync();


                }

            };


        }
        #endregion

        private static void InitializingParams()
        {
            if(IsServerConfigured)
            {
                var db = DependencyService.Get<ISQLConnection>().GetConnection();
                _settings = db.Table<AppSettings>().FirstOrDefault();
                SERVER_NAME = string.Format(SERVER_NAME, _settings.Server).Trim();
                _uri = SERVER_NAME + BASE_URL;
                db.Dispose();
            }
          
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
