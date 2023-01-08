using System.ComponentModel.DataAnnotations;

namespace Vila.WebApi.Models
{
    public class Vila
    {
        [Key]
        public int VilaId { get; set; }
        [Required]
        [MaxLength(255)]
        public string Name { get; set; }
        [Required]
        [MaxLength(255)]
        public string State { get; set; }
        [Required]
        [MaxLength(255)]
        public string City { get; set; }
        [Required]
        [MaxLength(500)]
        public string address { get; set; }
        [Required]
        [MaxLength(11)]
        public string Mobile { get; set; }
        [Required]
        public long DayPrice { get; set; }
        [Required]
        public long SellPrice { get; set; }
        [Required]
        public DateTime BuildDate { get; set; }
        public byte[]? Image { get; set; }
        public List<Detail> Details { get; set; }
    }
}
