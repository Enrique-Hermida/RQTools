using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using RQTools.Helpers;
using RQTools.Views;
using RQTools.Services;
using RQTools.Models;
using RQTools.ViewModels;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace RQTools
{
    public partial class App : Application
    {
        #region Propiedades
        public static NavigationPage Navigator { get; internal set; }
        #endregion
        #region Constructores
        public App()
        {
            InitializeComponent();
            var mainViewModel = MainViewModel.GetInstance();
            if (Settings.IsRemembered == "true")
            {
                var dataService = new DataService();
                var user = dataService.First<DeviceUser>(false);
                if (user!= null)
                {
                    mainViewModel.deviceUser = user;
                    Application.Current.MainPage = new PrincipalPage();
                }
                else
                {
                    this.MainPage = new NavigationPage(new LoginPage());
                }      
            }
            else
            {
                this.MainPage = new NavigationPage(new LoginPage());
            }
        }

        #endregion
        #region Metodos
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
        #endregion
    }
}
