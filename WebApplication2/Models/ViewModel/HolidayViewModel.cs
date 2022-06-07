using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Eng360Web.Models.ViewModel
{
    public class HolidayViewModel
    {
        public int HolidayId { get; set; }

        [Display(Name = "Holiday Code"), StringLength(50)]
        public string HolidayCode { get; set; }

        [Display(Name = "Date"),Required]
        public DateTime Date { get; set; }

        [Display(Name = "Day")]
        public string Day { get; set; }

        [Display(Name = "Country")]
        public int Country { get; set; }

        [Display(Name = "Holiday Description"), StringLength(100),Required]
        public string Description { get; set; }





        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<int> IsActive { get; set; }


        public   countryViewModel country { get; set; }


    }
}