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
    
    public partial class eng_project_permission
    {
        public int Project_Permission_ID { get; set; }
        public Nullable<int> ProjectID { get; set; }
        public Nullable<int> EmployeeID { get; set; }
        public Nullable<int> Access { get; set; }
    
        public virtual eng_employee_profile eng_employee_profile { get; set; }
        public virtual eng_project eng_project { get; set; }
    }
}
