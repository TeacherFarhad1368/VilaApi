namespace Vila.WebApi.Utility
{
    public class JWTSettings
    {
        public string Secret { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
    }
}
