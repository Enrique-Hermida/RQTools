namespace RQTools.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using RQTools.Models;
    using RQTools.Services;
    using System;
    using System.Windows.Input;
    using Xamarin.Forms;

    class AddProductViewModel : BaseViewModel
    {
        #region Services
        private DataServices dataService;
        #endregion
        #region Attributes
        private int cantidad;
        private string lote;
        private string nombreProducto;
        private bool isEnabled;
        private InventarioModel itemInventario;
        private Products product;
        #endregion
        #region Properties
        public int Cantidad
        {
            get { return this.cantidad; }
            set { SetValue(ref this.cantidad, value); }
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
            get { return this.product; }
            set { SetValue(ref this.product, value); }
        }
        #endregion
        #region Constructors
        public AddProductViewModel(Products products)
        {
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
            if (this.Cantidad > 4000)
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "Debes ingresar cantidades menores a 4000",
                    "Aceptar");
                return;
            }

            this.ItemInventario = new InventarioModel();
            this.ItemInventario.Id_Producto = this.Product.ID_Producto;
            this.ItemInventario.Producto = this.Product.Nombre_Producto;
            this.ItemInventario.Cantidad = this.Cantidad;
            this.ItemInventario.Lote = this.Lote;
            


        }
        #endregion
    }
}
