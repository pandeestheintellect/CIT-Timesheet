using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Eng360Web.Models.ViewModel
{
    public class HolidayMasterViewModel
    {
        public int HolidayId { get; set; }
        public string HolidayCode { get; set; }


        [Display(Name = "Date"),Required]
       
        public string Date { get; set; }

        [Display(Name = "Holiday Name"),Required]
        [StringLength(255)]
        public string Description { get; set; }
       

        [Display(Name = "Day")]
        public string Day { get; set; }
        [Display(Name = "Country")]
        public Nullable<int> Country { get; set; }
       
        [Display(Name = "Created Date")]
        public Nullable<DateTime> CreatedDate { get; set; }
        [Display(Name = "Updated Date")]
        public Nullable<DateTime> UpdatedDate { get; set; }
        [Display(Name = "Created By")]
        public Nullable<int> CreatedBy { get; set; }
        [Display(Name = "Updated By")]
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<int> IsActive { get; set; }


        public countryViewModel countryMaster { get; set; }
        public EmployeeViewModel eng_employee_profile { get; set; }
        public EmployeeViewModel eng_employee_profile1 { get; set; }

    }
}