using System.ComponentModel.DataAnnotations;

namespace Vila.Web.Models.Vila
{
    public class VilaModel
    {
        public int VilaId { get; set; }
        [Display(Name = "نام ویلا")]
        [Required(ErrorMessage = "نام ویلا اجباری است")]
        [MaxLength(255, ErrorMessage = "نام ویلا نباید بیش از 255 حرف باشد")]
        public string Name { get; set; }
        [Display(Name = "استان ویلا")]
        [Required(ErrorMessage = "استان ویلا اجباری است")]
        [MaxLength(255, ErrorMessage = "استان ویلا نباید بیش از 255 حرف باشد")]
        public string State { get; set; }
        [Display(Name = "شهرستان ویلا")]
        [Required(ErrorMessage = "شهرستان ویلا اجباری است")]
        [MaxLength(255, ErrorMessage = "شهرستان ویلا نباید بیش از 255 حرف باشد")]
        public string City { get; set; }
        [Display(Name = "ادرس ویلا")]
        [Required(ErrorMessage = "ادرس کامل ویلا اجباری است")]
        [MaxLength(500, ErrorMessage = "آدرس کامل ویلا نباید بیش از 500 حرف باشد")]
        public string address { get; set; }
        [Display(Name = "شماره تماس ویلا")]
        [Required(ErrorMessage = "شماره تماس کامل ویلا اجباری است")]
        [MaxLength(11, ErrorMessage = "شماره تماس ویلا نباید بیش از 11 حرف باشد")]
        [MinLength(11, ErrorMessage = "شماره تماس ویلا نباید کمتر از 11 حرف باشد")]
        public string Mobile { get; set; }
        [Display(Name = "قیمت کرایه روزانه")]
        [Required(ErrorMessage = "قیمت کرایه روزانه ویلا اجباری است")]
        public long DayPrice { get; set; }
        [Display(Name = "قیمت فروش  ویلا ویلا")]
        [Required(ErrorMessage = "قیمت فروش  ویلا اجباری است")]
        public long SellPrice { get; set; }
        [Display(Name = "تاریخ ساخت(فرمت 1389/05/04)")]
        [Required(ErrorMessage = "تاریخ ساخت  ویلا اجباری است")]
        public string BuildDate { get; set; }
        public byte[]? Image { get; set; }
    }
}
