using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;namespace Eng360Web.Models.ViewModel
{
    public class ProjectNewViewModel
    {
        public int ProjectId { get; set; }
        public string ProjectCode { get; set; }
        [Display(Name = "Project Name")]
        [Required]
        public string ProjectName { get; set; }
        [Display(Name = "Client Name")]
        public int ClientId { get; set; }
        [Required]
        [Display(Name = "Start Date")]
        public string StartDate { get; set; }
        [Required]
        [Display(Name = "End Date")]
        public string EndDate { get; set; }
        [Display(Name = "Project Leader")]

        public Nullable<int> UserID { get; set; }
        [Display(Name = "Project Leader")]
        
        public Nullable<int> ProjectLeader { get; set; }


        [Display(Name = "Created Date")]
        public Nullable<DateTime> CreatedDate { get; set; }
        [Display(Name = "Updated Date")]
        public Nullable<DateTime> UpdatedDate { get; set; }
        [Display(Name = "Created By")]
        public Nullable<int> CreatedBy { get; set; }
        [Display(Name = "Updated By")]
        public Nullable<int> UpdatedBy { get; set; }
       
        public Nullable<int> IsActive { get; set; }
        public ClientViewModel eng_client_master { get; set; }
        public EmployeeViewModel eng_employee_profile { get; set; }
        public EmployeeViewModel eng_employee_profile1 { get; set; }
        public EmployeeViewModel eng_employee_profile2 { get; set; }
    }
}

