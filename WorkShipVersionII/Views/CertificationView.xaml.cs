
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

            this.OnCertiGrid.Drop += new DragEventHandler(OnCertiGrid_Drop);

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

                if (index == OnCertiGrid.Items.Count)
                {
                    MessageBox.Show("This row-index cannot be used for Drop Operations");
                    return;
                }

                var myEmps = OnCertiGrid.Items.OfType<CertificatesClass>().ToList();

                CertificatesClass movedEmps = myEmps[prevRowIndex];
                myEmps.RemoveAt(prevRowIndex);

                myEmps.Insert(index, movedEmps);

                OnCertiGrid.ItemsSource = myEmps.ToList();
            }
            catch(Exception ex)
            {
                sc.ErrorLog(ex);
            }
        }

        void OnCertiGrid_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                prevRowIndex = GetDataGridItemCurrentRowIndex(e.GetPosition);

                if (prevRowIndex < 0)
                    return;
                OnCertiGrid.SelectedIndex = prevRowIndex;

                CertificatesClass selectedEmp = OnCertiGrid.Items[prevRowIndex] as CertificatesClass;

                if (selectedEmp == null)
                    return;


                DragDropEffects dragdropeffects = DragDropEffects.Move;

                if (DragDrop.DoDragDrop(OnCertiGrid, selectedEmp, dragdropeffects)
                                    != DragDropEffects.None)
                {
                    //Now This Item will be dropped at new location and so the new Selected Item
                    OnCertiGrid.SelectedItem = selectedEmp;
                }

            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);
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
            if (OnCertiGrid.ItemContainerGenerator.Status
                    != GeneratorStatus.ContainersGenerated)
                return null;

            return OnCertiGrid.ItemContainerGenerator.ContainerFromIndex(index)
                                                            as DataGridRow;
        }

        private int GetDataGridItemCurrentRowIndex(GetDragDropPosition pos)
        {
            int curIndex = -1;
            for (int i = 0; i < OnCertiGrid.Items.Count; i++)
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
                var myEmps = OnCertiGrid.Items.OfType<CertificatesClass>().ToList();

                if (MessageBox.Show("Do you want to save this ordering?", "", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {

                    int count = 1;
                    foreach (var item in myEmps)
                    {
                        CertificatesClass findrank = myEmps.Where(x => x.Id == item.Id).FirstOrDefault();

                        findrank.SrNo = count;
                        sc.Entry(findrank).State = EntityState.Modified;
                        //sc.SaveChanges();

                        count++;
                    }

                    sc.SaveChanges();


                    var data = sc.Certificates.ToList();
                    OnCertiGrid.ItemsSource = data.OrderBy(x => x.SrNo);

                    MessageBox.Show("Record saved successfully", "Certification", MessageBoxButton.OK, MessageBoxImage.Information);

                }
                else
                {
                    OnCertiGrid.ItemsSource = myEmps.OrderBy(x => x.SrNo).ToList();
                }
            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);
            }
        }


    }

}
