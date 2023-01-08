namespace Vila.Web.Services.Customer
{
    public interface IAuthService
    {
        string GetJwtToken();
        void SignOut();
    }
}
