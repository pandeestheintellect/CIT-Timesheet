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
    
    public partial class eng_claim_description
    {
        public int ClaimDescID { get; set; }
        public Nullable<int> ClaimID { get; set; }
        public Nullable<int> ClaimTypeID { get; set; }
        public Nullable<System.DateTime> RecpDate { get; set; }
        public string ClaimDescription { get; set; }
        public Nullable<decimal> RecpAmount { get; set; }
        public string GST { get; set; }
    
        public virtual eng_claim eng_claim { get; set; }
        public virtual eng_sys_claimtype eng_sys_claimtype { get; set; }
    }
}
