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
    
    public partial class eng_safety_hazard_list
    {
        public int HLID { get; set; }
        public Nullable<int> SafetyID { get; set; }
        public Nullable<int> HazardID { get; set; }
    
        public virtual eng_safety_master eng_safety_master { get; set; }
        public virtual eng_sys_safety_hazard eng_sys_safety_hazard { get; set; }
    }
}
