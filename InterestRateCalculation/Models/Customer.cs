using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace InterestRateCalculation.Models
{
    public class Customer
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Personal Id")]
        [RegularExpression("([0-9]+)", ErrorMessage = "Please enter valid Number")]
        public long PersonalId { get; set; }

        [Required]
        [StringLength(200)]
        [Display(Name = "Full name")]
        public string Name { get; set; }

        public virtual ICollection<Agreement> Agreements { get; set; }
    }
}