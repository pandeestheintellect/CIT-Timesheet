using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Eng360Web.Models.ViewModel
{
    public class TimeEntryMasterViewModel
    {

        public int TEMasterID { get; set; }

        public int EmpID { get; set; }
        [Display(Name = "Employee Name :")]

        public string EmployeeName { get; set; }
        [Display(Name = "Total working Hour")]

        public int TotalWorkingHour { get; set; }
        [Display(Name = "Total Leave Hour")]

        public int TotalLeaveHour { get; set; }
        [Display(Name = "Project Count")]

        public int ProjectCount { get; set; }
        [Display(Name = "Time Entry Code")]

        public string TimeEntryCode { get; set; }
        [Display(Name = "Designation :")]

        public string Designation { get; set; }
         [Display(Name = "Report Manager :")]

        public string ReportManager { get; set; }
        public string Month { get; set; }

        public int ManagerID { get; set; }

        public int Status { get; set; }

        public string ApprovalRemarks { get; set; }

        public string RejectedRemarks { get; set; }

        public string ApprovalDate { get; set; }

        public int ApprovedBy { get; set; }
        public int RejectedBy { get; set; }
        public string SubmittedDate { get; set; }

        public int SubmittedBy { get; set; }
        public string RejectedDate { get; set; }

        public int MonthYear { get; set; }

        public List<TimeEntryGridViewModel> tegridvm { get; set; }

        public EmployeeViewModel eng_employee_profile { get; set; }
        //public ProjectNewViewModel eng_Project { get; set; }

        //public UserViewModel eng_users { get; set; }



    }


}