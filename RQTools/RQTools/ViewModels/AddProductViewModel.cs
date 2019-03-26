namespace RQTools.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using RQTools.Models;
    using RQTools.Services;
    using RQTools.Views;
    using System;
    using System.Linq;
    using System.Windows.Input;
    using Xamarin.Forms;

    public class AddProductViewModel : BaseViewModel
    {
        #region Services
        private DataServices dataService;
        #endregion
        #region Attributes
        private int cantidad;
        private int idlist;
        private string lote;
        private string nombreProducto;
        private bool isEnabled;
        private MainViewModel mainViewModel = MainViewModel.GetInstance();
        private InventarioModel itemInventario;
        #endregion
        #region Properties
        public int Cantidad
        {
            get { return this.cantidad; }
            set { SetValue(ref this.cantidad, value); }
        }
        public int IdList
        {
            get { return this.idlist; }
            set { SetValue(ref this.idlist, value); }
        }
        public string Lote
        {
            get { return this.lote; }
            set { SetValue(ref this.lote, value); }
        }
        public string NombreProducto
        {
            get { return this.nombreProducto; }
            set { SetValue(ref this.nombreProducto, value); }
        }
        public bool IsEnabled
        {
            get { return this.isEnabled; }
            set { SetValue(ref this.isEnabled, value); }
        }
        public InventarioModel ItemInventario
        {
            get { return this.itemInventario; }
            set { SetValue(ref this.itemInventario, value); }
        }
        public Products Product
        {
            get;
            set;
        }
        #endregion
        #region Constructors
        public AddProductViewModel(Products products)
        {
            this.Product = new Products();
            this.dataService = new DataServices();
            this.isEnabled = true;
            this.Product = products;
            this.NombreProducto = this.Product.Nombre_Producto.ToString();

        }
        #endregion
        #region Commands
        public ICommand AddtoDBCommand
        {
            get
            {
                return new RelayCommand(AddtoDB);
            }
        }
        #endregion
        #region Methods
        private async void AddtoDB()
        {
            this.IsEnabled = false;
            if (string.IsNullOrEmpty(this.Lote))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "Debes ingresar el lote del producto seleccionado",
                    "Aceptar");
                return;
            }
            if (string.IsNullOrEmpty(this.Cantidad.ToString()))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "Debes ingresar la cantidad del producto seleccionado",
                    "Aceptar");
                return;
            }
            if (this.Cantidad<=0)
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "Debes ingresar cantidades mayores a 0",
                    "Aceptar");
                return;
            }
            if (this.Cantidad > 8000)
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Advertencia",
                    "Estas Ingresando Cantidades Mayores a 8000 ,Estas seguro",
                    "Aceptar");
                return;
            }

            #region BuscarID
            if (mainViewModel.InventarioActualMWM!=null)
            {
                IdList = mainViewModel.InventarioActualMWM.Count;
            }
            else
            {
                IdList = 0;
            }
            
            #endregion
            mainViewModel.InventarioActualMWM.Add(new InventarioModel
            {
                Id = this.IdList,
                Producto = this.Product.Nombre_Producto,
                Id_Producto = this.Product.ID_Producto,
                Cantidad = this.Cantidad,
                Lote = this.Lote,
            });
            mainViewModel.Inventario = new InventarioViewModel();
            await App.Navigator.PushAsync(new InventarioTabbedPage());

        }

        #endregion
    }
}
