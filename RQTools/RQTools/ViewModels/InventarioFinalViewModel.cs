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
        private bool isRunning;
        private bool isEnabled;
        public bool isVisible;
        private int CajaDializadores = 24;
        private int CajaLineas = 24;
        private int CajaGalones = 4;
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
            this.GenerateListWihtProducts();
        }
        #endregion
        #region Methods
        private void GenerateListWihtProducts()
        {
            this.IsRunning = true;

            #region Dialiozadores2.10
            var result = from producto in InventarioFinal where producto.Id_Producto == 1 select producto;
            if (result!=null)
            {
                foreach (var producto in result)
                {
                    this.Prodcuts.Dializadores2_10 = (this.Prodcuts.Dializadores2_10) + (producto.Cantidad)* CajaDializadores;                 
                }
                
            }
            result = from producto in InventarioFinal where producto.Id_Producto == 24 select producto;
            if (result != null)
            {
                foreach (var producto in result)
                {
                    this.Prodcuts.Dializadores2_10 = this.Prodcuts.Dializadores2_10 + producto.Cantidad;
                }

            }
            #endregion           
            #region Dialiozadores 1.90
            result = from producto in InventarioFinal where producto.Id_Producto == 2 select producto;
            if (result != null)
            {
                foreach (var producto in result)
                {
                    this.Prodcuts.Dializadores1_90 = (this.Prodcuts.Dializadores1_90) + (producto.Cantidad) * CajaDializadores;
                }

            }
            result = from producto in InventarioFinal where producto.Id_Producto == 25 select producto;
            if (result != null)
            {
                foreach (var producto in result)
                {
                    this.Prodcuts.Dializadores1_90 = this.Prodcuts.Dializadores1_90 + producto.Cantidad;
                }

            }
            #endregion
            #region Dialiozadores 1.70
            result = from producto in InventarioFinal where producto.Id_Producto == 3 select producto;
            if (result != null)
            {
                foreach (var producto in result)
                {
                    this.Prodcuts.Dializadores1_70 = (this.Prodcuts.Dializadores1_70) + (producto.Cantidad) * CajaDializadores;
                }

            }
            result = from producto in InventarioFinal where producto.Id_Producto == 26 select producto;
            if (result != null)
            {
                foreach (var producto in result)
                {
                    this.Prodcuts.Dializadores1_70 = this.Prodcuts.Dializadores1_70 + producto.Cantidad;
                }

            }
            #endregion
            #region Dialiozadores 1.50
            result = from producto in InventarioFinal where producto.Id_Producto == 4 select producto;
            if (result != null)
            {
                foreach (var producto in result)
                {
                    this.Prodcuts.Dializadores1_50 = (this.Prodcuts.Dializadores1_50) + (producto.Cantidad) * CajaDializadores;
                }

            }
            result = from producto in InventarioFinal where producto.Id_Producto == 27 select producto;
            if (result != null)
            {
                foreach (var producto in result)
                {
                    this.Prodcuts.Dializadores1_50 = this.Prodcuts.Dializadores1_50 + producto.Cantidad;
                }

            }
            #endregion
            #region Dialiozadores 1.30
            result = from producto in InventarioFinal where producto.Id_Producto == 5 select producto;
            if (result != null)
            {
                foreach (var producto in result)
                {
                    this.Prodcuts.Dializadores1_30 = (this.Prodcuts.Dializadores1_30) + (producto.Cantidad) * CajaDializadores;
                }

            }
            result = from producto in InventarioFinal where producto.Id_Producto == 28 select producto;
            if (result != null)
            {
                foreach (var producto in result)
                {
                    this.Prodcuts.Dializadores1_30 = this.Prodcuts.Dializadores1_30 + producto.Cantidad;
                }

            }
            #endregion
            #region Dialiozadores 1.10
            result = from producto in InventarioFinal where producto.Id_Producto == 6 select producto;
            if (result != null)
            {
                foreach (var producto in result)
                {
                    this.Prodcuts.Dializadores1_10 = (this.Prodcuts.Dializadores1_10) + (producto.Cantidad) * CajaDializadores;
                }

            }
            result = from producto in InventarioFinal where producto.Id_Producto == 29 select producto;
            if (result != null)
            {
                foreach (var producto in result)
                {
                    this.Prodcuts.Dializadores1_10 = this.Prodcuts.Dializadores1_10 + producto.Cantidad;
                }

            }
            #endregion
            #region Dialiozadores 0.90
            result = from producto in InventarioFinal where producto.Id_Producto == 7 select producto;
            if (result != null)
            {
                foreach (var producto in result)
                {
                    this.Prodcuts.Dializadores0_90 = (this.Prodcuts.Dializadores0_90) + (producto.Cantidad) * CajaDializadores;
                }

            }
            result = from producto in InventarioFinal where producto.Id_Producto == 30 select producto;
            if (result != null)
            {
                foreach (var producto in result)
                {
                    this.Prodcuts.Dializadores0_90 = this.Prodcuts.Dializadores0_90 + producto.Cantidad;
                }

            }
            #endregion
            #region Dialiozadores 0.70
            result = from producto in InventarioFinal where producto.Id_Producto == 8 select producto;
            if (result != null)
            {
                foreach (var producto in result)
                {
                    this.Prodcuts.Dializadores0_70 = (this.Prodcuts.Dializadores0_70) + (producto.Cantidad) * CajaDializadores;
                }

            }
            result = from producto in InventarioFinal where producto.Id_Producto == 31 select producto;
            if (result != null)
            {
                foreach (var producto in result)
                {
                    this.Prodcuts.Dializadores0_70 = this.Prodcuts.Dializadores0_70 + producto.Cantidad;
                }

            }
            #endregion
            #region Dialiozadores 0.50
            result = from producto in InventarioFinal where producto.Id_Producto == 9 select producto;
            if (result != null)
            {
                foreach (var producto in result)
                {
                    this.Prodcuts.Dializadores0_50 = (this.Prodcuts.Dializadores0_50) + (producto.Cantidad) * CajaDializadores;
                }

            }
            result = from producto in InventarioFinal where producto.Id_Producto == 32 select producto;
            if (result != null)
            {
                foreach (var producto in result)
                {
                    this.Prodcuts.Dializadores0_50 = this.Prodcuts.Dializadores0_50 + producto.Cantidad;
                }

            }
            #endregion
            #region Lineas Adulto
            result = from producto in InventarioFinal where producto.Id_Producto == 10 select producto;
            if (result != null)
            {
                foreach (var producto in result)
                {
                    this.Prodcuts.LineasAdulto = (this.Prodcuts.LineasAdulto) + (producto.Cantidad) * CajaLineas;
                }

            }
            result = from producto in InventarioFinal where producto.Id_Producto == 33 select producto;
            if (result != null)
            {
                foreach (var producto in result)
                {
                    this.Prodcuts.LineasAdulto = this.Prodcuts.LineasAdulto + producto.Cantidad;
                }

            }
            #endregion
            #region Lineas Pedriaticas
            result = from producto in InventarioFinal where producto.Id_Producto == 11 select producto;
            if (result != null)
            {
                foreach (var producto in result)
                {
                    this.Prodcuts.LineasPedriaticas = (this.Prodcuts.LineasPedriaticas) + (producto.Cantidad * CajaLineas);
                }

            }
            result = from producto in InventarioFinal where producto.Id_Producto == 34 select producto;
            if (result != null)
            {
                foreach (var producto in result)
                {
                    this.Prodcuts.LineasPedriaticas = this.Prodcuts.LineasPedriaticas + producto.Cantidad;
                }

            }
            #endregion
            #region Aquacid 120-2K
            result = from producto in InventarioFinal where producto.Id_Producto == 12 select producto;
            if (result != null)
            {
                foreach (var producto in result)
                {
                    this.Prodcuts.Aquacid2k = (this.Prodcuts.Aquacid2k) + (producto.Cantidad * CajaGalones);
                }

            }
            result = from producto in InventarioFinal where producto.Id_Producto == 35 select producto;
            if (result != null)
            {
                foreach (var producto in result)
                {
                    this.Prodcuts.Aquacid2k = this.Prodcuts.Aquacid2k + producto.Cantidad;
                }

            }
            #endregion
            #region Aquacid 220-0K
            result = from producto in InventarioFinal where producto.Id_Producto == 13 select producto;
            if (result != null)
            {
                foreach (var producto in result)
                {
                    this.Prodcuts.Aquacid0k = (this.Prodcuts.Aquacid0k) + (producto.Cantidad * CajaGalones);
                }

            }
            result = from producto in InventarioFinal where producto.Id_Producto == 36 select producto;
            if (result != null)
            {
                foreach (var producto in result)
                {
                    this.Prodcuts.Aquacid0k = this.Prodcuts.Aquacid0k + producto.Cantidad;
                }

            }
            #endregion
            #region Quabic
            result = from producto in InventarioFinal where producto.Id_Producto == 14 select producto;
            if (result != null)
            {
                foreach (var producto in result)
                {
                    this.Prodcuts.Quabic = (this.Prodcuts.Quabic) + (producto.Cantidad * CajaGalones);
                }

            }
            result = from producto in InventarioFinal where producto.Id_Producto == 37 select producto;
            if (result != null)
            {
                foreach (var producto in result)
                {
                    this.Prodcuts.Quabic = this.Prodcuts.Quabic + producto.Cantidad;
                }

            }
            #endregion
            #region Nipro Kart 650
            result = from producto in InventarioFinal where producto.Id_Producto == 15 select producto;
            if (result != null)
            {
                foreach (var producto in result)
                {
                    this.Prodcuts.Niprokart650 = (this.Prodcuts.Niprokart650) + (producto.Cantidad * CajaGalones);
                }

            }
            result = from producto in InventarioFinal where producto.Id_Producto == 38 select producto;
            if (result != null)
            {
                foreach (var producto in result)
                {
                    this.Prodcuts.Niprokart650 = this.Prodcuts.Niprokart650 + producto.Cantidad;
                }

            }
            #endregion
            #region  Kit CD Cateter
            result = from producto in InventarioFinal where producto.Id_Producto == 16 select producto;
            if (result != null)
            {
                foreach (var producto in result)
                {
                    this.Prodcuts.KitCateter = (this.Prodcuts.KitCateter) + (producto.Cantidad * 75);
                }

            }
            result = from producto in InventarioFinal where producto.Id_Producto == 39 select producto;
            if (result != null)
            {
                foreach (var producto in result)
                {
                    this.Prodcuts.KitCateter = this.Prodcuts.KitCateter + producto.Cantidad;
                }

            }
            #endregion
            #region  Kit CD Fistula
            result = from producto in InventarioFinal where producto.Id_Producto == 17 select producto;
            if (result != null)
            {
                foreach (var producto in result)
                {
                    this.Prodcuts.KitFistula = (this.Prodcuts.KitFistula) + (producto.Cantidad * 90);
                }

            }
            result = from producto in InventarioFinal where producto.Id_Producto == 40 select producto;
            if (result != null)
            {
                foreach (var producto in result)
                {
                    this.Prodcuts.KitFistula = this.Prodcuts.KitFistula + producto.Cantidad;
                }

            }
            #endregion
            #region Fistula 15
            result = from producto in InventarioFinal where producto.Id_Producto == 18 select producto;
            if (result != null)
            {
                foreach (var producto in result)
                {
                    this.Prodcuts.Fistula15 = (this.Prodcuts.Fistula15) + (producto.Cantidad * 50);
                }

            }
            result = from producto in InventarioFinal where producto.Id_Producto == 41 select producto;
            if (result != null)
            {
                foreach (var producto in result)
                {
                    this.Prodcuts.Fistula15 = this.Prodcuts.Fistula15 + producto.Cantidad;
                }

            }
            #endregion
            #region Fistula 16
            result = from producto in InventarioFinal where producto.Id_Producto == 19 select producto;
            if (result != null)
            {
                foreach (var producto in result)
                {
                    this.Prodcuts.Fistula16 = (this.Prodcuts.Fistula16) + (producto.Cantidad * 50);
                }

            }
            result = from producto in InventarioFinal where producto.Id_Producto == 42 select producto;
            if (result != null)
            {
                foreach (var producto in result)
                {
                    this.Prodcuts.Fistula16 = this.Prodcuts.Fistula16 + producto.Cantidad;
                }

            }
            #endregion
            #region Fistula 17
            result = from producto in InventarioFinal where producto.Id_Producto == 20 select producto;
            if (result != null)
            {
                foreach (var producto in result)
                {
                    this.Prodcuts.Fistula17 = (this.Prodcuts.Fistula17) + (producto.Cantidad * 50);
                }

            }
            result = from producto in InventarioFinal where producto.Id_Producto == 43 select producto;
            if (result != null)
            {
                foreach (var producto in result)
                {
                    this.Prodcuts.Fistula17 = this.Prodcuts.Fistula17 + producto.Cantidad;
                }

            }
            #endregion
            #region Aquacetic
            result = from producto in InventarioFinal where producto.Id_Producto == 21 select producto;
            if (result != null)
            {
                foreach (var producto in result)
                {
                    this.Prodcuts.Aquacetic = (this.Prodcuts.Aquacetic) + (producto.Cantidad * CajaGalones);
                }

            }
            result = from producto in InventarioFinal where producto.Id_Producto == 44 select producto;
            if (result != null)
            {
                foreach (var producto in result)
                {
                    this.Prodcuts.Aquacetic = this.Prodcuts.Aquacetic + producto.Cantidad;
                }

            }
            #endregion
            #region Cloro
            result = from producto in InventarioFinal where producto.Id_Producto == 22 select producto;
            if (result != null)
            {
                foreach (var producto in result)
                {
                    this.Prodcuts.Aquacetic = (this.Prodcuts.Aquacetic) + (producto.Cantidad * 15);
                }

            }
            result = from producto in InventarioFinal where producto.Id_Producto == 45 select producto;
            if (result != null)
            {
                foreach (var producto in result)
                {
                    this.Prodcuts.Aquacetic = this.Prodcuts.Aquacetic + producto.Cantidad;
                }

            }
            #endregion
            #region Sal
            result = from producto in InventarioFinal where producto.Id_Producto == 46 select producto;
            if (result != null)
            {
                foreach (var producto in result)
                {
                    this.Prodcuts.Sal = (this.Prodcuts.Sal) + producto.Cantidad;
                }

            }
            #endregion
            #region Aquacid 3.5 ca
            result = from producto in InventarioFinal where producto.Id_Producto == 23 select producto;
            if (result != null)
            {
                foreach (var producto in result)
                {
                    this.Prodcuts.Aquacid35CA = (this.Prodcuts.Aquacid35CA) + (producto.Cantidad * CajaGalones);
                }

            }
            result = from producto in InventarioFinal where producto.Id_Producto == 47 select producto;
            if (result != null)
            {
                foreach (var producto in result)
                {
                    this.Prodcuts.Aquacid35CA = this.Prodcuts.Aquacid35CA + producto.Cantidad;
                }

            }
            #endregion
            this.ListProductsPiece.Add(Prodcuts);
            this.IsRunning = false;
            this.IsVisible = false;
            this.isEnabled = true;
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
        private async void CargarWebServices()
        {
            this.GenerateHeaders();
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
            var responseMaster = await this.apiService.Post("http://ryqmty.dyndns.org:8181",
                "/apiRest/public/api/Inventarios",
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
                    
                    Producto = itemActual.Producto,
                    Id_Inventario = this.idInventario,
                    Id_Producto = itemActual.Clave,
                    Cantidad = itemActual.Cantidad,
                    Lote = itemActual.Lote,
                };
                var response = await this.apiService.Post(
                "http://ryqmty.dyndns.org:8181",
                "/apiRest/public/api/Inventarios",
                "/nuevo",
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

        private async void GenerateHeaders()
        {
            //codigo hospital mas fecha y hora
            try
            {
                DateTime Hoy = DateTime.Now;
                string fecha_actual = Hoy.ToString("yyyy-MM-dd");
                string hora_actual = Hoy.ToString("HH:mm:ss");
                string codehosp = mainViewModel.HospitalActual.Codigo_Hospital;
                this.idInventario = codehosp + "-" + fecha_actual + "-" + hora_actual;

                this.Prodcuts.Id_Inventario = this.idInventario;
                this.Prodcuts.Fecha = fecha_actual+" "+hora_actual;
                this.Prodcuts.Inventariador = mainViewModel.deviceUser.Name_User;
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
            }
            catch (Exception)
            {

                throw;
            }                      
        }

       
        #endregion
    }
}
