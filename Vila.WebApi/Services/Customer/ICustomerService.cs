using Vila.WebApi.CustomerModels;
using Vila.WebApi.Dtos;

namespace Vila.WebApi.Services.Customer
{
    public interface ICustomerService
    {
        public bool Register(RegisterModel model);
        public LoginResultDto Login(string mobile, string pass);
        public bool ExistMobile(string mobile);
        public bool PasswordIsCorrect(string mobile, string pass);

    }
}
