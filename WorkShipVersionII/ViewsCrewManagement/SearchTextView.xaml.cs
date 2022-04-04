using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WorkShipVersionII.ViewsCrewManagement
{
    /// <summary>
    /// Interaction logic for SearchTextView.xaml
    /// </summary>
    public partial class SearchTextView : UserControl
    {
        public SearchTextView()
        {
            InitializeComponent();
        }

        private void SearchDataGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            Style _style = new Style(typeof(TextBox));

            _style.Setters.Add(new Setter(TextBox.MaxLengthProperty, 2));
            (e.Column as DataGridTextColumn).EditingElementStyle = _style; ;
        }
    }
}
