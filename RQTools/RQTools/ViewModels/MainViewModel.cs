namespace RQTools.ViewModels
{
    using RQTools.Models;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    class MainViewModel : BaseViewModel
    {

        #region Properties
        public DeviceUser deviceUser { get; set; }
        public HospitalModel HospitalActual { get; set; }
        public List<HospitalModel> HospitalListlist { get; set; }
        public List<Products> ProductsNoCode { get; set; }
        public ObservableCollection<MenuItemViewModel> Menus { get; set; }
        public ObservableCollection<InventarioModelViewModel> InventarioActualMWM { get; set; }
        public string ComentariosDelInventario { get; set; }

        #endregion
        #region ViewModels
        public LoginViewModel Login { get; set; }
        public PrincipalViewModel Principal { get; set; }
        public HospitalViewModel Hospital { get; set; }
        public HospitalListViewModel HospitalList { get; set; }
        public InventarioViewModel Inventario { get; set; }
        public AddProductViewModel AddProduct { get; set; }
        public EditOrEliminateViewModel EditOrEliminate { get; set; }
        public InventarioFinalViewModel InventarioFinal { get; set; }
        #endregion
        #region Constructors
        public MainViewModel()
        {
            instance = this;
            this.Login = new LoginViewModel();
            this.InventarioActualMWM = new ObservableCollection<InventarioModelViewModel>();
            this.ProductsNoCode = new List<Products>();
            this.HospitalListlist = new List<HospitalModel>();
            this.LoadMenu();

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
        #region Methods

        private void LoadMenu()
        {
            this.Menus = new ObservableCollection<MenuItemViewModel>();

            this.Menus.Add(new MenuItemViewModel
            {
                Icon = "ic_bar_chart",
                PageName = "StaticsPage",
                Title = "Estadisticas",
            });

            this.Menus.Add(new MenuItemViewModel
            {
                Icon = "ic_update",
                PageName = "DataBase",
                Title = "Pendientes",
            });

            this.Menus.Add(new MenuItemViewModel
            {
                Icon = "ic_exit_to_app",
                PageName = "LoginPage",
                Title = "Cerrar Sesion",
            });

            

        }
        #endregion
    }
}
