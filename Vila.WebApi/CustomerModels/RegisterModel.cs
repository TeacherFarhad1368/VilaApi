using System.ComponentModel.DataAnnotations;

namespace Vila.WebApi.CustomerModels
{
    public class RegisterModel
    {
        [Required(ErrorMessage ="موبایل اجباری است")]
        [MaxLength(11, ErrorMessage = "موبایل باید 11 رقم باشد .")]
        [MinLength(11, ErrorMessage = "موبایل باید 11 رقم باشد .")]
        public string Mobile { get; set; }
        [Required(ErrorMessage = "کلمه عبور اجباری است")]
        [MaxLength(90)]
        public string Pass { get; set; }
    }
}
