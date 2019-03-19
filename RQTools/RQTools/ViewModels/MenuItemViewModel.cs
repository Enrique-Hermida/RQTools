namespace RQTools.ViewModels
{
    using System.Windows.Input;
    using GalaSoft.MvvmLight.Command;
    using Views;
    using Xamarin.Forms;
    using Helpers;
    class MenuItemViewModel
    {
        #region Properties
        public string Icon { get; set; }

        public string Title { get; set; }

        public string PageName { get; set; }
        #endregion
        #region Commands
        public ICommand NavigateCommand
        {
            get
            {
                return new RelayCommand(Navigate);
            }
        }

        private void Navigate()
        {
            App.Master.IsPresented = false;

            if (this.PageName == "LoginPage")
            {
                Settings.IsRemembered = "false";
                var mainViewModel = MainViewModel.GetInstance();
 
                Application.Current.MainPage = new NavigationPage(
                    new LoginPage());
            }

        }
        #endregion
    }
}
