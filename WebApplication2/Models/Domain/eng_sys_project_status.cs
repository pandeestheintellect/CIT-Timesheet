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
    
    public partial class eng_sys_project_status
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public eng_sys_project_status()
        {
            this.eng_project_master = new HashSet<eng_project_master>();
        }
    
        public int ProjectStatusID { get; set; }
        public string ProjectStatus { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<eng_project_master> eng_project_master { get; set; }
    }
}
