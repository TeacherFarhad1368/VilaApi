namespace Vila.WebApi.Dtos
{
    public class LoginResultDto
    {
        public int CustomerId { get; set; }
        public string Mobile { get; set; }
        /// <summary>
        /// توکن احراز هویت bearer
        /// </summary>
        public string JwtSecret { get; set; }
        public string Role { get; set; }
    }
}
