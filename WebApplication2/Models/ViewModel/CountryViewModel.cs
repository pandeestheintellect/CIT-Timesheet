using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Eng360Web.Models.ViewModel
{
    public class countryViewModel
    {
        public int id { get; set; }

      
        public string iso { get; set; }

        
        public string name { get; set; }

        
        public string nicename { get; set; }

       
        public string iso3 { get; set; }

       
        public int numcode { get; set; }


        public int phonecode { get; set; }




        // public   eng_supplier_master eng_supplier_master { get; set; }


    }
}