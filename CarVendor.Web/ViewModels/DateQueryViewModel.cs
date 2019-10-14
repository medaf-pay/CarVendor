using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CarVendor.Web.ViewModels
{
    public class DateQueryViewModel
    {


        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "From")]
        public DateTime StartDate { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "To")]
        public DateTime EndDate { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (EndDate < StartDate)
            {
                yield return
                  new ValidationResult(errorMessage: "EndDate must be greater than StartDate",
                                       memberNames: new[] { "EndDate" });
            }
            if ((EndDate == null || StartDate == null) && EndDate != StartDate)
            {
                yield return
                                new ValidationResult(errorMessage: "Make sure to enter both Start and End Dates",
                                                     memberNames: new[] { "EndDate","StartDate" });
            }
        }

        
        

     
    }
}