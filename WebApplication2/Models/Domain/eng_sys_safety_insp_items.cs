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
    
    public partial class eng_sys_safety_insp_items
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public eng_sys_safety_insp_items()
        {
            this.eng_safety_insp_detail = new HashSet<eng_safety_insp_detail>();
        }
    
        public int SIItemID { get; set; }
        public string SIHeaderID { get; set; }
        public string SITitle { get; set; }
        public string SIItemDescription { get; set; }
        public Nullable<int> OrderBy { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<eng_safety_insp_detail> eng_safety_insp_detail { get; set; }
    }
}
