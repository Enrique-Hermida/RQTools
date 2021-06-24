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
        private int clave;
        private string lote;
        private string nombreProducto;
        private bool isEnabled;
        private MainViewModel mainViewModel = MainViewModel.GetInstance();
        private InventarioModelViewModel itemInventario;
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
        public int Clave
        {
            get { return this.clave; }
            set { SetValue(ref this.clave, value); }
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
        public InventarioModelViewModel ItemInventario
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
            if (this.Product.Scanbar != "0")
            {
                this.CheckOtherData();
            }
            

        }

        private void CheckOtherData()
        {
            char[] codsc = this.Product.Scanbar.ToCharArray();
            string cproducto = "";
            string cfechaex = "";
            string clote = "";
            string Pef1 = codsc[0].ToString() + codsc[1].ToString();
            if (Pef1.Equals("01"))
            {


                for (int i = 2; i <= 15; i++)
                {
                    cproducto = cproducto + codsc[i].ToString();

                }
                for (int i = 18; i <= 23; i++)
                {
                    cfechaex = cfechaex + codsc[i].ToString();
                }
                string Pef2 = codsc[24].ToString() + codsc[25].ToString();
                if (Pef2.Equals("30"))
                {
                    // tiene cantidad
                    string Pef3 = codsc[28].ToString() + codsc[29].ToString();

                    if (Pef3.Equals("10"))
                    {
                        
                        for (int i = 30; i < codsc.Length; i++)
                        {
                            clote = clote + codsc[i].ToString();
                        }
                    }
                    else
                    {
                       ;
                        for (int i = 31; i < codsc.Length; i++)
                        {
                            clote = clote + codsc[i].ToString();
                        }
                    }
                }
                if (Pef2.Equals("10"))
                {
                    //pieza 
                   
                    for (int i = 26; i < codsc.Length; i++)
                    {
                        clote = clote + codsc[i].ToString();
                    }
                }
            }
            this.Lote = clote;
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
                this.Lote = "N/A";
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
                    "Error",
                    "Estas Ingresando Cantidades Mayores a 8000 ",
                    "Aceptar");
                return;
            }
            if (this.Cantidad > 4000)
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Advertencia",
                    "Estas Ingresando Cantidades Mayores a 4000 pza, estas seguro ",
                    "Aceptar");
                
            }
            if (this.Product.ID_Producto<24)
            {
                //caja de cualquier producto
                if (this.Product.ID_Producto <= 9 && this.Cantidad > 300)
                {
                    await Application.Current.MainPage.DisplayAlert(
                              "Error",
                              "Estas Ingresando Cantidades Mayores a 300 Cajas ",
                              "Aceptar");
                    return;
                }
                if (this.Product.ID_Producto == 14 && this.Cantidad > 222)
                {
                    await Application.Current.MainPage.DisplayAlert(
                              "Error",
                              "Estas Ingresando Cantidades Mayores a 222 Cajas ",
                              "Aceptar");
                    return;
                }
                if (Product.ID_Producto >= 18 && Product.ID_Producto<= 20 && this.Cantidad > 88)
                {
                    await Application.Current.MainPage.DisplayAlert(
                              "Error",
                              "Estas Ingresando Cantidades Mayores a 88 Cajas ",
                              "Aceptar");
                    return;
                }
            }

            #region BuscarID
            if (mainViewModel.InventarioActualMWM.Count==0)
            {
                IdList = 0;
            }
            else
            {
                var ultimoitem = mainViewModel.InventarioActualMWM.AsQueryable().LastOrDefault();
                var ultimoindex = ultimoitem.Id;
                IdList = ultimoindex + 1;
            }
            
            #endregion
            mainViewModel.InventarioActualMWM.Add(new InventarioModelViewModel
            {
                Id = this.IdList,
                Producto = this.Product.Nombre_Producto,
                Id_Producto = this.Product.ID_Producto,
                Clave = Int32.Parse(this.Product.Clave),
                Cantidad = this.Cantidad,
                Lote = this.Lote,
            });
            mainViewModel.Inventario = new InventarioViewModel();
            await App.Navigator.PushAsync(new InventarioTabbedPage());

        }

        #endregion
    }
}
