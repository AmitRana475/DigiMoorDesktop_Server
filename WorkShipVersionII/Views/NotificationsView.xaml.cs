using DataBuildingLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace WorkShipVersionII.Views
{
       /// <summary>
       /// Interaction logic for NotificationsView.xaml
       /// </summary>
       public partial class NotificationsView : UserControl
       {
        
              private static ShipmentContaxt sc;
              public NotificationsView()
        {
           XmlLanguage.GetLanguage(CultureInfo.CurrentCulture.IetfLanguageTag);
            InitializeComponent();
            
                     //if (sc == null)
                     //{
                     //       sc = new ShipmentContaxt();
                     //       sc.Configuration.ProxyCreationEnabled = false;
                     //}

              }

             

              static int check = 0;


              public class Person
              {
                     public string Name { get; set; }
                     public int Age { get; set; }
              }
       }

    //public class SelectorClass : StyleSelector
    //{

    //    public override Style SelectStyle(object item, DependencyObject container)
    //    {
    //        var data = item as OrderInfo;

    //        if (data != null && ((container as GridCell).ColumnBase.GridColumn.MappingName == "TotalPrice"))
    //        {

    //            //custom condition is checked based on data.

    //            if (data.TotalPrice < 1005)
    //                return App.Current.Resources["redCellStyle"] as Style;
    //            return App.Current.Resources["blueCellStyle"] as Style;
    //        }
    //        return base.SelectStyle(item, container);
    //    }
    //}
}
