using System.ComponentModel.DataAnnotations;
using Vila.WebApi.ModelValidation;

namespace Vila.WebApi.Dtos
{
    public class VilaDto
    {
        public int VilaId { get; set; }
        [Required(ErrorMessage = "نام ویلا اجباری است")]
        [MaxLength(255 , ErrorMessage = "نام ویلا نباید بیش از 255 حرف باشد")]
        public string Name { get; set; }
        [Required(ErrorMessage = "استان ویلا اجباری است")]
        [MaxLength(255, ErrorMessage = "استان ویلا نباید بیش از 255 حرف باشد")]
        public string State { get; set; }
        [Required(ErrorMessage = "شهرستان ویلا اجباری است")]
        [MaxLength(255, ErrorMessage = "شهرستان ویلا نباید بیش از 255 حرف باشد")]
        public string City { get; set; }
        [Required(ErrorMessage = "ادرس کامل ویلا اجباری است")]
        [MaxLength(500, ErrorMessage = "آدرس کامل ویلا نباید بیش از 500 حرف باشد")]
        public string address { get; set; }
        [Required(ErrorMessage = "شماره تماس کامل ویلا اجباری است")]
        [MaxLength(11, ErrorMessage = "شماره تماس ویلا نباید بیش از 11 حرف باشد")]
        [MinLength(11, ErrorMessage = "شماره تماس ویلا نباید کمتر از 11 حرف باشد")]
        public string Mobile { get; set; }
        /// <summary>
        /// قیمت کرایه یک روز ویلا (تومان)
        /// </summary>
        [Required(ErrorMessage = "قیمت کرایه روزانه ویلا اجباری است")]
        public long DayPrice { get; set; }
        /// <summary>
        /// قیمت فروش ویلا (تومان)
        /// </summary>
        [Required(ErrorMessage = "قیمت فروش  ویلا اجباری است")]
        public long SellPrice { get; set; }
        /// <summary>
        /// تاریخ ساخت ویلا (روز و ماه باید 2 رقمی باشد 1379/05/01)
        /// </summary>
        [Required]
        [DateValidation]
        public string BuildDate { get; set; }
        public byte[] Image { get; set; }
    }
}
