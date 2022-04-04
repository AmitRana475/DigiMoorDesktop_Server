using DataBuildingLayer;using DocumentFormat.OpenXml.Wordprocessing;using GalaSoft.MvvmLight;using GalaSoft.MvvmLight.Command;using iTextSharp.text.pdf;//using iTextSharp.tool.xml;//using Microsoft.Reporting.Map.WebForms.BingMaps;using Org.BouncyCastle.Asn1.Ocsp;using System;using System.Data;using System.Data.SqlClient;using System.IO;using System.Linq;using System.Text;using System.Windows;using System.Windows.Input;using WorkShipVersionII.Views;using iTextSharp.text;using iTextSharp.text.html.simpleparser;using System.Text.RegularExpressions;using iTextSharp.tool.xml.pipeline.end;using iTextSharp.tool.xml.html;using iTextSharp.tool.xml.pipeline.html;using iTextSharp.tool.xml.pipeline.css;using iTextSharp.tool.xml.parser;using System.Drawing.Printing;using System.Drawing;using System.Windows.Controls;using System.Windows.Documents;using System.Collections.Generic;using System.Net;using WorkShipVersionII.ViewModel;using Tag = iTextSharp.tool.xml.Tag;using System.Diagnostics;using iTextSharp.tool.xml.css;
using iTextSharp.tool.xml;
using System.Collections.Specialized;
using System.ComponentModel;
using WorkShipVersionII.ViewsCrewManagement;

namespace WorkShipVersionII.ViewModelCrewManagement
{
    public class MainViewModelCrewManagement : ViewModelBase
    {
        private readonly ShipmentContaxt sc;

        public ICommand HelpCommand { get; private set; }

        public MainViewModelCrewManagement()
        {
          
            if (sc == null)
            {
                sc = new ShipmentContaxt();
                sc.Configuration.ProxyCreationEnabled = false;
            }
            //var viemodel=new CrewManagementView();
            //viemodel.MenuTitle.Text = StaticHelper.MenuTitle;

          

            // CurrentViewModelCrew = MainViewModelCrewManagement._mSMPDefaultViewModel;
            NavCommand = new MyCommand<string>(OnNavCommand);


            TabCommand = new MyCommand<string>(OnTabCommand);

            HelpCommand = new RelayCommand(() => StaticHelper.HelpMethod(StaticHelper.HelpFor));

            var onload = sc.DocsPages.FirstOrDefault(); ;

            if (onload != null)
            {

                OnNavCommand(onload.Mid.ToString());
            }


            NextBtnCommand = new MyCommand<string>(OnNextBtnCommand);
            PreviousBtnCommand = new MyCommand<string>(OnPreviousBtnCommand);


        }



        public static ViewModelBase _currentViewModelcrew;
        public ViewModelBase CurrentViewModelCrew
        {
            get
            {
                return _currentViewModelcrew;
            }
            set
            {
                Set(ref _currentViewModelcrew, value);


            }
        }


        public static CommonPropertiesVisibilety _CommonVisible = new CommonPropertiesVisibilety();
        public CommonPropertiesVisibilety CommonVisible
        {
            get
            {
                if (_CommonVisible == null)
                    _CommonVisible = new CommonPropertiesVisibilety();
                return _CommonVisible;
            }
            set
            {

                _CommonVisible = value;
                RaisePropertyChanged("CommonVisible");
            }
        }




        public static Intro1ViewModel _intro1ViewModel = new Intro1ViewModel();
        public static CrewManagementViewModel _crewmanagementViewModel = new CrewManagementViewModel();

        readonly static MSMPDefaultViewModel _mSMPDefaultViewModel = new MSMPDefaultViewModel();
        ShipSpecificDataViewModel _ShipSpecificDataViewModel = new ShipSpecificDataViewModel();

        Intro1ViewModel _intro1ViewModel1 = new Intro1ViewModel();

        ShipAttachViewModel _shipAttachViewModel = new ShipAttachViewModel();

        SearchTextViewModel _searchTextViewModel = new SearchTextViewModel();
        RevisionViewModel _revisionViewModel = new RevisionViewModel();

        public static MyCommand<string> NavCommand { get; private set; }
        public static MyCommand<string> NextBtnCommand { get; private set; }

        public static MyCommand<string> PreviousBtnCommand { get; private set; }
        public static MyCommand<string> TabCommand { get; private set; }

        

        private void HelpMethod()
        {
            //MessageBox.Show("may i help you!");
            try
            {
                // MessageBox.Show("Hi");
                string Class1Helpfor = "addCrew";
                var path = AppDomain.CurrentDomain.BaseDirectory + @"WorkshipManual\" + "workship.chm";//, Class1Helpfor +".htm"; //"shipment.sdf"; // tt
                System.Windows.Forms.Help.ShowHelp(null, path, Class1Helpfor + ".htm");
            }
            catch
            {
            }
        }

        private void OnTabCommand(string Tab)
        {
            if (Tab == "GData")
            {

                StaticHelper.ShipSpecData = "GData";
                _intro1ViewModel = new Intro1ViewModel(SidemenuId);
                CurrentViewModelCrew = _intro1ViewModel;




            }
            if (Tab == "PDFGenerate")
            {
                int mid = SidemenuId;

                StaticHelper.MenuID = mid;

                try
                {
                    SqlDataAdapter adp1 = new SqlDataAdapter("select RPrefix from Revision where mid=" + mid + "", sc.con);
                    DataTable ddt = new DataTable();
                    adp1.Fill(ddt);

                    if(ddt.Rows.Count>0)
                    {
                        StaticHelper.RNumber = ddt.Rows[0][0].ToString();
                    }
                }
                catch { }

                if (StaticHelper.ShipSpecData == "GData")
                {

                    SqlDataAdapter adp = new SqlDataAdapter("select content from docspages where mid=" + mid + "", sc.con);
                    DataTable dt = new DataTable();
                    adp.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        string content = dt.Rows[0][0].ToString();
                        string CSS = @"body {font-size: 12px;} table {border-collapse:collapse; margin:8px;}.light-yellow {background-color:#ffff99;}td {border:1px solid #ccc;padding:4px;}";

                        // CreatePDF(content);
                        ConvertHtmlToPdf(content, CSS);
                    }
                }

                if (StaticHelper.ShipSpecData == "ShipSpecData")
                {

                    SqlDataAdapter adp = new SqlDataAdapter("select content from tblShipSpecificContent where mid=" + mid + "", sc.con);
                    DataTable dt = new DataTable();
                    adp.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        string content = dt.Rows[0][0].ToString();
                        string CSS = @"body {font-size: 12px;} table {border-collapse:collapse; margin:8px;}.light-yellow {background-color:#ffff99;}td {border:1px solid #ccc;padding:4px;}";

                        // CreatePDF(content);
                        ConvertHtmlToPdf(content, CSS);
                    }
                }

            }
            else if (Tab == "Printing")
            {
                int mid = SidemenuId;

                SqlDataAdapter adp = new SqlDataAdapter("select content from docspages where mid=" + mid + "", sc.con);
                DataTable dt = new DataTable();
                adp.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    string content = dt.Rows[0][0].ToString();
                    string s = content;
                    string CSS = @"body {font-size: 12px;} table {border-collapse:collapse; margin:8px;}.light-yellow {background-color:#ffff99;}td {border:1px solid #ccc;padding:4px;}";
                    //var ss = Printing(s, CSS);
                    PrintDocument p = new PrintDocument();
                    p.PrintPage += delegate (object sender1, PrintPageEventArgs e1)
                    {
                        //System.Windows.Forms.PrintPreviewDialog printPrvDlg = new System.Windows.Forms.PrintPreviewDialog();
                        //printPrvDlg.Document = p;
                        // printPrvDlg.ShowDialog();



                        e1.Graphics.DrawString(s, new System.Drawing.Font("Times New Roman", 12), new SolidBrush(System.Drawing.Color.Black), new RectangleF(0, 0, p.DefaultPageSettings.PrintableArea.Width, p.DefaultPageSettings.PrintableArea.Height));

                    };
                    try
                    {
                        p.Print();
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Exception Occured While Printing", ex);
                    }
                }

                // PrintDocument pd = new PrintDocument();
                // pd.PrintPage += new PrintPageEventHandler(this.pd_PrintPage);

                //System.Windows.Controls. PrintDialog printdlg = new System.Windows.Controls.PrintDialog();
                // System.Windows.Forms.PrintPreviewDialog printPrvDlg = new System.Windows.Forms.PrintPreviewDialog();

                // // preview the assigned document or you can create a different previewButton for it
                // printPrvDlg.Document = pd;
                // printPrvDlg.ShowDialog(); // this shows the preview and then show the Printer Dlg below

                // printdlg.Document = pd;

                // if (printdlg.ShowDialog() == DialogResult.OK)
                // {
                //     pd.Print();
                // }

            }
            else if (Tab == "ShipData")
            {

                StaticHelper.ShipSpecData = "ShipSpecData";
                _ShipSpecificDataViewModel = new ShipSpecificDataViewModel(SidemenuId);
                CurrentViewModelCrew = _ShipSpecificDataViewModel;
            }
            else if (Tab == "ShipAttach")
            {
                _shipAttachViewModel = new ShipAttachViewModel(SidemenuId);
                CurrentViewModelCrew = _shipAttachViewModel;
            }
            else if (Tab == "SearchText")
            {
                _searchTextViewModel = new SearchTextViewModel(SidemenuId);
                CurrentViewModelCrew = _searchTextViewModel;
            }
            else if (Tab == "Revision")
            {

                _revisionViewModel = new RevisionViewModel(SidemenuId);
                CurrentViewModelCrew = _revisionViewModel;
            }
        }
        //private void ss()
        //{
        //    PrintDialog printDialog = new PrintDialog();
        //    if (printDialog.ShowDialog() == true)
        //    {
        //        printDialog.PrintVisual(grid, "My First Print Job");

        //    }
        //}
        private FlowDocument CreateFlowDocument()
        {
            // Create a FlowDocument  
            FlowDocument doc = new FlowDocument();
            // Create a Section  
            System.Collections.Specialized.BitVector32.Section sec = new System.Collections.Specialized.BitVector32.Section();
            // Create first Paragraph  
            System.Windows.Documents.Paragraph p1 = new System.Windows.Documents.Paragraph();
            // Create and add a new Bold, Italic and Underline  
            System.Windows.Documents.Bold bld = new System.Windows.Documents.Bold();
            bld.Inlines.Add(new System.Windows.Documents.Run("First Paragraph"));
            System.Windows.Documents.Italic italicBld = new System.Windows.Documents.Italic();
            italicBld.Inlines.Add(bld);
            System.Windows.Documents.Underline underlineItalicBld = new System.Windows.Documents.Underline();
            underlineItalicBld.Inlines.Add(italicBld);
            // Add Bold, Italic, Underline to Paragraph  
            p1.Inlines.Add(underlineItalicBld);
            // Add Paragraph to Section  
            //sec.Blocks.Add(p1);
            // Add Section to FlowDocument  
            //doc.Blocks.Add(sec);
            return doc;
        }

        public string Printing(string xHtml, string css)
        {
            try
            {
                xHtml = xHtml.Replace("<br>", "<br />");


                xHtml = Regex.Replace(xHtml, "(?<image><img[^>]+)(?<=[^/])>", new MatchEvaluator(match => match.Groups["image"].Value + " />"), RegexOptions.IgnoreCase | RegexOptions.Multiline);


                System.Windows.Forms.SaveFileDialog dlg = new System.Windows.Forms.SaveFileDialog();
                //dlg.FileName = "Print_" + DateTime.Now.ToString("dd-MMM-yyyy");
                //dlg.DefaultExt = ".pdf";
                //dlg.Filter = "PDF Files|*.pdf";
                //if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                //{
                //    if (File.Exists(dlg.FileName))
                //    {
                //        File.Delete(dlg.FileName);
                //    }
                //}
                using (var stream = new FileStream(@"C:\DigiMoorDB_Backup\Attachmentss", FileMode.Create))
                {
                    using (var document = new iTextSharp.text.Document())
                    {
                        var writer = PdfWriter.GetInstance(document, stream);
                        document.Open();

                        // instantiate custom tag processor and add to `HtmlPipelineContext`.
                        var tagProcessorFactory = Tags.GetHtmlTagProcessorFactory();
                        //tagProcessorFactory.AddProcessor(
                        //     (),
                        //    new string[] { HTML.Tag.TD }
                        //);
                        var htmlPipelineContext = new HtmlPipelineContext(null);
                        htmlPipelineContext.SetTagFactory(tagProcessorFactory);

                        var pdfWriterPipeline = new PdfWriterPipeline(document, writer);
                        var htmlPipeline = new HtmlPipeline(htmlPipelineContext, pdfWriterPipeline);

                        // get an ICssResolver and add the custom CSS
                        var cssResolver = XMLWorkerHelper.GetInstance().GetDefaultCssResolver(true);
                        cssResolver.AddCss(css, "utf -8", true);
                        var cssResolverPipeline = new CssResolverPipeline(
                            cssResolver, htmlPipeline
                        );

                        var worker = new XMLWorker(cssResolverPipeline, true);
                        var parser = new XMLParser(worker);
                        using (var stringReader = new StringReader(xHtml))
                        {
                            parser.Parse(stringReader);
                        }
                    }
                }

                return xHtml;
            }
            catch (Exception ex)
            {
                MessageBox.Show("HTML not in proper format !", "HTML ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                return xHtml;
            }
        }



        public void ConvertHtmlToPdf(string xHtml, string css)
        {
            try
            {
                xHtml = xHtml.Replace("<br>", "<br/>");
                xHtml = xHtml.Replace("width: 50%;", "width: 200%;");               
                xHtml = Regex.Replace(xHtml, "(?<image><img[^>]+)(?<=[^/])>", new MatchEvaluator(match => match.Groups["image"].Value + "  />"), RegexOptions.IgnoreCase | RegexOptions.Multiline);
                System.Windows.Forms.SaveFileDialog dlg = new System.Windows.Forms.SaveFileDialog();
                dlg.FileName = "DigiMoorX7_MSMPR_PDF_" + DateTime.Now.ToString("dd-MMM-yyyy_HH_mm_ss");
                dlg.DefaultExt = ".pdf";
                dlg.Filter = "PDF Files|*.pdf";
                if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    if (File.Exists(dlg.FileName))
                    {
                        File.Delete(dlg.FileName);
                    }
                }               
                using (var stream = new FileStream(dlg.FileName, FileMode.Create))
                {
                    using (var document = new iTextSharp.text.Document(iTextSharp.text.PageSize.A4, 10f, 10f, 140f, 10f))
                    {
                        HTMLWorker htmlparser = new HTMLWorker(document);
                       // string mypath = @"C:\DigiMoorDB_Backup\InspectionImages\water-drop-300KB-2.jpg";                       
                        var writer = PdfWriter.GetInstance(document, stream);
                        writer.PageEvent = new WorkShipVersionII.ITextEvents();
                        // document.AddHeader = new HeaderFooter(new Phrase("Header Text"), false);
                        document.NewPage();
                        document.Open();
                        var tagProcessorFactory = (DefaultTagProcessorFactory)Tags.GetHtmlTagProcessorFactory();
                        tagProcessorFactory.RemoveProcessor(HTML.Tag.IMG); // remove the default processor
                        tagProcessorFactory.AddProcessor(HTML.Tag.IMG, new CustomImageTagProcessor()); // use our new processor
                        var htmlPipelineContext = new HtmlPipelineContext(null);
                        htmlPipelineContext.SetTagFactory(tagProcessorFactory);                     
                        var pdfWriterPipeline = new PdfWriterPipeline(document, writer);
                        var htmlPipeline = new HtmlPipeline(htmlPipelineContext, pdfWriterPipeline);
                        // get an ICssResolver and add the custom CSS
                        var cssResolver = XMLWorkerHelper.GetInstance().GetDefaultCssResolver(true);
                        cssResolver.AddCss(css, "utf-8", true);
                        var cssResolverPipeline = new CssResolverPipeline(cssResolver, htmlPipeline);
                        xHtml = xHtml.Replace("\r", "\n").Replace("\n", "");// you html code (for example table from your page)
                        var memoryStream = new MemoryStream();
                        var worker = new XMLWorker(cssResolverPipeline, true);
                        var charset = Encoding.UTF8;
                        //var xmlParser = new XMLParser(true, worker, charset);
                        var xmlParser = new XMLParser(true, worker, charset);
                        writer.RgbTransparencyBlending = true;
                        PdfPTable table = new PdfPTable(3);
                        table.HeaderRows = 1;
                        using (var stringReader = new StringReader(xHtml))
                        {
                            try
                            {
                                //CreatePDF(xHtml);
                                htmlparser.Parse(stringReader);
                                xmlParser.Parse(stringReader);

                                writer.CloseStream = false;
                                document.Close();
                                memoryStream.Position = 0;

                                MessageBox.Show("PDF generated successfully !", "Mooring Manual", MessageBoxButton.OK, MessageBoxImage.Information);
                            }
                            catch (Exception ex7)
                            {
                                using (var stringReader1 = new StringReader(xHtml))
                                {                                    
                                    xmlParser.Parse(stringReader1);

                                    writer.CloseStream = false;
                                    document.Close();
                                    memoryStream.Position = 0;
                                }

                                MessageBox.Show("PDF generated successfully !", "Mooring Manual", MessageBoxButton.OK, MessageBoxImage.Information);
                            }
                            //xmlParser.Parse(stringReader);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("HTML not in proper format !", "HTML ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
      
        static void WriteAllText(string path, string contents, Encoding encoding)
        {
            using (StreamWriter sw = new StreamWriter(path, false, encoding))
            {
                sw.Write(contents);
            }
        }
        private static string htmml;
        private void CreatePDF(string html)
        {
          html=  CleanWordHtml(html);
            html = FixEntities(html);


            System.Windows.Forms.SaveFileDialog dlg = new System.Windows.Forms.SaveFileDialog();
            dlg.FileName = "DigiMoorX7_MSMPR_PDF_" + DateTime.Now.ToString("dd-MMM-yyyy_HH_mm_ss");
            dlg.DefaultExt = ".pdf";
            dlg.Filter = "PDF Files|*.pdf";
            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (File.Exists(dlg.FileName))
                {
                    File.Delete(dlg.FileName);
                }
            }
          

            using (FileStream msReport = new FileStream(dlg.FileName, FileMode.Create))
            {
                //step 1  
                using (iTextSharp.text.Document pdfDoc = new iTextSharp.text.Document(iTextSharp.text.PageSize.A4, 10f, 10f, 140f, 10f))
                {
                    try
                    {
                        // step 2  
                        PdfWriter pdfWriter = PdfWriter.GetInstance(pdfDoc, msReport);
                        pdfWriter.PageEvent = new WorkShipVersionII.ITextEvents();


                        //HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
                        //var stringReader = new StringReader(html);

                        //htmlparser.Parse(stringReader);
                     
                        //open the stream   
                        pdfDoc.Open();

                        for (int i = 0; i < 5; i++)
                        {
                            iTextSharp.text.Paragraph para = new iTextSharp.text.Paragraph(html, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10));
                            para.Alignment = Element.ALIGN_CENTER;
                            pdfDoc.Add(para);
                            pdfDoc.NewPage();
                        }

                        pdfDoc.Close();
                    }
                    catch (Exception ex)
                    {
                        //handle exception  
                    }
                    finally
                    {
                    }
                }
            }
        }


        public static string AutoCloseHtmlTags(string inputHtml)
        {
            var regexStartTag = new Regex(@"<(!--\u002E\u002E\u002E--|!DOCTYPE|a|abbr|" +
                  @"acronym|address|applet|area|article|aside|audio|b|base|basefont|bdi|bdo|big" +
                  @"|blockquote|body|br|button|canvas|caption|center|cite|code|col|colgroup|" +
                  @"command|datalist|dd|del|details|dfn|dialog|dir|div|dl|dt|em|embed|fieldset|" +
                  @"figcaption|figure|font|footer|form|frame|frameset|h1> to <h6|head|" +
                  @"header|hr|html|i|iframe|img|input|ins|kbd|keygen|label|legend|li|link|" +
                  @"map|mark|menu|meta|meter|nav|noframes|noscript|object|ol|optgroup|option|" +
                  @"output|p|param|pre|progress|q|rp|rt|ruby|s|samp|script|section|select|small|" +
                  @"source|span|strike|strong|style|sub|summary|sup|table|tbody|td|textarea|" +
                  @"tfoot|th|thead|time|title|tr|track|tt|u|ul|var|video|wbr)(\s\w+.*(\u0022|'))?>");
            var startTagCollection = regexStartTag.Matches(inputHtml);
            var regexCloseTag = new Regex(@"</(!--\u002E\u002E\u002E--|!DOCTYPE|a|abbr|" +
                  @"acronym|address|applet|area|article|aside|audio|b|base|basefont|bdi|bdo|" +
                  @"big|blockquote|body|br|button|canvas|caption|center|cite|code|col|colgroup|" +
                  @"command|datalist|dd|del|details|dfn|dialog|dir|div|dl|dt|em|embed|fieldset|" +
                  @"figcaption|figure|font|footer|form|frame|frameset|h1> to <h6|head|header" +
                  @"|hr|html|i|iframe|img|input|ins|kbd|keygen|label|legend|li|link|map|mark|menu|" +
                  @"meta|meter|nav|noframes|noscript|object|ol|optgroup|option|output|p|param|pre|" +
                  @"progress|q|rp|rt|ruby|s|samp|script|section|select|small|source|span|strike|" +
                  @"strong|style|sub|summary|sup|table|tbody|td|textarea|tfoot|th|thead|" +
                  @"time|title|tr|track|tt|u|ul|var|video|wbr)>");
            var closeTagCollection = regexCloseTag.Matches(inputHtml);
            var startTagList = new List<string>();
            var closeTagList = new List<string>();
            var resultClose = "";
            foreach (Match startTag in startTagCollection)
            {
                startTagList.Add(startTag.Value);
            }
            foreach (Match closeTag in closeTagCollection)
            {
                closeTagList.Add(closeTag.Value);
            }
            startTagList.Reverse();
            for (int i = 0; i < closeTagList.Count; i++)
            {
                if (startTagList[i] != closeTagList[i])
                {
                    int indexOfSpace = startTagList[i].IndexOf(
                             " ", System.StringComparison.Ordinal);
                    if (startTagList[i].Contains(" "))
                    {
                        startTagList[i].Remove(indexOfSpace);
                    }
                    startTagList[i] = startTagList[i].Replace("<", "</");
                    resultClose += startTagList[i] + ">";
                    resultClose = resultClose.Replace(">>", ">");
                }
            }
            return inputHtml + resultClose;
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

       


        private string HtmlToString(string htmlcode)
        {
            string url = "http://www.google.com/";
            string strHtml = htmlcode;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            if (response.StatusCode == HttpStatusCode.OK)
            {
                Stream receiveStream = response.GetResponseStream();
                StreamReader readStream = null;
                if (response.CharacterSet == null)
                    readStream = new StreamReader(receiveStream);
                else
                    readStream = new StreamReader(receiveStream, Encoding.GetEncoding(response.CharacterSet));
                strHtml = readStream.ReadToEnd();
                response.Close();
                readStream.Close();
            }

            return strHtml;
        }


        //PrintDocument myPrintDocument = new PrintDocument();

        //public void ShowPrintDialog()
        //{
        //   System.Windows.Controls. PrintDialog myDialog = new System.Windows.Controls.PrintDialog();
        //    myDialog.Document = myPrintDocument;
        //    if (myDialog.ShowDialog() == System.Windows.Forms. DialogResult.OK)
        //        Print();
        //}

        //public static Byte[] PdfSharpConvert(String html)
        //{
        //    Byte[] res = null;
        //    using (MemoryStream ms = new MemoryStream())
        //    {
        //        var pdf = TheArtOfDev.HtmlRenderer.PdfSharp.PdfGenerator.GeneratePdf(html, PdfSharp.PageSize.A4);
        //        pdf.Save(ms);
        //        res = ms.ToArray();
        //    }
        //    return res;
        //}
        public int SidemenuId { get; set; }
        private void OnNavCommand(string destination)
        {
            SqlDataAdapter pp = new SqlDataAdapter("select menuname from tblmenuname where mid=" + destination + "", sc.con);
            DataTable ddt = new DataTable();
            pp.Fill(ddt);
            if (ddt.Rows.Count > 0)
            {
                string mt = ddt.Rows[0]["MenuName"].ToString();
                StaticHelper.MenuTitle = mt;
            }


            SidemenuId = Convert.ToInt32(destination);
            _intro1ViewModel = new Intro1ViewModel(SidemenuId);
            CurrentViewModelCrew = _intro1ViewModel;




        }


        private void OnNextBtnCommand(string ss)
        {
            try
            {
              //MainViewModel.LoderVisibility = "Visible";
              //  RaisePropertyChanged("MyLoderVisibility");

                //SqlDataAdapter adp = new SqlDataAdapter("SELECT LAG(Mid) OVER ( ORDER BY Mid ) AS PreviousID,Mid,LEAD(Mid) OVER ( ORDER BY Mid ) AS NextID FROM tblMenuName where type !=1", sc.con);
                SqlDataAdapter adp = new SqlDataAdapter("GetNextPrevious", sc.con);
                adp.SelectCommand.CommandType = CommandType.StoredProcedure;
                DataTable dt = new DataTable();
                adp.Fill(dt);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    int mid = Convert.ToInt32(dt.Rows[i]["Mid"]);

                    if (mid == StaticHelper.ContentID)
                    {
                        CrewManagementView._menutitles.MenuTitle = dt.Rows[i]["MenuName"].ToString();

                     

                       // CrewManagementViewModel dd = new CrewManagementViewModel();
                       

                     

                        //CrewManagementView ds = new CrewManagementView();
                        //ds.DynamicMenus();
                        //ds.MenuItemClick1();
           
                        
                        // OnPropertyChanged(new PropertyChangedEventArgs("MenuTitle"));

                        // RaisePropertyChanged("MenuTitles");

                        int nextid = Convert.ToInt32(dt.Rows[i]["NextID"]);
                        SidemenuId = nextid;


                        SqlDataAdapter pp = new SqlDataAdapter("select menuname from tblmenuname where mid=" + nextid + "", sc.con);
                        DataTable ddt = new DataTable();
                        pp.Fill(ddt);
                        if (ddt.Rows.Count > 0)
                        {
                            string mt = ddt.Rows[0]["MenuName"].ToString();
                            StaticHelper.MenuTitle = mt;
                        }

                        _intro1ViewModel = new Intro1ViewModel(SidemenuId);
                        CurrentViewModelCrew = _intro1ViewModel;



                        break;
                    }
                }
            }
            catch(Exception ex) { }
        }

        new public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, e);
        }

        private void OnPreviousBtnCommand(string ss)
        {
            try
            {
                //SqlDataAdapter adp = new SqlDataAdapter("SELECT LAG(Mid) OVER ( ORDER BY Mid ) AS PreviousID,Mid,LEAD(Mid) OVER ( ORDER BY Mid ) AS NextID FROM tblMenuName where type !=1", sc.con);
                SqlDataAdapter adp = new SqlDataAdapter("GetNextPrevious", sc.con);
                adp.SelectCommand.CommandType = CommandType.StoredProcedure;
                DataTable dt = new DataTable();
                adp.Fill(dt);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    int mid = Convert.ToInt32(dt.Rows[i]["Mid"]);

                    if (mid == StaticHelper.ContentID)
                    {
                        CrewManagementView._menutitles.MenuTitle = dt.Rows[i]["MenuName"].ToString();
                        OnPropertyChanged(new PropertyChangedEventArgs("MenuTitle"));

                        int nextid = Convert.ToInt32(dt.Rows[i]["PreviousID"]);
                        SidemenuId = nextid;

                        SqlDataAdapter pp = new SqlDataAdapter("select menuname from tblmenuname where mid=" + nextid + "", sc.con);
                        DataTable ddt = new DataTable();
                        pp.Fill(ddt);
                        if (ddt.Rows.Count > 0)
                        {
                            string mt = ddt.Rows[0]["MenuName"].ToString();
                            StaticHelper.MenuTitle = mt;
                        }


                        _intro1ViewModel = new Intro1ViewModel(SidemenuId);
                        CurrentViewModelCrew = _intro1ViewModel;
                        break;
                    }
                }
            }
            catch { }
        }

        public void Confirmation()
        {

            var result = MessageBox.Show("Close Application?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {

            }

            //if (MessageBox.Show("Do you want to leave this page without saving record?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No)
            //{
            //}
        }


        public override void Cleanup()
        {
            _intro1ViewModel.Cleanup();
            _mSMPDefaultViewModel.Cleanup();
            _ShipSpecificDataViewModel.Cleanup();
            _shipAttachViewModel.Cleanup();
            _searchTextViewModel.Cleanup();

        }

    }

    public class CustomImageTagProcessor : iTextSharp.tool.xml.html.Image
    {
        public override IList<IElement> End(IWorkerContext ctx, Tag tag, IList<IElement> currentContent)
        {
            IDictionary<string, string> attributes = tag.Attributes;
            string src;
            if (!attributes.TryGetValue(HTML.Attribute.SRC, out src))
                return new List<IElement>(1);

            if (string.IsNullOrEmpty(src))
                return new List<IElement>(1);

            if (src.StartsWith("data:image/", StringComparison.InvariantCultureIgnoreCase))
            {
                // data:[<MIME-type>][;charset=<encoding>][;base64],<data>
                var base64Data = src.Substring(src.IndexOf(",") + 1);
                var imagedata = Convert.FromBase64String(base64Data);
                var image = iTextSharp.text.Image.GetInstance(imagedata);

                var list = new List<IElement>();
                var htmlPipelineContext = GetHtmlPipelineContext(ctx);
                list.Add(GetCssAppliers().Apply(new Chunk((iTextSharp.text.Image)GetCssAppliers().Apply(image, tag, htmlPipelineContext), 0, 0, true), tag, htmlPipelineContext));
                return list;
            }
            else
            {
                return base.End(ctx, tag, currentContent);
            }
        }
    }
    public class HoliProperty
    {
        public static int Gid { get; set; }
        public static string GroupName { get; set; }
    }
    
}
