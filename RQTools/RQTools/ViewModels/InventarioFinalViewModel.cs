namespace RQTools.ViewModels
{
    using RQTools.Models;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    public class InventarioFinalViewModel :BaseViewModel
    {
        #region Atributtes
        private bool isRunning;
        private int CajaDializadores = 24;
        private MainViewModel mainViewModel = MainViewModel.GetInstance();
        private ProductsPiece prodcuts;
        #endregion
        #region Popierties
        public bool IsRunning
        {
            get { return this.isRunning; }
            set { SetValue(ref this.isRunning, value); }
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
        #endregion
        #region Constructor
        public InventarioFinalViewModel()
        {
            this.Prodcuts = new ProductsPiece();
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
            this.Prodcuts.Dializadores2_10 = 0;
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
            this.Prodcuts.Dializadores1_90 = 0;
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
            this.Prodcuts.Dializadores1_70 = 0;
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
            this.ListProductsPiece.Add(Prodcuts);
        }
        #endregion
    }
}
