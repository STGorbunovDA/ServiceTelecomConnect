namespace ServiceTelecomConnect
{
    public class cheakUser
    {
        public string Login { get; set; }

        public string IsAdmin { get; }

        //public string Status => IsAdmin ? "Admin" : "User";

        public cheakUser(string login, string isAdmin)
        {
            Login = login.Trim();
            IsAdmin = isAdmin;
        }         
    }
}
