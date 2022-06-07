using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Eng360Web.Models.ViewModel
{
    public class LeaveSettingViewModel
    {
        public int LeaveId { get; set; }

        [Display(Name = "Leave Code"), StringLength(50)]
        public string LeaveCode { get; set; }

        [Display(Name = "Leave Name"), Required]
        public string LeaveName { get; set; }

        [Display(Name = "Days"), Required]
        public int Days { get; set; }

        [Display(Name = "Remarks"), StringLength(100)]
        public string Remarks { get; set; }


        [Display(Name = "Leave Description")]
        public string LeaveDescription { get; set; }

        [Display(Name = "Created Date")]
        public DateTime CreatedDate { get; set; }
        [Display(Name = "Updated Date")]
        public DateTime UpdatedDate { get; set; }
        [Display(Name = "Created By")]
        public Nullable<int> CreatedBy { get; set; }
        [Display(Name = "Updated By")]
        public Nullable<int> UpdatedBy { get; set; }

       
        public Nullable<int> IsActive { get; set; }


        public EmployeeViewModel eng_employee_profile { get; set; }
        public EmployeeViewModel eng_employee_profile1 { get; set; }


    }
}