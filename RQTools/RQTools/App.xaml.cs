using RQTools.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Linq;
using RQTools.Helpers;
using RQTools.Views;
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

            if (Settings.IsRemembered == "true")
            {
                using (var datos = new DataAccess())
                {
                    var deviceUsers = datos.GetDeviceUsers().FirstOrDefault();
                    if (deviceUsers != null)
                    {
                        var mainViewModel = MainViewModel.GetInstance();
                        Application.Current.MainPage = new PrincipalPage();
                    }
                    else
                    {
                        this.MainPage = new NavigationPage(new LoginPage());
                    }
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
