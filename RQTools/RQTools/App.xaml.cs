using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using RQTools.Helpers;
using RQTools.Views;
using RQTools.ViewModels;
using RQTools.Services;
using RQTools.Models;
using System;

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
            this.MainPage = new NavigationPage(new LoginPage());
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
