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
    
    public partial class eng_leave_master
    {
        public int LeaveMasId { get; set; }
        public string LeaveMasCode { get; set; }
        public Nullable<int> LeaveMasType { get; set; }
        public Nullable<System.DateTime> FromDate { get; set; }
        public Nullable<System.DateTime> ToDate { get; set; }
        public Nullable<int> NoOfDays { get; set; }
        public Nullable<int> RemainingLeaves { get; set; }
        public string LeaveReason { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public Nullable<int> IsActive { get; set; }
    
        public virtual eng_leave_settings eng_leave_settings { get; set; }
    }
}
