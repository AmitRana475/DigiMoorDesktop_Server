using DataBuildingLayer;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Controls.Primitives;
using System.Linq;
using System.Data.Entity;
using System;
using WorkShipVersionII.ViewModelCrewManagement;

namespace WorkShipVersionII.ViewsCrewManagement
{
    /// <summary>
    /// Interaction logic for CrewRankView.xaml
    /// </summary>

    public delegate Point GetDragDropPosition(IInputElement theElement);

    public partial class CrewRankView : UserControl
    {
        int prevRowIndex = -1;
        private readonly ShipmentContaxt sc;
        public CrewRankView()
        {
            InitializeComponent();
            if (sc == null)
            {
                sc = new ShipmentContaxt();
                sc.Configuration.ProxyCreationEnabled = false;
            }

            //this.DataContext = new CrewRankViewModel();

            //The Event on DataGrid for selecting the Row
            this.RankGrid.PreviewMouseLeftButtonDown +=
                new MouseButtonEventHandler(RankGrid_PreviewMouseLeftButtonDown);
            //The Drop Event
            this.RankGrid.Drop += new DragEventHandler(RankGrid_Drop);

        }

        void RankGrid_Drop(object sender, DragEventArgs e)
        {
            try
            {
                if (prevRowIndex < 0)
                    return;

                int index = this.GetDataGridItemCurrentRowIndex(e.GetPosition);

                //The current Rowindex is -1 (No selected)
                if (index < 0)
                    return;
                //If Drag-Drop Location are same
                if (index == prevRowIndex)
                    return;
                //If the Drop Index is the last Row of DataGrid(
                // Note: This Row is typically used for performing Insert operation)
                if (index == RankGrid.Items.Count)
                {
                    MessageBox.Show("This row-index cannot be used for Drop Operations");
                    return;
                }

                var myEmps = RankGrid.Items.OfType<CrewRankClass>().ToList();

                CrewRankClass movedEmps = myEmps[prevRowIndex];
                myEmps.RemoveAt(prevRowIndex);

                myEmps.Insert(index, movedEmps);

                RankGrid.ItemsSource = myEmps.ToList();
            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);
            }
        }

        void RankGrid_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                prevRowIndex = GetDataGridItemCurrentRowIndex(e.GetPosition);

                if (prevRowIndex < 0)
                    return;
                RankGrid.SelectedIndex = prevRowIndex;

                CrewRankClass selectedEmp = RankGrid.Items[prevRowIndex] as CrewRankClass;

                if (selectedEmp == null)
                    return;

                //Now Create a Drag Rectangle with Mouse Drag-Effect
                //Here you can select the Effect as per your choice

                DragDropEffects dragdropeffects = DragDropEffects.Move;

                if (DragDrop.DoDragDrop(RankGrid, selectedEmp, dragdropeffects)
                                    != DragDropEffects.None)
                {
                    //Now This Item will be dropped at new location and so the new Selected Item
                    RankGrid.SelectedItem = selectedEmp;
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
            if (RankGrid.ItemContainerGenerator.Status
                    != GeneratorStatus.ContainersGenerated)
                return null;

            return RankGrid.ItemContainerGenerator.ContainerFromIndex(index)
                                                            as DataGridRow;
        }

        private int GetDataGridItemCurrentRowIndex(GetDragDropPosition pos)
        {
            int curIndex = -1;
            for (int i = 0; i < RankGrid.Items.Count; i++)
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
                var myEmps = RankGrid.Items.OfType<CrewRankClass>().ToList();

                if (MessageBox.Show("Do you want to save this ordering?", "", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {


                    int count = 1;
                    foreach (var item in myEmps)
                    {
                        CrewRankClass findrank = myEmps.Where(x => x.cid == item.cid).FirstOrDefault();

                        findrank.SrNo = count;
                        sc.Entry(findrank).State = EntityState.Modified;
                        //sc.SaveChanges();

                        count++;
                    }

                    sc.SaveChanges();


                    var data = sc.CrewRanks.ToList();
                    RankGrid.ItemsSource = data.OrderBy(x => x.SrNo);
                    new AddCrewDetailModel();
                    
                    MessageBox.Show("Record saved successfully", "Crew Rank", MessageBoxButton.OK, MessageBoxImage.Information);

                }
                else
                {
                    RankGrid.ItemsSource = myEmps.OrderBy(x => x.SrNo).ToList();
                }
            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);
            }

        }
    }
}
