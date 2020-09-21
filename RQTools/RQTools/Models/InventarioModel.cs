namespace RQTools.Models
{
    using SQLite;
    public class InventarioModel
    {
        [PrimaryKey]
        public int Id { get; set; }//indice de la lista
        public string Producto { get; set; }//nombre del prodicto
        public int Id_Producto { get; set; }//id producto
        public int Clave { get; set; }//grupo al que pertenece osea caja pieza etc.
        public int Cantidad { get; set; }//cantidad
        public string Lote { get; set; }// lote
    }
}
