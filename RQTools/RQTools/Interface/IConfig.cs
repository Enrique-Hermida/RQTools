namespace RQTools.Interface
{
   using SQLite.Net.Interop;
   public interface IConfig
    {
        string DirectorioDB { get; }
        ISQLitePlatform Plataforma { get; }
    }
}
