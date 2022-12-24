namespace JWTApp.BackOffice.Infrastructure.Tools
{
    public class JWTSettings
    {
        /*
         ValidAudience = "http://localhost",
         ValidIssuer = "http://localhost",
         ClockSkew = TimeSapn.Zero,
         ValidateLifeTime = true,
         IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("barkinbayzin.net6")) //16 karakterden fazla bir şey vermemi bekler!!
         ValidateIssuerSingingKey = true
         */

        public const string Issuer = "http://localhost";
        public const string Audience = "http://localhost";
        public const string Key = "barkinbayzin.net6";
        public const int Expire = 30;

    }
}
