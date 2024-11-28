using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MaterialDetailUsingMVC.Models
{
    public class ReferenceDetail
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public string ReferenceNumber { get; set; }
        [Required]
        public DateTime ReferenceDate { get; set; }

        public IList<Material> Materials { get; set; }
    }
}
