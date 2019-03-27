namespace RQTools.ViewModels
{
    using RQTools.Models;
    using System;
    using System.Collections.Generic;
    using System.Text;
    public class EditOrEliminateViewModel : BaseViewModel
    {
        #region Atributtes
        private int cantidad;
        private int idlist;
        private string lote;
        private string nombreProducto;
        private bool isEnabled;
        private MainViewModel mainViewModel = MainViewModel.GetInstance();
        #endregion
        #region Properties
        public InventarioModel ItemActual
        {
            get;
            set;
        }
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
        #endregion
        #region Contructors
        public EditOrEliminateViewModel(InventarioModel itemList)
        {
            this.ItemActual = new InventarioModel();
            this.ItemActual = itemList;
            this.NombreProducto = ItemActual.Producto;
            this.Lote = ItemActual.Lote;
            this.Cantidad = ItemActual.Cantidad;

        }
        #endregion
    }
}
