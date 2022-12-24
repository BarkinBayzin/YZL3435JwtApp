namespace JWTApp.BackOffice.Infrastructure.Tools
{
    public class JWTRepsonse
    {
        public JWTRepsonse(string token, DateTime expireDate)
        {
            Token = token;
            ExpireDate = expireDate;
        }
        public string Token { get; set; }
        public DateTime ExpireDate { get; set; }
    }
}
