using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WorkShipVersionII
{
   public static class StaticHelper
    {
        public static bool Wathckeeping { get; set; } = true;
        public static bool Editing { get; set; }
        public static ItemCollection ItemMenu { get; set; }
        public static string LastMenuItem { get; set; }
              public static string TabButtonMenuName { get; set; }

       }
}
