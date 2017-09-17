namespace InterestRateCalculation.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class BaseRate
    {

        public int Id { get; set; }

        [Required]
        [StringLength(9)]
        public string BaseRateCode { get; set; }

        public virtual ICollection<Agreement> Agreements { get; set; }

    }
}