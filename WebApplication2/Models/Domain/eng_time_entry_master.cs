//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Eng360Web.Models.Domain
{
    using System;
    using System.Collections.Generic;
    
    public partial class eng_time_entry_master
    {
        public int TEMasterID { get; set; }
        public Nullable<int> EmpID { get; set; }
        public int ManagerID { get; set; }
        public Nullable<System.DateTime> Month { get; set; }
        public Nullable<int> status { get; set; }
        public string ApprovalRemarks { get; set; }
        public string RejectedRemarks { get; set; }
        public Nullable<System.DateTime> ApprovalDate { get; set; }
        public Nullable<System.DateTime> RejectedDate { get; set; }
        public Nullable<int> ApprovedBy { get; set; }
        public Nullable<int> RejectedBy { get; set; }
        public Nullable<int> TotalWorkingHour { get; set; }
        public Nullable<int> TotalLeaveHour { get; set; }
        public string TimeEntryCode { get; set; }
        public Nullable<int> SubmittedBy { get; set; }
        public Nullable<System.DateTime> SubmittedDate { get; set; }
        public Nullable<int> ProjectCount { get; set; }
    
        public virtual eng_employee_profile eng_employee_profile { get; set; }
    }
}