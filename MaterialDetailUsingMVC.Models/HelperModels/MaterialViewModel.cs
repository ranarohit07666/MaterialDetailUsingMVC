using System.ComponentModel.DataAnnotations;

namespace MaterialDetailUsingMVC.Models.HelperModels
{
    public class MaterialViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Material Name is required.")]
        public string MaterialName { get; set; }

        [Required(ErrorMessage = "Rate is required.")]
        [Range(10, double.MaxValue, ErrorMessage = "Rate must be greater than 10.")]
        public decimal Rate { get; set; }

        [Required(ErrorMessage = "Consumption is required.")]
        [Range(10, double.MaxValue, ErrorMessage = "Consumption must be greater than 10.")]
        public decimal Consumption { get; set; }

        [Required(ErrorMessage = "Type is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Type is required.")]
        public int TypeId { get; set; }

        [Required(ErrorMessage = "Unit is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Unit is required.")]
        public int UnitId { get; set; }
        public int ReferenceId { get; set; }


    }
}
