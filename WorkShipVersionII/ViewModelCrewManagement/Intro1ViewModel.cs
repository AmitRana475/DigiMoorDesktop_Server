using DataBuildingLayer;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WorkShipVersionII.ViewModel;

namespace WorkShipVersionII.ViewModelCrewManagement
{
    public class Intro1ViewModel : ViewModelBase
    {
        private readonly ShipmentContaxt sc;
        public Intro1ViewModel(int contentid)
        {
            if (sc == null)
            {
                sc = new ShipmentContaxt();
                sc.Configuration.ProxyCreationEnabled = false;
            }

     

            GetSearchResult("cargo");

            StaticHelper.ContentID = contentid;

            DocsContant.MenuTitle = StaticHelper.MenuTitle;

            var dt = sc.DocsPages.Where(x => x.Mid == contentid).FirstOrDefault();
            if (dt != null)
            {
                //var dfdf = dt.Content.Length();
                //dynamic doc = firstgrid.Document;

                StringBuilder sb = new StringBuilder();

                //string ss="<head>< meta http - equiv = 'Content-Type content' = text / html; charset = UTF - 8 >
                sb.Append("<head><meta http-equiv='Content-Type' content='text/html;charset=UTF-8'>");
                sb.Append(dt.Content);
                sb.Append("</head>");

               var  fragment = Regex.Replace(sb.ToString(), "<!--.*?-->", String.Empty, RegexOptions.Multiline);

                string abc = fragment;
                //abc = abc.Replace('_', '-');


                //fragment=  Regex.Replace(fragment, "<.*?>", String.Empty);


                // fragment = System.Net.WebUtility.HtmlEncode(fragment);

                //Encoding.Convert(Encoding.Default, Encoding.UTF8, Encoding.Default.GetBytes(fragment));

                //string s = "søme string";
                //s = Regex.Replace(s, @"[^\u0000-\u007F]", "");

                // To strip non-ASCII characters from a string without using regular expression in C#


                //string asAscii = Encoding.ASCII.GetString(
                // Encoding.Convert(
                //  Encoding.UTF8,
                //  Encoding.GetEncoding(
                //   Encoding.ASCII.EncodingName,
                //   new EncoderReplacementFallback(string.Empty),
                //   new DecoderExceptionFallback()
                //   ),
                //  Encoding.UTF8.GetBytes(fragment)
                // )
                //);
                //fragment = asAscii;

                DocsContant.Content = abc; //"<p>Page title 7Page title 7Page title 7Page title 7Page title 7Page title 7Page title 7Page title 7<br></p>";
            }
            else
            {
                DocsContant.Content = "<p>Data not available.<br></p>";
            }


            //MainViewModel.LoderVisibility = "Hidden";
            //RaisePropertyChanged("MyLoderVisibility");

            //try
            //{
            //    var check = StaticHelper.SearchText;
            //    GetSearchResult(check);
            //}
            //catch { }

        }

        public Image LoadImage()
        {
            //data:image/gif;base64,
            //this image is a single pixel (black)
            byte[] bytes = Convert.FromBase64String("R0lGODlhAQABAIAAAAAAAAAAACH5BAAAAAAALAAAAAABAAEAAAICTAEAOw==");

            Image image;
            using (MemoryStream ms = new MemoryStream(bytes))
            {
                image = Image.FromStream(ms);
            }

            return image;
        }
        public Intro1ViewModel()
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

            //try
            //{
            //    var check = StaticHelper.SearchText;
            //    GetSearchResult(check);
            //}
            //catch { }
        }

      //  public static DocsPages datacontant = new DocsPages();

        private DocsPages datacontant = new DocsPages();
        public  DocsPages DocsContant
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
                        //MenuName = (string)row["MenuName"],

                        MenuName = (row["MenuName"] == DBNull.Value) ? string.Empty : row["MenuName"].ToString(),
                        Mid = (int)row["Mid"],

                    });
                }

                OnPropertyChanged(new PropertyChangedEventArgs("LoadSearchTextDetail"));
            }
            catch (Exception ex)
            {
               // sc.ErrorLog(ex);
                /// return null;
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
