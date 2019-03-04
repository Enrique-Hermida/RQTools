namespace RQTools.Models
{
    using SQLite;

    public class DeviceUser
    {
        [PrimaryKey]
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
