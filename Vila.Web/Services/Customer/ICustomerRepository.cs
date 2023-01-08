using Vila.Web.Models;
using Vila.Web.Models.Customer;

namespace Vila.Web.Services.Customer
{
    public interface ICustomerRepository
    {
        Task<OperationResult> Register(RegisterModel model);
        Task<LoginResultModel> Login(RegisterModel model);
    }
}
