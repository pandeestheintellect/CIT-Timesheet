using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Eng360Web.Models.ViewModel
{
    public class TimeEntryGridViewModel
    {


        public int TEGridID { get; set; }

        public int TEMasterID { get; set; }

        public int ProjectID { get; set; }

        public string ProjectName { get; set; }

        public DateTime Date { get; set; }

        public DateTime Hours { get; set; }

        public Nullable<int> Day1 { get; set; }
        public Nullable<int> Day2 { get; set; }
        public Nullable<int> Day3 { get; set; }
        public Nullable<int> Day4 { get; set; }
        public Nullable<int> Day5 { get; set; }
        public Nullable<int> Day6 { get; set; }
        public Nullable<int> Day7 { get; set; }
        public Nullable<int> Day8 { get; set; }
        public Nullable<int> Day9 { get; set; }
        public Nullable<int> Day10 { get; set; }

        public Nullable<int> Day11 { get; set; }
        public Nullable<int> Day12 { get; set; }
        public Nullable<int> Day13 { get; set; }
        public Nullable<int> Day14 { get; set; }
        public Nullable<int> Day15 { get; set; }
        public Nullable<int> Day16 { get; set; }
        public Nullable<int> Day17 { get; set; }
        public Nullable<int> Day18 { get; set; }
        public Nullable<int> Day19 { get; set; }
        public Nullable<int> Day20 { get; set; }

        public Nullable<int> Day21 { get; set; }
        public Nullable<int> Day22 { get; set; }
        public Nullable<int> Day23 { get; set; }
        public Nullable<int> Day24 { get; set; }
        public Nullable<int> Day25 { get; set; }
        public Nullable<int> Day26 { get; set; }
        public Nullable<int> Day27 { get; set; }
        public Nullable<int> Day28 { get; set; }
        public Nullable<int> Day29 { get; set; }
        public Nullable<int> Day30 { get; set; }
        public Nullable<int> Day31 { get; set; }




        //public ProjectViewModel eng_project_master { get; set; }
        public TimeEntryMasterViewModel eng_time_entry_master { get; set; }

        public ProjectNewViewModel eng_Project { get; set; }



    }
}