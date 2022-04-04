using DataBuildingLayer;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WorkShipVersionII.ViewModelCrewManagement
{
    public class ShipSpecificDataViewModel : ViewModelBase
    {
        private readonly ShipmentContaxt sc;

        public ShipSpecificDataViewModel(int menuid)
        {
            if (sc == null)
            {
                sc = new ShipmentContaxt();
                sc.Configuration.ProxyCreationEnabled = false;
            }

            var dt = sc.ShipSpecificContents.Where(x => x.MId == menuid).FirstOrDefault();
            if (dt != null)
            {
                StringBuilder sb = new StringBuilder();

                //string ss="<head>< meta http - equiv = 'Content-Type content' = text / html; charset = UTF - 8 >
                sb.Append("<head><meta http-equiv='Content-Type' content='text/html;charset=UTF-8'>");
                sb.Append(dt.Content);
                sb.Append("</head>");

                var fragment = Regex.Replace(sb.ToString(), "<!--.*?-->", String.Empty, RegexOptions.Multiline);

                
                ShipSpecificData.Content = fragment; //"<p>Page title 7Page title 7Page title 7Page title 7Page title 7Page title 7Page title 7Page title 7<br></p>";
            }
            else
            {
                ShipSpecificData.Content = "<p>Data not available.<br></p>";
            }
        }

        public ShipSpecificDataViewModel()
        {
            if (sc == null)
            {
                sc = new ShipmentContaxt();
                sc.Configuration.ProxyCreationEnabled = false;
            }


            ShipSpecificData.Content = "<p>Data not available.<br></p>";

        }


        private ShipSpecificContent _shipData = new ShipSpecificContent();

        public ShipSpecificContent ShipSpecificData
        {
            get { return _shipData; }
            set
            {
                _shipData = value;
                RaisePropertyChanged("ShipSpecificData");
            }

        }

    }
}
