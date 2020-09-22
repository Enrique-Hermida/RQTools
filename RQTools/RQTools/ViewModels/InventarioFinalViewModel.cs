namespace RQTools.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using Plugin.Geolocator;
    using Plugin.Geolocator.Abstractions;
    using RQTools.Models;
    using RQTools.Services;
    using RQTools.Views;
    using System;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Windows.Input;
    using Xamarin.Forms;

    public class InventarioFinalViewModel :BaseViewModel
    {
        #region Services
        private ApiService apiService;
        #endregion
        #region Atributtes
        private string idInventario;
        private string idInv;
        private string fechainventario;
        private string usuarioinventario;
        private bool isRunning;
        private bool isEnabled;
        public bool isVisible;
        private IGeolocator locator;
        private MainViewModel mainViewModel = MainViewModel.GetInstance();
        private ProductsPiece prodcuts;
        private WebInvenatarioLote webInvenatario;
        #endregion
        #region Popierties
        public bool IsRunning
        {
            get { return this.isRunning; }
            set { SetValue(ref this.isRunning, value); }
        }
        public bool IsEnabled
        {
            get { return this.isEnabled; }
            set { this.SetValue(ref this.isEnabled, value); }
        }
        public bool IsVisible
        {
            get { return this.isVisible; }
            set { this.SetValue(ref this.isVisible, value); }
        }
        public ProductsPiece Prodcuts
        {
            get { return this.prodcuts; }
            set { SetValue(ref this.prodcuts, value); }
        }
        public ObservableCollection<InventarioModelViewModel> InventarioFinal
        {
            get;
            set;
        }
        public ObservableCollection<ProductsPiece> ListProductsPiece
        {
            get;
            set;
        }
        public WebInvenatarioLote WebInvenatario
        {
            get { return this.webInvenatario; }
            set { SetValue(ref this.webInvenatario, value); }
        }
        public string IdInventario
        {
            get { return this.idInv; }
            set { SetValue(ref this.idInv, value); }
        }
        public string FechaInventario
        {
            get { return this.fechainventario; }
            set { SetValue(ref this.fechainventario, value); }
        }
        public string UsuarioInventario
        {
            get { return this.usuarioinventario; }
            set { SetValue(ref this.usuarioinventario, value); }
        }
        #endregion
        #region Constructor
        public InventarioFinalViewModel()
        {
            this.apiService = new ApiService();
            this.Prodcuts = new ProductsPiece();
            this.IsEnabled = false;
            this.IsVisible = true;
            this.ListProductsPiece = new ObservableCollection<ProductsPiece>();
            this.InventarioFinal = mainViewModel.InventarioActualMWM;
            this.GenerateInvMaster();
        }
        #endregion        
        #region Commands
        public ICommand FinishCommand
        {
            get
            {
                return new RelayCommand(CargarWebServices);
            }
        }
        #endregion
        #region Methods

        private async void GenerateInvMaster()
        {
            //datos para los inventarios master 
            try
            {
                this.IsRunning = true;
                this.IsEnabled = false;

                DateTime Hoy = DateTime.Now;
                string fecha_actual = Hoy.ToString("yyyy-MM-dd");
                string hora_actual = Hoy.ToString("HH:mm:ss");
                string codehosp = mainViewModel.HospitalActual.Codigo_Hospital;
                this.idInventario = codehosp + "-" + fecha_actual + "-" + hora_actual;

                this.Prodcuts.Id_Inventario = this.idInventario;
                this.Prodcuts.Fecha = fecha_actual + " " + hora_actual;
                this.Prodcuts.Usuario = mainViewModel.deviceUser.ID_User;
                this.Prodcuts.Id_Hospital = mainViewModel.HospitalActual.ID_Hospital;
                if (string.IsNullOrEmpty(mainViewModel.ComentariosDelInventario))
                {
                    this.Prodcuts.Comentarios = "Inventario Completo";
                }
                this.Prodcuts.Comentarios = mainViewModel.ComentariosDelInventario;

                this.locator = CrossGeolocator.Current;
                locator.DesiredAccuracy = 50;
                if (locator.IsGeolocationAvailable)
                {
                    if (locator.IsGeolocationEnabled)
                    {
                        if (!locator.IsListening)
                        {

                            await locator.StartListeningAsync(TimeSpan.FromSeconds(5), 10, true);
                        }

                        var myPosition = await locator.GetPositionAsync();
                        var lo = myPosition.Longitude;
                        var la = myPosition.Latitude;
                        this.Prodcuts.Longitud = lo;
                        this.Prodcuts.Latitud = la;
                    }
                }

                if (string.IsNullOrEmpty(this.Prodcuts.Longitud.ToString()) && string.IsNullOrEmpty(this.Prodcuts.Longitud.ToString()))
                {
                    this.Prodcuts.Longitud = 0.00000;
                    this.Prodcuts.Latitud = 0.00000;
                }

                this.IdInventario = this.Prodcuts.Id_Inventario;
                this.UsuarioInventario = mainViewModel.deviceUser.Name_User;
                this.FechaInventario = Hoy.ToString("dd-MM-yyyy");

                this.IsRunning = false;
                this.IsEnabled = true;
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        private async void CargarWebServices()
        {
            //la lista de inventarios master se crea desde el inicio
            this.IsRunning = true;
            this.IsEnabled = false;

            var connection = await this.apiService.CheckConnection();
            if (!connection.IsSuccess)
            {
                this.IsRunning = false;
                this.IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "No hay conexion a Internet",
                    "Aceptar"  );
                return;
            }
            var responseMaster = await this.apiService.Post("http://ryqmty.dyndns.org:8181/",
                "apiRest/public/api/Inventarios",
                "/Master",
                Prodcuts);
            if (!responseMaster.IsSuccess)
            {
                this.IsRunning = false;
                this.IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert(
                    "Error MasterService",
                    responseMaster.Message,
                    "Aceptar");
                return;
            }
            for (int i = 0; i < mainViewModel.InventarioActualMWM.Count; i++)
            {
                var itemActual = mainViewModel.InventarioActualMWM[i];
                var productWeb = new WebInvenatarioLote
                {
                    Id_Inventario = this.idInventario,
                    Id_Producto = itemActual.Id_Producto,
                    Cantidad = itemActual.Cantidad,
                    Lote = itemActual.Lote,
                };
                var response = await this.apiService.Post(
                "http://ryqmty.dyndns.org:8181/",
                "apiRest/public/api/Inventarios",
                "/data",
                productWeb);
                if (!response.IsSuccess)
                {
                    this.IsRunning = false;
                    this.IsEnabled = true;
                    await Application.Current.MainPage.DisplayAlert(
                        "Error",
                        response.Message,
                        "Aceptar");
                    return;
                }
            }
            await Application.Current.MainPage.DisplayAlert(
                         "Exitoso",
                         "Inventario realizado ",
                         "Aceptar");
            
            this.IsRunning = false;
            this.IsEnabled = true;
            mainViewModel.InventarioActualMWM.Clear();
            mainViewModel.ComentariosDelInventario = "";
            mainViewModel.Principal = new PrincipalViewModel();
            Application.Current.MainPage = new MasterPage();

        }
  
        #endregion
    }
}
