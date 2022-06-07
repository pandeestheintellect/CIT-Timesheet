using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eng360Web.Models.ViewModel
{
    public class ProjectPermissionViewModel
    {

        public int Project_Permission_ID { get; set; }
        public Nullable<int> ProjectID { get; set; }
        public Nullable<int> EmployeeID { get; set; }
        public Nullable<int> Access { get; set; }

        public  EmployeeViewModel eng_employee { get; set; }
        public ProjectNewViewModel eng_project { get; set; }
    }
}