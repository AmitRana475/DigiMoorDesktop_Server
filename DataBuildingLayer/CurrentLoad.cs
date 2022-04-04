﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataBuildingLayer
{
       [Table("tblCurrentLoad")]
       public class CurrentLoad
       {
              [Key]
              public int Id { get; set; }
              public string Name { get; set; }
              public string Notation { get; set; }
              public Nullable<decimal> MainValue { get; set; }
              public string Units { get; set; }
       }
}
