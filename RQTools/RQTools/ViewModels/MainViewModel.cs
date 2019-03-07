namespace RQTools.ViewModels
{
    using RQTools.Models;
    using System.Collections.Generic;

    class MainViewModel : BaseViewModel
    {
        #region Attibrutes

        #endregion
        #region Properties
        public DeviceUser deviceUser { get; set; }
        public List<HospitalModel> HospitalListlist { get; set; }

        #endregion
        #region ViewModels
        public LoginViewModel Login { get; set; }
        public PrincipalViewModel Principal { get; set; }
        public HospitalViewModel Hospital { get; set; }
        public HospitalListViewModel HospitalList { get; set; }
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
