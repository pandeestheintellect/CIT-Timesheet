using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Eng360Web.Models.ViewModel
{
    public class RoleViewModel
    {
        public int RoleId { get; set; }

        [Display(Name = "Role Code"), StringLength(50)]
        public string RoleCode { get; set; }

        [Display(Name = "Role Name")]
        public string RoleName { get; set; }



        [Display(Name = "Remarks"), StringLength(100)]
        public string Remarks { get; set; }





        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<int> IsActive { get; set; }

        public UserViewModel eng_user { get; set; }
        // public   eng_supplier_master eng_supplier_master { get; set; }


    }
}