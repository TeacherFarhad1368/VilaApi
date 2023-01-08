using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vila.WebApi.Models
{
    public class Detail
    {
        [Key]
        public int DetailId { get; set; }
        [Required]
        public int VilaId { get; set; }
        [Required]
        [MaxLength(255)]
        public string What { get; set; }
        [Required]
        [MaxLength(500)]
        public string Value { get; set; }
        [ForeignKey("VilaId")]
        public Models.Vila Vila { get; set; }
    }
}
