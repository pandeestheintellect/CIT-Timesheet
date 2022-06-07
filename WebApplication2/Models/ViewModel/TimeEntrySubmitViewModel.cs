using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Eng360Web.Models.ViewModel
{
    public class TimeEntrySubmitViewModel
    {

       
        public int TimeEntryId { get; set; }
        [Display(Name = "Time Entry Code")]
        public string TimeEntryCode { get; set; }
        [Display(Name = "Employee Name")]
        public Nullable<int> EmployeeName { get; set; }
        [Display(Name = "Project Name")]
        public Nullable<int> ProjectName { get; set; }
        [Display(Name = "Employee")]
        public Nullable<int> Employee { get; set; }
        public string WorkStartTime { get; set; }
        public string WorkEndTime { get; set; }        
        
        public Nullable<int> Leave { get; set; }
       
        public string ApprovalRemarks { get; set; }
        public string RejectedRemarks { get; set; }
        [Display(Name = "Approver")]
        public Nullable<int> ApprovedBy { get; set; }
        public string ApprovedDate { get; set; }
        public Nullable<int> RejectedBy { get; set; }
        public DateTime RejectedDate { get; set; }
        public Nullable<int> SubmittedBy { get; set; }
        [Display(Name = "Submit Date"), Required]
        public string SubmittedDate { get; set; }
        public int Status { get; set; }
        public int ProjectId { get; set; }
        public int UserId { get; set; }
        public int LeaveId { get; set; }


        public EmployeeViewModel eng_employee_profile { get; set; }
        public ProjectNewViewModel eng_project { get; set; }
        public LeaveSettingViewModel eng_leave_settings { get; set; }
        public UserViewModel eng_users { get;  set; }
        public UserViewModel eng_users1 { get;  set; }
        public UserViewModel eng_users2 { get; set; }




    }
}