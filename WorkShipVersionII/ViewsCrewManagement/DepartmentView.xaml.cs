using System.Windows.Controls;
using WorkShipVersionII.ViewModelCrewManagement;

namespace WorkShipVersionII.ViewsCrewManagement
{
    /// <summary>
    /// Interaction logic for DepartmentView.xaml
    /// </summary>
    public partial class DepartmentView : UserControl
    {
        public DepartmentView()
        {
            InitializeComponent();
            this.DataContext = new DepartmentViewModel();
        }
    }
}
