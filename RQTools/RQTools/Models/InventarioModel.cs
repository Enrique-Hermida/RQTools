﻿namespace RQTools.Models
{
    using SQLite;
    class InventarioModel
    {
        [PrimaryKey]
        public int Id { get; set; }
        public string Producto { get; set; }
        public int Id_Producto { get; set; }
        public int Cantidad { get; set; }
        public string Lote { get; set; }
    }
}