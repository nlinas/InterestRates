using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InterestRateCalculation.Models
{
    public class Agreement
    {
        public int Id { get; set; }

        //I think this should be decimal, but in example were given int numbers
        public int Amount { get; set; }
 
        public decimal Margin { get; set; }

        public int Duration { get; set; }

        [Required]
        public int CustomerId { get; set; }

        [Required]
        public int BaseRateId { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual BaseRate BaseRate { get; set; }

        [NotMapped]
        [Display(Name = "New Base Rate")]
        public int NewBaseRateId { get; set; }

        [NotMapped]
        [Display(Name = "Interest Rate")]
        public decimal InterestRate { get; set; }

        [NotMapped]
        [Display(Name = "New Interest Rate")]
        public decimal NewInterestRate { get; set; }

        [NotMapped]
        [Display(Name = "Interest rates differences")]
        public decimal InterestRatesDiff { get; set; }
    }
}