using AutoMapper;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Vila.WebApi.Context;
using Vila.WebApi.CustomerModels;
using Vila.WebApi.Dtos;
using Vila.WebApi.Utility;

namespace Vila.WebApi.Services.Customer
{
    public class CustomerService : ICustomerService
    {
        private readonly DataContext _context;
        private readonly JWTSettings _setting;
        private readonly IMapper _mapper;
        public CustomerService(DataContext context , IMapper mapper, IOptions<JWTSettings> setting)
        {
            _context = context;
            _setting = setting.Value;
            _mapper = mapper;
        }
        public bool ExistMobile(string mobile) =>
            _context.Customers.Any(c => c.Mobile.Trim() == mobile.Trim());

        public LoginResultDto Login(string mobile, string pass)
        {
            var hashPass = PasswordHelper.EncodeProSecurity(pass.Trim());
            var customer = _context.Customers.SingleOrDefault(c => c.Mobile == mobile && c.Pass == hashPass);
            if (customer == null) return null;

            var key = Encoding.ASCII.GetBytes(_setting.Secret);

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescription = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name,customer.CustomerId.ToString()),
                    new Claim(ClaimTypes.Role,customer.Role.ToString())
                }),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature
                    ),
                Issuer = _setting.Issuer,
                Audience = _setting.Audience
            };
            var token = tokenHandler.CreateToken(tokenDescription);
            customer.JwtSecret = tokenHandler.WriteToken(token);
            return _mapper.Map<LoginResultDto>(customer);
        }

        public bool PasswordIsCorrect(string mobile, string pass)
        {
            var hashPass = PasswordHelper.EncodeProSecurity(pass.Trim());
            return _context.Customers.Any(c => c.Mobile.Trim() == mobile.Trim() &&
            c.Pass == hashPass
            );
        }

        public bool Register(RegisterModel model)
        {
            var hashPass = PasswordHelper.EncodeProSecurity(model.Pass.Trim());
            Models.Customer customer = new()
            {
                Mobile=model.Mobile,
                Pass=hashPass,
                Role="user"
            };
            
            try
            {
                _context.Customers.Add(customer);
                _context.SaveChanges();
                return true;
            }
            catch 
            {
                return false;
            }
           
        }
    }
}
