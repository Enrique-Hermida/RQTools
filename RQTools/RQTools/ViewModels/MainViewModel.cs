namespace RQTools.ViewModels
{
    using RQTools.Models;
    class MainViewModel : BaseViewModel
    {
        #region Attibrutes
        private DeviceUser user;
        #endregion
        #region Properties
        public DeviceUser LocalUser
        {
            get { return this.user; }
            set { SetValue(ref this.user, value); }
        }
        #endregion
        #region ViewModels
        public LoginViewModel Login { get; set; }
        public PrincipalViewModel Principal { get; set; }
        public HospitalViewModel Hospital { get; set; }
        #endregion
        #region Constructors
        public MainViewModel()
        {
            instance = this;
            this.Login = new LoginViewModel();
        }
        #endregion
        #region Singleton
        private static MainViewModel instance;

        public static MainViewModel GetInstance()
        {
            if (instance==null)
            {
                return new MainViewModel();
            }
            return instance;
        }

        #endregion
    }
}
