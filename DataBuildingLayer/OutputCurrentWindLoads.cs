﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBuildingLayer
{
    public class OutputCurrentWindLoads
    {
        [Key]
        public int Id { get; set; }
        public int PortId { get; set; }
        public string PortName { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal MainValues { get; set; }
        public string Units { get; set; }
        public DateTime OutputDate { get; set; }
    }
}
