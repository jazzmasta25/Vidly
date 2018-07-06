using System.ComponentModel.DataAnnotations;

namespace Vidly.ViewModels
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Drivers License")]
        public string DriversLicense { get; set; }

        [Required]
        [MaxLength(50)]
        public string Phone { get; set; }
    }
}