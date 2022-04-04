
using DataBuildingLayer;
using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;

namespace WorkShipVersionII.Views
{
       /// <summary>
       /// Interaction logic for CertificationView.xaml
       /// </summary>
       /// 
       public delegate Point GetDragDropPosition(IInputElement theElement);

       public partial class CertificationView : UserControl
       {
              int prevRowIndex = -1;
              private readonly ShipmentContaxt sc;
              public CertificationView()
              {
                     InitializeComponent();

                     if (sc == null)
                     {
                            sc = new ShipmentContaxt();
                            sc.Configuration.ProxyCreationEnabled = false;
                     }

                     //this.OnCertiGrid.PreviewMouseLeftButtonDown +=
                     // new MouseButtonEventHandler(OnCertiGrid_PreviewMouseLeftButtonDown);
              }
       }

}
