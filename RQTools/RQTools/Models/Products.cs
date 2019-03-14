namespace RQTools.Models
{
    using SQLite;
    public class Products
    {
        [PrimaryKey]
        public int ID_Producto { get; set; }
        public string Nombre_Producto { get; set; }
        public string Clave { get; set; }
        public int Tipo_Producto { get; set; }
        public string Scanbar { get; set; }
        public string Fecha_Modificacion { get; set; }
    }
}
