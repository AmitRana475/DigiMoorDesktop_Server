using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace WorkShipVersionII.ViewsCrewManagement
{
    /// <summary>
    /// Interaction logic for AddHODView.xaml
    /// </summary>
    public partial class AddHODView : UserControl
    {
        public AddHODView()
        {
            InitializeComponent();
            lbSuggestion.Visibility = Visibility.Collapsed;
        }

        private void SubAllSelected_Click(object sender, RoutedEventArgs e)
        {
            var tag = ((CheckBox)sender).IsChecked;

            CheckBox[] boxes = new CheckBox[7];
            boxes[0] = this.chkDeviation;
            boxes[1] = this.chkCrewDetailAll;
            boxes[2] = this.chkGroupPlan;
            boxes[3] = this.chkResetPasswordAll;
            boxes[4] = this.chkFreeze;
            boxes[5] = this.chkWRHAll;
            boxes[6] = this.chkOverviewAll;

            foreach (CheckBox chkBoxes in boxes)
            {

                chkBoxes.IsChecked = tag;
            }



        }

        private void chkSelectAll_Click(object sender, RoutedEventArgs e)
        {

            CheckBox[] boxes = new CheckBox[23];
            boxes[0] = this.chkDeviation;
            boxes[1] = this.chkCertiNotif;
            boxes[2] = this.chkOffcNoti;
            boxes[3] = this.chkCrewDetailAll;
            boxes[4] = this.chkRank;
            boxes[5] = this.chkDepart;
            boxes[6] = this.chkHolidayGroup;
            boxes[7] = this.chkHOD;
            boxes[8] = this.chkResetPasswordAll;
            boxes[9] = this.chkFreeze;
            boxes[10] = this.chkWRHAll;
            boxes[11] = this.chkOverviewAll;
            //boxes[12] = this.chkworkSheduleRep;
            //boxes[13] = this.chkWorkhours;
            //boxes[14] = this.chkDeviationRep;
            //boxes[15] = this.chkOvertimeRep;
            boxes[12] = this.chkimpexp;
            boxes[13] = this.chkBackUp;
            boxes[14] = this.chkApplog;
            boxes[15] = this.chkRules;
            boxes[16] = this.chkLicense;
            boxes[17] = this.chkErrorlog;
            boxes[18] = this.chkView;
            boxes[19] = this.chkAdd;
            boxes[20] = this.chkEdit;
            boxes[21] = this.chkDelete;
            boxes[22] = this.chkGroupPlan;

            


            if (chkSelectAll.IsChecked == true)
            {
                foreach (CheckBox chkBoxes in boxes)
                {

                    chkBoxes.IsChecked = true;
                    //chkSelectAll.IsChecked = true;

                }

            }

            if (chkSelectAll.IsChecked == false)
            {
                foreach (CheckBox chkBoxes in boxes)
                {

                    chkBoxes.IsChecked = false;
                    //chkSelectAll.IsChecked = false;

                }

            }
        }

        private void txtCrewName_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Down)
            {
                lbSuggestion.Focus();
            }
        }

        private void lbSuggestion_KeyDown(object sender, KeyEventArgs e)
        {
            if (lbSuggestion.SelectedIndex > -1)
            {
                if (e.Key == Key.Enter)
                {
                    txtCrewName.Text = lbSuggestion.SelectedItem.ToString();
                    lbSuggestion.Visibility = Visibility.Collapsed;
                }


            }
        }

        private void lbSuggestion_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (lbSuggestion.SelectedIndex > -1)
            {

                txtCrewName.Text = lbSuggestion.SelectedItem.ToString();
                lbSuggestion.Visibility = Visibility.Collapsed;

            }
        }
    }
}
