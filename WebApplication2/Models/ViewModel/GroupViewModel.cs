using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Eng360Web.Models.ViewModel
{
    public class GroupViewModel
    {
        public int GroupID { get; set; }
        
        [Display(Name = "Group Code")]
        public string GroupCode { get; set; }

        [Display(Name = "Group Name"),Required]
        public string GroupName { get; set; }

        public string Remarks { get; set; }
        public string UpdatedByName { get; set; }
        
        public string CreatedByName { get; set; }
        [Display(Name = "Created Date")]
        public Nullable<DateTime> CreatedDate { get; set; }
        [Display(Name = "Updated Date")]
        public Nullable<DateTime> UpdatedDate { get; set; }
        [Display(Name = "Created By")]
        public Nullable<int> CreatedBy { get; set; }
        [Display(Name = "Updated By")]
        public Nullable<int> UpdatedBy { get; set; }


       
        public Nullable<int> IsActive { get; set; }
        
        List<PermissionViewModel> eng_permission { get; set; }
        public UserViewModel eng_user { get; set; }
        
    }
}