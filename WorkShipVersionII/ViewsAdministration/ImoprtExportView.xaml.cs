using System.Globalization;
using System.Windows.Controls;
using System.Windows.Markup;

namespace WorkShipVersionII.ViewsAdministration
{
    /// <summary>
    /// Interaction logic for ImoprtExportView.xaml
    /// </summary>
    public partial class ImoprtExportView : UserControl
    {
        public ImoprtExportView()
        {
           XmlLanguage.GetLanguage(CultureInfo.CurrentCulture.IetfLanguageTag);
            InitializeComponent();
        }
    }
}
