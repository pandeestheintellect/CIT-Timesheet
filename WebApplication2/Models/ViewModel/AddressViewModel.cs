using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Eng360Web.Models.ViewModel
{
    public class AddressViewModel
    {
        public int AddressID { get; set; }
        [Display(Name = "Email"), Required]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail is not valid")]
        public string Email { get; set; }
        [Required]
        [MaxLength(13)]
        [MinLength(7)]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Mobile Number must be numeric")]
        public string Mobile { get; set; }
        public string Tel { get; set; }
        public string Web { get; set; }
        [StringLength(50)]
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        [ StringLength(50)]
        public string City { get; set; }
        [StringLength(10)]
        public string Country { get; set; }
        [RegularExpression(@"^\d{6}(-\d{4})?$", ErrorMessage = "Postal Code is not valid")]
        [Display(Name ="Postal Code")]
        public string Postal_Code { get; set; }
        public string Fax1 { get; set; }
        public string SkypeID { get; set; }
        public string Remarks { get; set; }
        public countryViewModel countryMaster { get; set; }
    }
}