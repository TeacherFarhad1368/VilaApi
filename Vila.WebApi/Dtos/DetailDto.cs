using System.ComponentModel.DataAnnotations;

namespace Vila.WebApi.Dtos
{
    public class DetailDto
    {
        public int DetailId { get; set; }
        [Required]
        public int VilaId { get; set; }
        [Required(ErrorMessage = "ویژگی ویلا اجباری است")]
        [MaxLength(255, ErrorMessage = "ویژگی ویلا نباید بیش از 255 حرف باشد")]
        public string What { get; set; }
        [Required(ErrorMessage = "توضیحات ویژگی ویلا اجباری است")]
        [MaxLength(500, ErrorMessage = "توضیحات ویژگی ویلا نباید بیش از 500 حرف باشد")]
        public string Value { get; set; }
    }
}
