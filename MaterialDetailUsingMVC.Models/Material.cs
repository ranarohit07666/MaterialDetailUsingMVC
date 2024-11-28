using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace MaterialDetailUsingMVC.Models
{
    public class Material
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string MaterialName { get; set; }
        [Required]
        [Precision(18, 2)]
        public decimal Rate { get; set; }
        [Required]
        [Precision(18, 2)]
        public decimal Consumption { get; set; }
        [Required]
        public Types Types { get; set; }
        [Required]
        public Unit Unit { get; set; }
        public ReferenceDetail ReferenceDetail { get; set; }
    }
}
