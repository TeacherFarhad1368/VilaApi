using System.ComponentModel.DataAnnotations;

namespace Vila.Web.Models.Customer
{
    public class RegisterModel
    {
        [Display(Name = "موبایل")]
        [Required(ErrorMessage = "موبایل اجباری است")]
        [MaxLength(11 , ErrorMessage = " موبایل باید 11 رقم باشد .")]
        [MinLength(11 , ErrorMessage = " موبایل باید 11 رقم باشد .")]
        public string Mobile { get; set; }
        [Display(Name = "رمز عبور")]
        [Required(ErrorMessage = "رمز عبور اجباری است .")]
        public string Pass { get; set; }
    }
}
