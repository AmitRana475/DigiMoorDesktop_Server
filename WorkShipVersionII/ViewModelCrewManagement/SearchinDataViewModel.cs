using DataBuildingLayer;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;
using WorkShipVersionII.ViewModel;
using WorkShipVersionII.ViewsCrewManagement;

namespace WorkShipVersionII.ViewModelCrewManagement
{
    public class SearchinDataViewModel :ViewModelBase
    {
        private readonly ShipmentContaxt sc;
        public SearchinDataViewModel(int contentid)
        {
            if (sc == null)
            {
                sc = new ShipmentContaxt();
                sc.Configuration.ProxyCreationEnabled = false;
            }


            cancelCommand = new RelayCommand(CancelSearchText);

            //GetSearchResult("cargo");


            var dt = sc.DocsPages.Where(x => x.Mid == contentid).FirstOrDefault();


            StringBuilder sb = new StringBuilder();

            //string ss="<head>< meta http - equiv = 'Content-Type content' = text / html; charset = UTF - 8 >
            sb.Append("<head><meta http-equiv='Content-Type' content='text/html;charset=UTF-8'>");
            sb.Append(dt.Content);
            sb.Append("</head>");

            var fragment = Regex.Replace(sb.ToString(), "<!--.*?-->", String.Empty, RegexOptions.Multiline);

            string abc = fragment;



            string searchString = StaticHelper.SearchText;
            string content = abc;
            string replacePattern = "$1<span style=\"background-color:yellow\">$2</span>$3";
            string searchPattern = String.Format("(>[^<>]*?)({0})([^<>]*?<)", searchString.Trim());
            content = Regex.Replace(content, searchPattern, replacePattern, RegexOptions.IgnoreCase);

            if (dt != null)
            {
                DocsContant.Content = content; //"<p>Page title 7Page title 7Page title 7Page title 7Page title 7Page title 7Page title 7Page title 7<br></p>";
            }
            else
            {
                DocsContant.Content = "<p>Data not available.<br></p>";
            }


            #region Commented Code



            // string sss = dt.Content.ToString();

            // var ss = sss;
            //var ss = Regex.Replace(sss, "<.*?>", String.Empty);


            //ss = Regex.Replace(ss, "(<style.+?</style>)|(<script.+?</script>)", "", RegexOptions.IgnoreCase | RegexOptions.Singleline);
            //ss = Regex.Replace(ss, "(<img.+?>)", "", RegexOptions.IgnoreCase | RegexOptions.Singleline);
            //ss = Regex.Replace(ss, "(<o:.+?</o:.+?>)", "", RegexOptions.IgnoreCase | RegexOptions.Singleline);
            //ss = Regex.Replace(ss, "<!--.+?-->", "", RegexOptions.IgnoreCase | RegexOptions.Singleline);
            //ss = Regex.Replace(ss, "class=.+?>", ">", RegexOptions.IgnoreCase | RegexOptions.Singleline);
            //ss = Regex.Replace(ss.Replace(System.Environment.NewLine, "<br/>"), "<[^(a|img|b|i|u|ul|ol|li)][^>]*>", " ");
            //ss = ss.Replace("&nbsp;", "");
            //ss = ss.Replace("v\\:* {behavior:url(#default#VML);}o\\:* {behavior:url(#default#VML);}w\\:* {behavior:url(#default#VML);}.shape {behavior:url(#default#VML);}", string.Empty);

            //ss = Regex.Replace(ss, @"\t|\n|\r", "");

            //var input = ss;


            //Regex regex = new Regex("\\<[^\\>]*\\>");
            //ss=String.Format("<b>Before:</b>{0}", ss); // HTML Text
            //ss = "<br/>";
            //ss = regex.Replace(ss, String.Empty);

            // ss = Regex.Replace(ss, @"(?s)(?<=<!--).+?(?=-->)", "");

            // string html = ss;
            //Console.WriteLine("Input html is " + html.Length + " chars");
            // html = CleanWordHtml(html);
            //  html = FixEntities(html);

            // ss = html;




            //Regex clearMarkup = new Regex(@"(<!--\[.*\]-->)", RegexOptions.Singleline);
            //ss = clearMarkup.Replace(ss, ""); // str is the content as shown above.       


            //ss = Regex.Replace(ss, "<html><head>.*</head></html>", "", RegexOptions.Singleline);

            //ss = Regex.Replace(ss, @"(?s)(?<=<!--).+?(?=-->)", "");

            //ss = Regex.Replace(ss, "(<style.+?</style>)|(<script.+?</script>)", "", RegexOptions.IgnoreCase | RegexOptions.Singleline);
            //ss = Regex.Replace(ss, "(<img.+?>)", "", RegexOptions.IgnoreCase | RegexOptions.Singleline);
            //ss = Regex.Replace(ss, "(<o:.+?</o:.+?>)", "", RegexOptions.IgnoreCase | RegexOptions.Singleline);
            //ss = Regex.Replace(ss, "<!--.+?-->", "", RegexOptions.IgnoreCase | RegexOptions.Singleline);
            //ss = Regex.Replace(ss, "class=.+?>", ">", RegexOptions.IgnoreCase | RegexOptions.Singleline);
            //ss = Regex.Replace(ss.Replace(System.Environment.NewLine, "<br/>"), "<[^(a|img|b|i|u|ul|ol|li)][^>]*>", " ");
            //ss = ss.Replace("&nbsp;", "");
            //ss = ss.Replace("\n;", string.Empty);

            //ss = Regex.Replace(ss, @"\t|\n|\r", "");

            //ss = Regex.Replace(ss, "<.*?>|&.*?;", string.Empty);



            #endregion



        }
        public SearchinDataViewModel()
        {
            if (sc == null)
            {
                sc = new ShipmentContaxt();
                sc.Configuration.ProxyCreationEnabled = false;
            }

            var dt = sc.DocsPages.Where(x => x.Mid == 4).FirstOrDefault();


            if (dt != null)
            {
                DocsContant.Content = dt.Content; //"<p>Page title 7Page title 7Page title 7Page title 7Page title 7Page title 7Page title 7Page title 7<br></p>";
            }
            else
            {
                DocsContant.Content = "<p>Data not available.<br></p>";
            }

            cancelCommand = new RelayCommand(CancelSearchText);
            //try
            //{
            //    var check = StaticHelper.SearchText;
            //    GetSearchResult(check);
            //}
            //catch { }
        }

        //private void highlighttext()
        //{
        //    //firstgrid.Refresh();
        //    string textchnge = "bollard";

        //    //System.Net.Mime.MediaTypeNames.Application.DoEvents();

        //    if (DocsContant.Content != null)
        //    {

        //        mshtml.IHTMLDocument2 document = (mshtml.IHTMLDocument2)firstgrid.Document as IHTMLDocument2;
        //        if (document != null)
        //        {
        //            IHTMLSelectionObject currentSelection = document.selection;

        //            IHTMLTxtRange range = currentSelection.createRange() as IHTMLTxtRange;

        //            if (range != null)
        //            {
        //                String search = TextBox1.Text;
        //                if (search == "")
        //                {
        //                    //  MessageBox.Show("not selected");
        //                }
        //                else
        //                {
        //                line1:
        //                    if ((range.findText(search)) && (range.htmlText != "span style='background-color: rgb(255, 255, 0);'>" + textchnge.ToLower() + "</span>"))
        //                    {
        //                        range.select();
        //                        range.pasteHTML("<span style='background-color: rgb(255, 255, 0);'>" + textchnge.ToLower() + "</span>");
        //                        goto line1;

        //                    }
        //                }

        //            }
        //        }
        //    }
        //}
        private void CancelSearchText()
        {
            ChildWindowManager.Instance.CloseChildWindow();
        }

        private ICommand cancelCommand;
        public ICommand CancelCommand
        {
            get { return cancelCommand; }
        }



        private DocsPages datacontant = new DocsPages();

        public DocsPages DocsContant
        {
            get { return datacontant; }
            set
            {
                datacontant = value;
                RaisePropertyChanged("DocsContant");
            }
        }



        private void GetSearchResult(string SearchText)
        {

            try
            {
                LoadSearchTextDetail.Clear();

                SqlCommand cmd = new SqlCommand("GetSearchText", sc.con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@text", SearchText);

                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                DataTable ds = new DataTable();
                adp.Fill(ds);
                // var data = sc.AssignRopetoWinch.ToList();          
                foreach (DataRow row in ds.Rows)
                {
                    LoadSearchTextDetail.Add(new MenuNameClass()
                    {
                        Id = (int)row["id"],
                        Content = (string)row["Content"],
                        MenuName = (string)row["MenuName"],
                        Mid = (int)row["Mid"],

                    });
                }

                OnPropertyChanged(new PropertyChangedEventArgs("LoadSearchTextDetail"));
            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);
                /// return null;
            }

        }

        static bool Mso = false;
        static bool IgnoreSpans = false;
        static bool IgnoreDivs = false;
        static string CleanWordHtml(string html)
        {
            StringCollection sc = new StringCollection();
            if (!IgnoreSpans)
            {
                sc.Add(@"<(/?span|!\[)[^>]*?>");
            }
            if (!IgnoreDivs)
            {
                sc.Add(@"<(/?div|!\[)[^>]*?>");
            }
            if (!Mso)
            {
                // Get rid of classes
                sc.Add(@"\s?class=[""']?\w+[""']?");
            }
            else
            {
                // Get rid of office classes
                sc.Add(@"\s?class=[""']?Mso\w+[""']?");
            }
            // get rid of unnecessary tag spans (comments and title)
            sc.Add(@"<!--(\w|\W)+?-->");
            sc.Add(@"<title>(\w|\W)+?</title>");
            // get rid of inline style
            sc.Add(@"\s?style=[""']?\w+[""']?");
            // Get rid of unnecessary tags
            sc.Add(@"<(meta|link|/?o:|/?style|/?font|/?st\d|/?head|/?html|body|/?body|!\[)[^>]*?>");
            // Get rid of empty tags (except table cells)
            sc.Add(@"(<[^/][^(th|d)>]*>){1}(&nbsp;)*(</[^>]+>){1}");
            // remove bizarre v: element attached to <img> tag
            sc.Add(@"\s+v:\w+=""[^""]+""");
            // remove extra lines
            sc.Add(@"(" + Environment.NewLine + "){2,}");
            // remove extra spaces
            sc.Add(@"( ){2,}");
            foreach (string s in sc)
            {
                html = Regex.Replace(html, s, "", RegexOptions.IgnoreCase);
            }
            // quote unquoted attributes
            //html = Regex.Replace(html, @"(\w+=)(\w+)(?=[ >])", @"$1""$2""", RegexOptions.IgnoreCase);
            return html;
        }

        static string FixEntities(string html)
        {
            NameValueCollection nvc = new NameValueCollection();
            nvc.Add("“", "&ldquo;");
            nvc.Add("”", "&rdquo;");
            nvc.Add("—", "&mdash;");
            foreach (string key in nvc.Keys)
            {
                html = html.Replace(key, nvc[key]);
            }
            return html;
        }

        static bool IsNullOrEmpty(string value)
        {
            if (value != null)
            {
                return (value.Length == 0);
            }
            return true;
        }

        static string ReadAllText(string path)
        {
            StringBuilder sb = new StringBuilder();
            using (StreamReader sr = new StreamReader(path))
            {
                String line;
                // Read and display lines from the file until the end of 
                // the file is reached.
                while ((line = sr.ReadLine()) != null)
                {
                    sb.Append(line + Environment.NewLine);
                }
            }
            return sb.ToString();
        }

        static void WriteAllText(string path, string contents)
        {
            WriteAllText(path, contents, new UTF8Encoding(false, true));
        }

        static void WriteAllText(string path, string contents, Encoding encoding)
        {
            using (StreamWriter sw = new StreamWriter(path, false, encoding))
            {
                sw.Write(contents);
            }
        }



        public static ObservableCollection<MenuNameClass> loadsearchDetail = new ObservableCollection<MenuNameClass>();
        public ObservableCollection<MenuNameClass> LoadSearchTextDetail
        {
            get
            {
                return loadsearchDetail;
            }
            set
            {
                loadsearchDetail = value;
                OnPropertyChanged(new PropertyChangedEventArgs("LoadSearchTextDetail"));
            }
        }

        new public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, e);
        }

    }
}
