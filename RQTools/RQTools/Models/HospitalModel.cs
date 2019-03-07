namespace RQTools.Models
{
    using SQLite;
    public class HospitalModel
    {
        [PrimaryKey]
        public int ID_Hospital { get; set; }
        public string Nombre_Hospital { get; set; }
        public string Codigo_Hospital { get; set; }
        public int Activo { get; set; }
    }
}
