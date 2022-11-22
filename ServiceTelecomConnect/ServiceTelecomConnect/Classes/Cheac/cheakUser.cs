namespace ServiceTelecomConnect
{
    public class cheakUser
    {
        public string Login { get; set; }

        public string IsAdmin { get; }

        public cheakUser(string login, string isAdmin)
        {
            Login = login.Trim();
            IsAdmin = isAdmin;
        }         
    }
}
