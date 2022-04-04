using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBuildingLayer
{
    public class InputMooringCalculation
    {
        [Key]
        public int Id { get; set; }
        public int PortId { get; set; }
        public string PortName { get; set; }
        [NotMapped]
        public string AssignNumber { get; set; }
        [NotMapped]
        public string Location { get; set; }
        [NotMapped]
        public string Certi_No { get; set; }
        [NotMapped]
        public string UniqueId { get; set; }
        public Nullable<decimal> Xch { get; set; }
        public Nullable<decimal> Ych { get; set; }
        public decimal Zch { get; set; }
        public decimal Xbl { get; set; }
        public decimal Ybl { get; set; }
        public decimal Zbl { get; set; }
        public Nullable<decimal> l0 { get; set; }
        public decimal E { get; set; }
        public decimal n { get; set; }
        public decimal a { get; set; }
        public decimal MBSrope { get; set; }
        public int RopeId { get; set; }
        public DateTime InputDate { get; set; }
        public int OldInputId { get; set; }

        public object Xch1 { get; set; }
        public object Ych1 { get; set; }
        public object Zch1 { get; set; }
        public object Xbl1 { get; set; }
        public object Ybl1 { get; set; }
        public object Zbl1 { get; set; }
        public object l01 { get; set; }
        public object E1 { get; set; }
        public object n1 { get; set; }
        public object a1 { get; set; }
        public object MBSrope1 { get; set; }
        
    }
}
