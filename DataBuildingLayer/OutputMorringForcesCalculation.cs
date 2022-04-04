using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBuildingLayer
{
    public class OutputMorringForcesCalculation
    {
        [Key]
        public int Id { get; set; }
        public int RopeId { get; set; }
        public int PortId { get; set; }
        public string PortName { get; set; }
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
        public decimal l0 { get; set; }
        public decimal Li { get; set; }
        public decimal ki { get; set; }
        public decimal kyi { get; set; }
        public decimal kyi_Xch { get; set; }
        public decimal kyi_Xch2 { get; set; }
        public decimal Fyi { get; set; }
        public decimal Ti { get; set; }
        public decimal FSi { get; set; }
        public DateTime OutputDate { get; set; }
    }
}
