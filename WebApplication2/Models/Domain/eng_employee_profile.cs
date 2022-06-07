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
    
    public partial class eng_employee_profile
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public eng_employee_profile()
        {
            this.eng_claim = new HashSet<eng_claim>();
            this.eng_time_entry_master = new HashSet<eng_time_entry_master>();
            this.eng_employee_profile12 = new HashSet<eng_employee_profile>();
            this.eng_holiday = new HashSet<eng_holiday>();
            this.eng_holiday1 = new HashSet<eng_holiday>();
            this.eng_inward = new HashSet<eng_inward>();
            this.eng_leave_settings = new HashSet<eng_leave_settings>();
            this.eng_leave_settings1 = new HashSet<eng_leave_settings>();
            this.eng_mm_stockadj_master = new HashSet<eng_mm_stockadj_master>();
            this.eng_outward = new HashSet<eng_outward>();
            this.eng_project = new HashSet<eng_project>();
            this.eng_project1 = new HashSet<eng_project>();
            this.eng_project2 = new HashSet<eng_project>();
            this.eng_project_permission = new HashSet<eng_project_permission>();
            this.eng_pymt_payable = new HashSet<eng_pymt_payable>();
            this.eng_qa_defect = new HashSet<eng_qa_defect>();
            this.eng_qa_defect1 = new HashSet<eng_qa_defect>();
            this.eng_safety_worker_list = new HashSet<eng_safety_worker_list>();
            this.eng_time_entry = new HashSet<eng_time_entry>();
            this.eng_usergroup1 = new HashSet<eng_usergroup>();
            this.eng_usergroup2 = new HashSet<eng_usergroup>();
            this.eng_users = new HashSet<eng_users>();
            this.eng_users1 = new HashSet<eng_users>();
            this.eng_users2 = new HashSet<eng_users>();
        }
    
        public int UserID { get; set; }
        public string EmpID { get; set; }
        public string OpBranch { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Nullable<int> AddressID { get; set; }
        public string Nationality { get; set; }
        public Nullable<System.DateTime> DoB { get; set; }
        public string SOC_number { get; set; }
        public Nullable<System.DateTime> SOC_Issue_Date { get; set; }
        public Nullable<System.DateTime> SOC_Expiry_Date { get; set; }
        public Nullable<decimal> Salary { get; set; }
        public Nullable<decimal> Levy { get; set; }
        public Nullable<System.DateTime> DoJ { get; set; }
        public Nullable<System.DateTime> DoR { get; set; }
        public string Gender { get; set; }
        public string Designation { get; set; }
        public string ID_Type { get; set; }
        public string ID_Number { get; set; }
        public string Profile_Desc { get; set; }
        public string Profile_Photo_Path { get; set; }
        public Nullable<int> llevel { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public string Passport_Number { get; set; }
        public Nullable<System.DateTime> Passport_Valid_Till { get; set; }
        public string Permit_Number { get; set; }
        public Nullable<System.DateTime> Permit_Valid_From { get; set; }
        public Nullable<System.DateTime> Permit_Valid_To { get; set; }
        public string Licence_Number { get; set; }
        public Nullable<System.DateTime> Licence_Valid_Till { get; set; }
        public string Insurance_Number { get; set; }
        public Nullable<System.DateTime> Insurance_Valid_Till { get; set; }
        public Nullable<int> IsActive { get; set; }
        public string License_Scissor_Lift_Number { get; set; }
        public Nullable<System.DateTime> License_Scissor_Lift_ExpiryDate { get; set; }
        public string License_Boom_Lift_Number { get; set; }
        public Nullable<System.DateTime> License_Boom_Lift_ExpiryDate { get; set; }
        public string License_WorkatHeight_Number { get; set; }
        public Nullable<System.DateTime> License_WorkatHeight_ExpiryDate { get; set; }
        public string License_IslandPass_Number { get; set; }
        public Nullable<System.DateTime> License_IslandPass_ExpiryDate { get; set; }
        public Nullable<int> Skilled_Level { get; set; }
        public string Safety_Supervisor_Name { get; set; }
        public string License_Course { get; set; }
        public Nullable<System.DateTime> License_Course_Expiry_Date { get; set; }
        public Nullable<int> GroupID { get; set; }
        public Nullable<int> Report_Manager { get; set; }
        public string ReportManager { get; set; }
        public Nullable<int> Country { get; set; }
    
        public virtual eng_address_master eng_address_master { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<eng_claim> eng_claim { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<eng_time_entry_master> eng_time_entry_master { get; set; }
        public virtual eng_employee_profile eng_employee_profile1 { get; set; }
        public virtual eng_employee_profile eng_employee_profile2 { get; set; }
        public virtual eng_employee_profile eng_employee_profile11 { get; set; }
        public virtual eng_employee_profile eng_employee_profile3 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<eng_employee_profile> eng_employee_profile12 { get; set; }
        public virtual eng_employee_profile eng_employee_profile4 { get; set; }
        public virtual eng_employee_profile eng_employee_profile13 { get; set; }
        public virtual eng_employee_profile eng_employee_profile5 { get; set; }
        public virtual eng_usergroup eng_usergroup { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<eng_holiday> eng_holiday { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<eng_holiday> eng_holiday1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<eng_inward> eng_inward { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<eng_leave_settings> eng_leave_settings { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<eng_leave_settings> eng_leave_settings1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<eng_mm_stockadj_master> eng_mm_stockadj_master { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<eng_outward> eng_outward { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<eng_project> eng_project { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<eng_project> eng_project1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<eng_project> eng_project2 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<eng_project_permission> eng_project_permission { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<eng_pymt_payable> eng_pymt_payable { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<eng_qa_defect> eng_qa_defect { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<eng_qa_defect> eng_qa_defect1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<eng_safety_worker_list> eng_safety_worker_list { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<eng_time_entry> eng_time_entry { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<eng_usergroup> eng_usergroup1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<eng_usergroup> eng_usergroup2 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<eng_users> eng_users { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<eng_users> eng_users1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<eng_users> eng_users2 { get; set; }
    }
}