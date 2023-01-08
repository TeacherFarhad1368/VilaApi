namespace Vila.Web.Models.Customer
{
    public class CustomerModel
    {
        public int CustomerId { get; set; }
        public string Mobile { get; set; }
        public string JwtSecret { get; set; }
        public string Role { get; set; }
    }
}
