namespace RQTools.Models
{
    using SQLite.Net.Attributes;
    public class DeviceUser
    {
        [PrimaryKey, AutoIncrement]
        public int ID_User { get; set; }
        public string Name_User { get; set; }
        public string Correo { get; set; }
        public string Password { get; set; }
        public int User_Type { get; set; }

        public override string ToString()
        {
            return string.Format("{0} {1} {2} {3}", ID_User, Name_User, Correo, Password);
        }
    }

}
