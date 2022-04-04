using DataBuildingLayer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WorkShipVersionII.WorkHoursViewModel;

namespace WorkShipVersionII.WorkHoursView
{
    /// <summary>
    /// Interaction logic for MooringWinchListView.xaml
    /// </summary>
    /// 
    public delegate Point GetDragDropPosition(IInputElement theElement);
    public partial class MooringWinchListView : UserControl
    {
        int prevRowIndex = -1;
        private readonly ShipmentContaxt sc;
        public MooringWinchListView()
        {
            InitializeComponent();

            if (sc == null)
            {
                sc = new ShipmentContaxt();
                sc.Configuration.ProxyCreationEnabled = false;
            }

            this.MooringWinchGrid.Drop += new DragEventHandler(OnCertiGrid_Drop);
        }

        void OnCertiGrid_Drop(object sender, DragEventArgs e)
        {
            try
            {
                if (prevRowIndex < 0)
                    return;

                int index = this.GetDataGridItemCurrentRowIndex(e.GetPosition);


                if (index < 0)
                    return;

                if (index == prevRowIndex)
                    return;

                if (index == MooringWinchGrid.Items.Count)
                {
                    MessageBox.Show("This row-index cannot be used for Drop Operations");
                    return;
                }

                var myEmps = MooringWinchGrid.Items.OfType<MooringWinchClass>().ToList();

                MooringWinchClass movedEmps = myEmps[prevRowIndex];
                myEmps.RemoveAt(prevRowIndex);

                myEmps.Insert(index, movedEmps);

                MooringWinchGrid.ItemsSource = myEmps.ToList();
            }
            catch (Exception ex)
            {
                //sc.ErrorLog(ex);
            }
        }

        void OnCertiGrid_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                prevRowIndex = GetDataGridItemCurrentRowIndex(e.GetPosition);

                if (prevRowIndex < 0)
                    return;
                MooringWinchGrid.SelectedIndex = prevRowIndex;

                MooringWinchClass selectedEmp = MooringWinchGrid.Items[prevRowIndex] as MooringWinchClass;

                if (selectedEmp == null)
                    return;


                DragDropEffects dragdropeffects = DragDropEffects.Move;

                if (DragDrop.DoDragDrop(MooringWinchGrid, selectedEmp, dragdropeffects)
                                    != DragDropEffects.None)
                {
                    //Now This Item will be dropped at new location and so the new Selected Item
                    MooringWinchGrid.SelectedItem = selectedEmp;
                }

            }
            catch (Exception ex)
            {
                //sc.ErrorLog(ex);
            }

        }

        private bool IsTheMouseOnTargetRow(Visual theTarget, GetDragDropPosition pos)
        {
            Rect posBounds = VisualTreeHelper.GetDescendantBounds(theTarget);
            Point theMousePos = pos((IInputElement)theTarget);
            return posBounds.Contains(theMousePos);
        }

        private DataGridRow GetDataGridRowItem(int index)
        {
            if (MooringWinchGrid.ItemContainerGenerator.Status
                    != GeneratorStatus.ContainersGenerated)
                return null;

            return MooringWinchGrid.ItemContainerGenerator.ContainerFromIndex(index)
                                                            as DataGridRow;
        }

        private int GetDataGridItemCurrentRowIndex(GetDragDropPosition pos)
        {
            int curIndex = -1;
            for (int i = 0; i < MooringWinchGrid.Items.Count; i++)
            {
                DataGridRow itm = GetDataGridRowItem(i);
                if (IsTheMouseOnTargetRow(itm, pos))
                {
                    curIndex = i;
                    break;
                }
            }
            return curIndex;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var myEmps = MooringWinchGrid.Items.OfType<MooringWinchClass>().ToList();

                if (MessageBox.Show("Do you want to save this ordering?", "", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {

                    int count = 1;
                    foreach (var item in myEmps)
                    {
                        MooringWinchClass findrank = myEmps.Where(x => x.Id == item.Id && x.IsActive == true).FirstOrDefault();

                        findrank.SortingOrder = count;
                        sc.Entry(findrank).State = EntityState.Modified;
                        //sc.SaveChanges();



                        count++;

                        sc.SaveChanges();

                        sc.Entry(findrank).State = EntityState.Detached;


                      
                    }

                   

                    var data = sc.MooringWinch.Where(x=> x.IsActive==true).ToList();
                    MooringWinchGrid.ItemsSource = data.OrderBy(x => x.SortingOrder);

                    MessageBox.Show("Record saved successfully", "Mooring Winches", MessageBoxButton.OK, MessageBoxImage.Information);

                }
                else
                {
                    MooringWinchGrid.ItemsSource = myEmps.OrderBy(x => x.SortingOrder).ToList();
                }
            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);
            }
        }
    }
}
