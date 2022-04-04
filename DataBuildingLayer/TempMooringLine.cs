using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBuildingLayer
{
       public class TempMooringLine
       {

        public int Id { get; set; }

        public string AssignNumber { get; set; }

        public string Location { get; set; }

        public string Certi_No { get; set; }

        public string UniqueId { get; set; }

        public int? RopeId { get; set; }


        public decimal Xch { get; set; }
              public decimal Ych { get; set; }
              public decimal Zch { get; set; }
              public decimal Xbl { get; set; }
              public decimal Ybl { get; set; }
              public decimal Zbl { get; set; }
              public decimal l1 { get; set; }
              public decimal ΔZi { get; set; }
              public decimal a { get; set; }
              public decimal n { get; set; }
              public decimal ai { get; set; }
              public decimal MBSrope { get; set; }
              public decimal MBS { get; set; }
              public decimal Ei { get; set; }
              public decimal cosθi { get; set; }
              public decimal φi { get; set; }
              public decimal cosφi { get; set; }
              public Nullable<decimal> l0 { get; set; }
              public Nullable<decimal> Li { get; set; }
              public Nullable<decimal> ki { get; set; }
              public Nullable<decimal> kyi { get; set; }
              public Nullable<decimal> kyi_Xch { get; set; }
              public Nullable<decimal> kyi_Xch2 { get; set; }
              public Nullable<decimal> Fyi { get; set; }
              public Nullable<decimal> Ti { get; set; }
              public Nullable<decimal> FSi { get; set; }
             



       }
}
