namespace RQTools.Models
{
    using SQLite;
    class Products
    {
        [PrimaryKey]
        public string ID_Producro { get; set; }
        public string Nombre_Producro { get; set; }
        public string Clave { get; set; }
        public string Tipo_Producto { get; set; }
        public string Scanbar { get; set; }
        public string Fecha_Modificacion { get; set; }
    }
}
