using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Eng360Web.Models.ViewModel
{
    public class LeaveMasViewModel
    {
        public int LeaveMasId { get; set; }

        [Display(Name = "Leave Code")]
        public string LeaveMasCode { get; set; }

        [Display(Name = "Leave Type")]
        public int LeaveMasType { get; set; }

        [Display(Name = "From Date")]
        public string FromDate { get; set; }

        [Display(Name = "To Date")]
        public string ToDate { get; set; }

        [Display(Name = "Number of days")]
        public int NoOfDays { get; set; }


        [Display(Name = "Remaining Days")]
        public int RemainingLeaves { get; set; }

        [Display(Name = "Leave Reason"), Required, StringLength(250)]
        public string LeaveReason { get; set; }

        
        public int LeaveId { get; set; }

        [Display(Name = "Created Date")]
        public DateTime CreatedDate { get; set; }
        [Display(Name = "Updated Date")]
        public DateTime UpdatedDate { get; set; }
        [Display(Name = "Created By")]
        public Nullable<int> CreatedBy { get; set; }
        [Display(Name = "Updated sBy")]
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<int> IsActive { get; set; }
        public LeaveSettingViewModel eng_leave_settings { get; set; }

        // public   eng_supplier_master eng_supplier_master { get; set; }


    }
}