namespace ServiceTelecomConnect
{
    public class CheakUser
    {
        public string Login { get; set; }

        public string IsAdmin { get; }

        public CheakUser(string login, string isAdmin)
        {
            Login = login.Trim();
            IsAdmin = isAdmin;
        }         
    }
}
