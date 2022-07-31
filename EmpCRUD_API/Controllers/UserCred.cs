namespace EmpCRUD_API.Controllers
{
    public class Users
    {
        public string Password { get; set; }
        public string UserName { get; set; }
    }
    public class Tokens
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }
}