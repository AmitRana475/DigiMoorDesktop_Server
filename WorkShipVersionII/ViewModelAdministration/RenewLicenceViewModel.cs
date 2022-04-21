using DataBuildingLayer;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using WorkShipVersionII.ViewModel;
using WorkShipVersionII.ViewModelCrewManagement;

namespace WorkShipVersionII.ViewModelAdministration
{
       public class RenewLicenceViewModel : ViewModelBase
       {
              private int erinfo;
              private readonly ShipmentContaxt sc;
              public ICommand HelpCommand { get; private set; }
              public RenewLicenceViewModel()
              {
                     if (sc == null)
                     {
                            sc = new ShipmentContaxt();
                            sc.Configuration.ProxyCreationEnabled = false;
                     }
                     _saveLicenceCommand = new RelayCommand<ProductInfoClass>(SaveLicence);
                     _cancelLicenceCommand = new RelayCommand(CancelLicence);


                     HelpCommand = new RelayCommand(() => StaticHelper.HelpMethod(StaticHelper.HelpFor));

              }

              private ICommand _saveLicenceCommand;
              public ICommand SaveCommand
              {
                     get { return _saveLicenceCommand; }
              }

              private ICommand _cancelLicenceCommand;
              public ICommand CancelCommand
              {
                     get { return _cancelLicenceCommand; }
              }

              private static ProductInfoClass productValidate = new ProductInfoClass();
              public ProductInfoClass ProductValidate
              {
                     get
                     {
                            if (erinfo != 0)
                            {
                                   RefreshMessageKey(productValidate);
                                   RaisePropertyChanged("ProductMessage");
                            }
                            return productValidate;
                     }
                     set
                     {
                            productValidate = value;
                            OnPropertyChanged(new PropertyChangedEventArgs("ProductValidate"));
                     }
              }

              private string productMessage;
              public string ProductMessage
              {
                     get
                     {
                            return productMessage;
                     }
                     set
                     {
                            productMessage = value;
                            RaisePropertyChanged("ProductMessage");
                     }
              }

              private void RefreshMessageKey(ProductInfoClass obj)
              {
                     try
                     {
                            // if (string.IsNullOrEmpty(obj.Text1) || string.IsNullOrEmpty(obj.Text2) || string.IsNullOrEmpty(obj.Text3) || string.IsNullOrEmpty(obj.Text4) || string.IsNullOrEmpty(obj.Text5) || obj.Text1.Length < 4 || obj.Text2.Length < 4 || obj.Text3.Length < 4 || obj.Text4.Length < 4 || obj.Text5.Length < 4)
                            if (string.IsNullOrEmpty(obj.TextMain))
                            {
                                   CheckErrorMessage.CheckErrorMessages = false;
                                   productMessage = "Please Enter the Licence Key";
                                   RaisePropertyChanged("ProductMessage");
                            }
                            else if (string.IsNullOrEmpty(obj.TextMain.Trim()))
                            {
                                   CheckErrorMessage.CheckErrorMessages = false;
                                   productMessage = "Please Enter the Licence Key";
                                   RaisePropertyChanged("ProductMessage");
                            }
                            else //if (!string.IsNullOrEmpty(obj.Text1) && !string.IsNullOrEmpty(obj.Text2) && !string.IsNullOrEmpty(obj.Text3) && !string.IsNullOrEmpty(obj.Text4) && !string.IsNullOrEmpty(obj.Text5))
                            {
                                   CheckErrorMessage.CheckErrorMessages = true;
                                   productMessage = string.Empty;
                                   RaisePropertyChanged("ProductMessage");
                            }
                     }
                     catch (Exception ex)
                     {
                            sc.ErrorLog(ex);
                     }
              }
              private VesselClass GetVesselInfo()
              {
                     VesselClass vessel = new VesselClass();
                     string qry = "select * from Vessel";
                     using (SqlDataAdapter sda = new SqlDataAdapter(qry, sc.con))
                     {
                            DataTable dtp = new DataTable();
                            sda.Fill(dtp);
                            if (dtp.Rows.Count > 0)
                            {
                                   vessel.vessel_ID = Convert.ToInt32(dtp.Rows[0]["vessel_ID"]);
                                   vessel.imo = Convert.ToInt32(dtp.Rows[0]["imo"]);
                                   vessel.VesselName = dtp.Rows[0]["VesselName"].ToString();
                                   vessel.Email = dtp.Rows[0]["Email"].ToString();
                                   vessel.Flag = dtp.Rows[0]["Flag"].ToString();
                                   vessel.website = dtp.Rows[0]["website"].ToString();

                            }
                     }
                     return vessel;
              }
              public void SaveLicence(ProductInfoClass obj)
              {
                     try
                     {
                            erinfo = 1;
                            RefreshMessageKey(obj);
                            if (CheckErrorMessage.CheckErrorMessages)
                            {
                                   sc.CreateLog("Renew Licence", "Add Key", DateTime.Now);

                                   ProductKeyClass KeyClass = new ProductKeyClass();

                                   var keys = sc.GetLicencekeys().ToList();
                                   var vessel = GetVesselInfo(); // sc.Vessels.FirstOrDefault();

                                   if (vessel != null)
                                   {
                                          KeyClass.VesselName = vessel.VesselName;
                                          KeyClass.IMO = vessel.imo;
                                          KeyClass.Flag = vessel.Flag;
                                   }

                                   KeyClass.Key1 = keys.Where(x => x.keyno == "key1").FirstOrDefault().keycode;
                                   KeyClass.Key2 = keys.Where(x => x.keyno == "key2").FirstOrDefault().keycode;
                                   KeyClass.Key3 = keys.Where(x => x.keyno == "key3").FirstOrDefault().keycode;
                                   KeyClass.Key4 = keys.Where(x => x.keyno == "key4").FirstOrDefault().keycode;
                                   KeyClass.Key5 = keys.Where(x => x.keyno == "key5").FirstOrDefault().keycode;
                                   KeyClass.Key6 = keys.Where(x => x.keyno == "key6").FirstOrDefault().keycode;
                                   KeyClass.Key7 = keys.Where(x => x.keyno == "key7").FirstOrDefault().keycode;
                                   KeyClass.Key8 = keys.Where(x => x.keyno == "key8").FirstOrDefault().keycode;
                                   KeyClass.Key9 = keys.Where(x => x.keyno == "key9").FirstOrDefault().keycode;
                                   KeyClass.Key10 = keys.Where(x => x.keyno == "key10").FirstOrDefault().keycode;
                                   //KeyClass.Key11 = keys.Where(x => x.keyno == "key11").FirstOrDefault().keycode.ToString();
                                   //KeyClass.Key12 = keys.Where(x => x.keyno == "key12").FirstOrDefault().keycode.ToString();
                                   //KeyClass.Key13 = keys.Where(x => x.keyno == "key13").FirstOrDefault().keycode.ToString();
                                   //KeyClass.Key14 = keys.Where(x => x.keyno == "key14").FirstOrDefault().keycode.ToString();
                                   //KeyClass.Key15 = keys.Where(x => x.keyno == "key15").FirstOrDefault().keycode.ToString();
                                   //KeyClass.Key16 = keys.Where(x => x.keyno == "key16").FirstOrDefault().keycode.ToString();
                                   //KeyClass.Key17 = keys.Where(x => x.keyno == "key17").FirstOrDefault().keycode.ToString();
                                   //KeyClass.Key18 = keys.Where(x => x.keyno == "key18").FirstOrDefault().keycode.ToString();
                                   //KeyClass.Key19 = keys.Where(x => x.keyno == "key19").FirstOrDefault().keycode.ToString();
                                   //KeyClass.Key20 = keys.Where(x => x.keyno == "key20").FirstOrDefault().keycode.ToString();
                                   //KeyClass.Key21 = keys.Where(x => x.keyno == "key21").FirstOrDefault().keycode.ToString();
                                   //KeyClass.Key22 = keys.Where(x => x.keyno == "key22").FirstOrDefault().keycode.ToString();
                                   //KeyClass.Key23 = keys.Where(x => x.keyno == "key23").FirstOrDefault().keycode.ToString();
                                   //KeyClass.Key24 = keys.Where(x => x.keyno == "key24").FirstOrDefault().keycode.ToString();
                                   //KeyClass.Key25 = keys.Where(x => x.keyno == "key25").FirstOrDefault().keycode.ToString();
                                   //KeyClass.Key26 = keys.Where(x => x.keyno == "key26").FirstOrDefault().keycode.ToString();
                                   //KeyClass.Key27 = keys.Where(x => x.keyno == "key27").FirstOrDefault().keycode.ToString();
                                   //KeyClass.Key28 = keys.Where(x => x.keyno == "key28").FirstOrDefault().keycode.ToString();
                                   //KeyClass.Key29 = keys.Where(x => x.keyno == "key29").FirstOrDefault().keycode.ToString();
                                   //KeyClass.Key30 = keys.Where(x => x.keyno == "key30").FirstOrDefault().keycode.ToString();

                                   //string txtLicenseKey = obj.Text1.Trim().ToUpper() + "-" + obj.Text2.Trim().ToUpper() + "-" + obj.Text3.Trim().ToUpper() + "-" + obj.Text4.Trim().ToUpper() + "-" + obj.Text5.Trim().ToUpper();

                                   string[] InputkeyList = { "A", "B" };
                                   string DCodeKey = sc.Decrypt(obj.TextMain, StaticHelper.Key);
                                   if (DCodeKey != null)
                                   {
                                          InputkeyList = null;
                                          InputkeyList = DCodeKey.Split(',');
                                   }

                                   string txtLicenseKey = InputkeyList[0];
                                   string InputSerialKey = InputkeyList[1];


                                   var txtpath101 = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "RecordDigiMoor.txt");
                                   if (File.Exists(txtpath101))
                                   {
                                          StreamReader srd = new StreamReader(txtpath101);
                                          string RecordData = sc.Decrypt(srd.ReadLine(), StaticHelper.Key);
                                          srd.Close();

                                          string[] RecordArr = RecordData.Split(',');
                                          string nxdt = RecordArr[1];
                                          // string nd = Convert.ToDateTime(RecordArr[1]).AddDays(1).ToString("yyyy-MM-dd");
                                          // CreateKeyMethod(RecordArr[0], nd, RecordArr[2], KeyClass.VesselName);
                                          DateTime ct = Convert.ToDateTime(nxdt);

                                          string dtt = RecordArr[0];
                                          DateTime dtt1 = Convert.ToDateTime(dtt);

                                          int mm = dtt1.Month;//.Substring(0, 1);
                                          int dd = dtt1.Day;//.Substring(2, 2);
                                          int yy = dtt1.Year;//.Substring(5, 4);

                        string dtt5 = RecordArr[1];
                        DateTime dtt55 = Convert.ToDateTime(dtt5);
                        int yy5 = dtt55.Year;

                        if (yy5 == 2021)
                        {
                            ct = ct.AddDays(-1);
                        }
                        //if (yy == 2024)
                        //{
                        //    ct = ct.AddDays(-1);
                        //}


                        KeyClass.Dt1 = KeyClass.Yr1 = new DateTime(yy, mm, dd);
                                          KeyClass.Year1 = KeyClass.Yr1.Year;



                                          KeyClass.Yr2 = KeyClass.Yr1.AddYears(1);
                                          KeyClass.Year2 = KeyClass.Dt1.Year;
                                          if (KeyClass.Yr2.Year % 4 == 0)
                                          {
                                                 KeyClass.Dt2 = KeyClass.Dt1.AddDays(366 - 1);
                                          }
                                          else
                                          {
                                                 KeyClass.Dt2 = KeyClass.Dt1.AddDays(365);
                                          }

                                          KeyClass.Yr3 = KeyClass.Yr2.AddYears(1);
                                          KeyClass.Year3 = KeyClass.Dt2.Year;
                                          if (KeyClass.Yr3.Year % 4 == 0)
                                          {
                                                 KeyClass.Dt3 = KeyClass.Dt2.AddDays(366 - 1);
                                          }
                                          else
                                          {
                                                 KeyClass.Dt3 = KeyClass.Dt2.AddDays(365);
                                          }

                                          KeyClass.Yr4 = KeyClass.Yr3.AddYears(1);
                                          KeyClass.Year4 = KeyClass.Dt3.Year;
                                          if (KeyClass.Yr4.Year % 4 == 0)
                                          {
                                                 KeyClass.Dt4 = KeyClass.Dt3.AddDays(366 - 1);
                                          }
                                          else
                                          {
                                                 KeyClass.Dt4 = KeyClass.Dt3.AddDays(365);
                                          }

                                          KeyClass.Yr5 = KeyClass.Yr4.AddYears(1);
                                          KeyClass.Year5 = KeyClass.Dt4.Year;
                                          if (KeyClass.Yr5.Year % 4 == 0)
                                          {
                                                 KeyClass.Dt5 = KeyClass.Dt4.AddDays(366 - 1);
                                          }
                                          else
                                          {
                                                 KeyClass.Dt5 = KeyClass.Dt4.AddDays(365);
                                          }

                                          KeyClass.Yr6 = KeyClass.Yr5.AddYears(1);
                                          KeyClass.Year6 = KeyClass.Dt5.Year;
                                          if (KeyClass.Yr6.Year % 4 == 0)
                                          {
                                                 KeyClass.Dt6 = KeyClass.Dt5.AddDays(366 - 1);
                                          }
                                          else
                                          {
                                                 KeyClass.Dt6 = KeyClass.Dt5.AddDays(365);
                                          }

                                          KeyClass.Yr7 = KeyClass.Yr6.AddYears(1);
                                          KeyClass.Year7 = KeyClass.Dt6.Year;
                                          if (KeyClass.Yr7.Year % 4 == 0)
                                          {
                                                 KeyClass.Dt7 = KeyClass.Dt6.AddDays(366 - 1);
                                          }
                                          else
                                          {
                                                 KeyClass.Dt7 = KeyClass.Dt6.AddDays(365 - 1);
                                          }

                                          KeyClass.Yr8 = KeyClass.Yr7.AddYears(1);
                                          KeyClass.Year8 = KeyClass.Dt7.Year;
                                          if (KeyClass.Yr8.Year % 4 == 0)
                                          {
                                                 KeyClass.Dt8 = KeyClass.Dt7.AddDays(366 - 1);
                                          }
                                          else
                                          {
                                                 KeyClass.Dt8 = KeyClass.Dt7.AddDays(365);
                                          }

                                          KeyClass.Yr9 = KeyClass.Yr8.AddYears(1);
                                          KeyClass.Year9 = KeyClass.Dt8.Year;
                                          if (KeyClass.Yr9.Year % 4 == 0)
                                          {
                                                 KeyClass.Dt9 = KeyClass.Dt8.AddDays(366 - 1);
                                          }
                                          else
                                          {
                                                 KeyClass.Dt9 = KeyClass.Dt8.AddDays(365);
                                          }

                                          KeyClass.Yr10 = KeyClass.Yr9.AddYears(1);
                                          KeyClass.Year10 = KeyClass.Dt9.Year;
                                          if (KeyClass.Yr10.Year % 4 == 0)
                                          {
                                                 KeyClass.Dt10 = KeyClass.Dt9.AddDays(366 - 1);
                                          }
                                          else
                                          {
                                                 KeyClass.Dt10 = KeyClass.Dt9.AddDays(365);
                                          }

                                          KeyClass.Yr11 = KeyClass.Yr10.AddYears(1);
                                          KeyClass.Year11 = KeyClass.Dt10.Year;
                                          if (KeyClass.Yr11.Year % 4 == 0)
                                          {
                                                 KeyClass.Dt11 = KeyClass.Dt10.AddDays(366 - 1);
                                          }
                                          else
                                          {
                                                 KeyClass.Dt11 = KeyClass.Dt10.AddDays(365);
                                          }

                                          KeyClass.Yr12 = KeyClass.Yr11.AddYears(1);
                                          KeyClass.Year12 = KeyClass.Dt11.Year;
                                          if (KeyClass.Yr12.Year % 4 == 0)
                                          {
                                                 KeyClass.Dt12 = KeyClass.Dt11.AddDays(366 - 1);
                                          }
                                          else
                                          {
                                                 KeyClass.Dt12 = KeyClass.Dt11.AddDays(365);
                                          }

                                          KeyClass.Yr13 = KeyClass.Yr12.AddYears(1);
                                          KeyClass.Year13 = KeyClass.Dt12.Year;
                                          if (KeyClass.Yr13.Year % 4 == 0)
                                          {
                                                 KeyClass.Dt13 = KeyClass.Dt12.AddDays(366 - 1);
                                          }
                                          else
                                          {
                                                 KeyClass.Dt13 = KeyClass.Dt12.AddDays(365);
                                          }

                                          KeyClass.Yr14 = KeyClass.Yr13.AddYears(1);
                                          KeyClass.Year14 = KeyClass.Dt13.Year;
                                          if (KeyClass.Yr14.Year % 4 == 0)
                                          {
                                                 KeyClass.Dt14 = KeyClass.Dt13.AddDays(366 - 1);
                                          }
                                          else
                                          {
                                                 KeyClass.Dt14 = KeyClass.Dt13.AddDays(365);
                                          }

                                          KeyClass.Yr15 = KeyClass.Yr14.AddYears(1);
                                          KeyClass.Year15 = KeyClass.Dt14.Year;
                                          if (KeyClass.Yr15.Year % 4 == 0)
                                          {
                                                 KeyClass.Dt15 = KeyClass.Dt14.AddDays(366 - 1);
                                          }
                                          else
                                          {
                                                 KeyClass.Dt15 = KeyClass.Dt14.AddDays(365);
                                          }

                                          KeyClass.Yr16 = KeyClass.Yr15.AddYears(1);
                                          KeyClass.Year16 = KeyClass.Dt15.Year;
                                          if (KeyClass.Yr16.Year % 4 == 0)
                                          {
                                                 KeyClass.Dt16 = KeyClass.Dt15.AddDays(366 - 1);
                                          }
                                          else
                                          {
                                                 KeyClass.Dt16 = KeyClass.Dt15.AddDays(365);
                                          }

                                          KeyClass.Yr17 = KeyClass.Yr16.AddYears(1);
                                          KeyClass.Year17 = KeyClass.Dt16.Year;
                                          if (KeyClass.Yr17.Year % 4 == 0)
                                          {
                                                 KeyClass.Dt17 = KeyClass.Dt16.AddDays(366 - 1);
                                          }
                                          else
                                          {
                                                 KeyClass.Dt17 = KeyClass.Dt16.AddDays(365);
                                          }

                                          KeyClass.Yr18 = KeyClass.Yr17.AddYears(1);
                                          KeyClass.Year18 = KeyClass.Dt17.Year;
                                          if (KeyClass.Yr18.Year % 4 == 0)
                                          {
                                                 KeyClass.Dt18 = KeyClass.Dt17.AddDays(366 - 1);
                                          }
                                          else
                                          {
                                                 KeyClass.Dt18 = KeyClass.Dt17.AddDays(365);
                                          }

                                          KeyClass.Yr19 = KeyClass.Yr18.AddYears(1);
                                          KeyClass.Year19 = KeyClass.Dt18.Year;
                                          if (KeyClass.Yr19.Year % 4 == 0)
                                          {
                                                 KeyClass.Dt19 = KeyClass.Dt18.AddDays(366 - 1);
                                          }
                                          else
                                          {
                                                 KeyClass.Dt19 = KeyClass.Dt18.AddDays(365);
                                          }

                                          KeyClass.Yr20 = KeyClass.Yr19.AddYears(1);
                                          KeyClass.Year20 = KeyClass.Dt19.Year;
                                          if (KeyClass.Yr20.Year % 4 == 0)
                                          {
                                                 KeyClass.Dt20 = KeyClass.Dt19.AddDays(366 - 1);
                                          }
                                          else
                                          {
                                                 KeyClass.Dt20 = KeyClass.Dt19.AddDays(365);
                                          }

                                          KeyClass.Yr21 = KeyClass.Yr20.AddYears(1);
                                          KeyClass.Year21 = KeyClass.Dt20.Year;
                                          if (KeyClass.Yr21.Year % 4 == 0)
                                          {
                                                 KeyClass.Dt21 = KeyClass.Dt20.AddDays(366 - 1);
                                          }
                                          else
                                          {
                                                 KeyClass.Dt21 = KeyClass.Dt20.AddDays(365);
                                          }

                                          KeyClass.Yr22 = KeyClass.Yr21.AddYears(1);
                                          KeyClass.Year22 = KeyClass.Dt21.Year;
                                          if (KeyClass.Yr22.Year % 4 == 0)
                                          {
                                                 KeyClass.Dt22 = KeyClass.Dt21.AddDays(366 - 1);
                                          }
                                          else
                                          {
                                                 KeyClass.Dt22 = KeyClass.Dt21.AddDays(365);
                                          }

                                          KeyClass.Yr23 = KeyClass.Yr22.AddYears(1);
                                          KeyClass.Year23 = KeyClass.Dt22.Year;
                                          if (KeyClass.Yr23.Year % 4 == 0)
                                          {
                                                 KeyClass.Dt23 = KeyClass.Dt22.AddDays(366 - 1);
                                          }
                                          else
                                          {
                                                 KeyClass.Dt23 = KeyClass.Dt22.AddDays(365);
                                          }

                                          KeyClass.Yr24 = KeyClass.Yr23.AddYears(1);
                                          KeyClass.Year24 = KeyClass.Dt23.Year;
                                          if (KeyClass.Yr24.Year % 4 == 0)
                                          {
                                                 KeyClass.Dt24 = KeyClass.Dt23.AddDays(366 - 1);
                                          }
                                          else
                                          {
                                                 KeyClass.Dt24 = KeyClass.Dt23.AddDays(365);
                                          }

                                          KeyClass.Yr25 = KeyClass.Yr24.AddYears(1);
                                          KeyClass.Year25 = KeyClass.Dt24.Year;
                                          if (KeyClass.Yr25.Year % 4 == 0)
                                          {
                                                 KeyClass.Dt25 = KeyClass.Dt24.AddDays(366 - 1);
                                          }
                                          else
                                          {
                                                 KeyClass.Dt25 = KeyClass.Dt24.AddDays(365);
                                          }

                                          KeyClass.Yr26 = KeyClass.Yr25.AddYears(1);
                                          KeyClass.Year26 = KeyClass.Dt25.Year;
                                          if (KeyClass.Yr26.Year % 4 == 0)
                                          {
                                                 KeyClass.Dt26 = KeyClass.Dt25.AddDays(366 - 1);
                                          }
                                          else
                                          {
                                                 KeyClass.Dt26 = KeyClass.Dt25.AddDays(365);
                                          }

                                          KeyClass.Yr27 = KeyClass.Yr26.AddYears(1);
                                          KeyClass.Year27 = KeyClass.Dt26.Year;
                                          if (KeyClass.Yr27.Year % 4 == 0)
                                          {
                                                 KeyClass.Dt27 = KeyClass.Dt26.AddDays(366 - 1);
                                          }
                                          else
                                          {
                                                 KeyClass.Dt27 = KeyClass.Dt26.AddDays(365);
                                          }

                                          KeyClass.Yr28 = KeyClass.Yr27.AddYears(1);
                                          KeyClass.Year28 = KeyClass.Dt27.Year;
                                          if (KeyClass.Yr28.Year % 4 == 0)
                                          {
                                                 KeyClass.Dt28 = KeyClass.Dt27.AddDays(366 - 1);
                                          }
                                          else
                                          {
                                                 KeyClass.Dt28 = KeyClass.Dt27.AddDays(365);
                                          }

                                          KeyClass.Yr29 = KeyClass.Yr28.AddYears(1);
                                          KeyClass.Year29 = KeyClass.Dt28.Year;
                                          if (KeyClass.Yr29.Year % 4 == 0)
                                          {
                                                 KeyClass.Dt29 = KeyClass.Dt28.AddDays(366 - 1);
                                          }
                                          else
                                          {
                                                 KeyClass.Dt29 = KeyClass.Dt28.AddDays(365);
                                          }

                                          KeyClass.Yr30 = KeyClass.Yr29.AddYears(1);
                                          KeyClass.Year30 = KeyClass.Dt29.Year;
                                          if (KeyClass.Yr30.Year % 4 == 0)
                                          {
                                                 KeyClass.Dt30 = KeyClass.Dt29.AddDays(366 - 1);
                                          }
                                          else
                                          {
                                                 KeyClass.Dt30 = KeyClass.Dt29.AddDays(365);
                                          }

                                          KeyClass.Yr31 = KeyClass.Yr30.AddYears(1);
                                          KeyClass.Year31 = KeyClass.Dt30.Year;
                                          if (KeyClass.Yr31.Year % 4 == 0)
                                          {
                                                 KeyClass.Dt31 = KeyClass.Dt30.AddDays(366 - 1);
                                          }
                                          else
                                          {
                                                 KeyClass.Dt31 = KeyClass.Dt30.AddDays(365);
                                          }


                                          ////=========================

                                          DateTime expirydate;
                                          if (KeyClass.Dt1.Date == ct.Date)
                                          {
                                                 if (txtLicenseKey == KeyClass.Key1)
                                                 {

                                                        expirydate = KeyClass.Dt2;
                                                       CreateKeyMethod(RecordArr[0], expirydate.ToString("yyyy-MM-dd"), txtLicenseKey, KeyClass.VesselName);
                                                 }
                                                 else
                                                 {
                                                        productMessage = "Invalid License Key";
                                                        RaisePropertyChanged("ProductMessage");

                                                 }

                                          }

                                          else if (KeyClass.Dt2.Date == ct.Date)
                                          {
                                                 if (txtLicenseKey == KeyClass.Key2)
                                                 {

                                                        expirydate = KeyClass.Dt3;
                                                        CreateKeyMethod(RecordArr[0], expirydate.ToString("yyyy-MM-dd"), txtLicenseKey, KeyClass.VesselName);
                                                 }
                                                 else
                                                 {
                                                        productMessage = "Invalid License Key";
                                                        RaisePropertyChanged("ProductMessage");
                                                 }

                                          }

                                          else if (KeyClass.Dt3.Date == ct.Date)
                                          {
                                                 if (txtLicenseKey == KeyClass.Key3)
                                                 {

                                                        expirydate = KeyClass.Dt4;
                                                        CreateKeyMethod(RecordArr[0], expirydate.ToString("yyyy-MM-dd"), txtLicenseKey, KeyClass.VesselName);
                                                 }
                                                 else
                                                 {
                                                        productMessage = "Invalid License Key";
                                                        RaisePropertyChanged("ProductMessage");
                                                 }

                                          }

                                          else if (KeyClass.Dt4.Date == ct.Date)
                                          {
                                                 if (txtLicenseKey == KeyClass.Key4)
                                                 {

                                                        expirydate = KeyClass.Dt5;
                                                        CreateKeyMethod(RecordArr[0], expirydate.ToString("yyyy-MM-dd"), txtLicenseKey, KeyClass.VesselName);
                                                 }
                                                 else
                                                 {
                                                        productMessage = "Invalid License Key";
                                                        RaisePropertyChanged("ProductMessage");
                                                 }

                                          }

                                          else if (KeyClass.Dt5.Date == ct.Date)
                                          {
                                                 if (txtLicenseKey == KeyClass.Key5)
                                                 {

                                                        expirydate = KeyClass.Dt6;
                                                        CreateKeyMethod(RecordArr[0], expirydate.ToString("yyyy-MM-dd"), txtLicenseKey, KeyClass.VesselName);
                                                 }
                                                 else
                                                 {
                                                        productMessage = "Invalid License Key";
                                                        RaisePropertyChanged("ProductMessage");
                                                 }

                                          }

                                          else if (KeyClass.Dt6.Date == ct.Date)
                                          {
                                                 if (txtLicenseKey == KeyClass.Key6)
                                                 {

                                                        expirydate = KeyClass.Dt7;
                                                        CreateKeyMethod(RecordArr[0], expirydate.ToString("yyyy-MM-dd"), txtLicenseKey, KeyClass.VesselName);
                                                 }
                                                 else
                                                 {
                                                        productMessage = "Invalid License Key";
                                                        RaisePropertyChanged("ProductMessage");
                                                 }

                                          }

                                          else if (KeyClass.Dt7.Date == ct.Date)
                                          {
                                                 if (txtLicenseKey == KeyClass.Key7)
                                                 {

                                                        expirydate = KeyClass.Dt8;
                                                        CreateKeyMethod(RecordArr[0], expirydate.ToString("yyyy-MM-dd"), txtLicenseKey, KeyClass.VesselName);
                                                 }
                                                 else
                                                 {
                                                        productMessage = "Invalid License Key";
                                                        RaisePropertyChanged("ProductMessage");
                                                 }

                                          }

                                          else if (KeyClass.Dt8.Date == ct.Date)
                                          {
                                                 if (txtLicenseKey == KeyClass.Key8)
                                                 {

                                                        expirydate = KeyClass.Dt9;
                                                        CreateKeyMethod(RecordArr[0], expirydate.ToString("yyyy-MM-dd"), txtLicenseKey, KeyClass.VesselName);
                                                 }
                                                 else
                                                 {
                                                        productMessage = "Invalid License Key";
                                                        RaisePropertyChanged("ProductMessage");
                                                 }

                                          }

                                          else if (KeyClass.Dt9.Date == ct.Date)
                                          {
                                                 if (txtLicenseKey == KeyClass.Key9)
                                                 {

                                                        expirydate = KeyClass.Dt10;
                                                        CreateKeyMethod(RecordArr[0], expirydate.ToString("yyyy-MM-dd"), txtLicenseKey, KeyClass.VesselName);
                                                 }
                                                 else
                                                 {
                                                        productMessage = "Invalid License Key";
                                                        RaisePropertyChanged("ProductMessage");
                                                 }

                                          }

                                          else if (KeyClass.Dt10.Date == ct.Date)
                                          {
                                                 if (txtLicenseKey == KeyClass.Key10)
                                                 {

                                                        expirydate = KeyClass.Dt11;
                                                        CreateKeyMethod(RecordArr[0], expirydate.ToString("yyyy-MM-dd"), txtLicenseKey, KeyClass.VesselName);
                                                 }
                                                 else
                                                 {
                                                        productMessage = "Invalid License Key";
                                                        RaisePropertyChanged("ProductMessage");
                                                 }

                                          }

                                          else if (KeyClass.Dt11.Date == ct.Date)
                                          {
                                                 if (txtLicenseKey == KeyClass.Key11)
                                                 {

                                                        expirydate = KeyClass.Dt12;
                                                        CreateKeyMethod(RecordArr[0], expirydate.ToString("yyyy-MM-dd"), txtLicenseKey, KeyClass.VesselName);
                                                 }
                                                 else
                                                 {
                                                        productMessage = "Invalid License Key";
                                                        RaisePropertyChanged("ProductMessage");
                                                 }

                                          }

                                          else if (KeyClass.Dt12.Date == ct.Date)
                                          {
                                                 if (txtLicenseKey == KeyClass.Key12)
                                                 {

                                                        expirydate = KeyClass.Dt13;
                                                        CreateKeyMethod(RecordArr[0], expirydate.ToString("yyyy-MM-dd"), txtLicenseKey, KeyClass.VesselName);
                                                 }
                                                 else
                                                 {
                                                        productMessage = "Invalid License Key";
                                                        RaisePropertyChanged("ProductMessage");
                                                 }

                                          }

                                          else if (KeyClass.Dt13.Date == ct.Date)
                                          {
                                                 if (txtLicenseKey == KeyClass.Key13)
                                                 {

                                                        expirydate = KeyClass.Dt14;
                                                        CreateKeyMethod(RecordArr[0], expirydate.ToString("yyyy-MM-dd"), txtLicenseKey, KeyClass.VesselName);
                                                 }
                                                 else
                                                 {
                                                        productMessage = "Invalid License Key";
                                                        RaisePropertyChanged("ProductMessage");
                                                 }

                                          }

                                          else if (KeyClass.Dt14.Date == ct.Date)
                                          {
                                                 if (txtLicenseKey == KeyClass.Key14)
                                                 {

                                                        expirydate = KeyClass.Dt15;
                                                        CreateKeyMethod(RecordArr[0], expirydate.ToString("yyyy-MM-dd"), txtLicenseKey, KeyClass.VesselName);
                                                 }
                                                 else
                                                 {
                                                        productMessage = "Invalid License Key";
                                                        RaisePropertyChanged("ProductMessage");
                                                 }

                                          }

                                          else if (KeyClass.Dt15.Date == ct.Date)
                                          {
                                                 if (txtLicenseKey == KeyClass.Key15)
                                                 {

                                                        expirydate = KeyClass.Dt16;
                                                        CreateKeyMethod(RecordArr[0], expirydate.ToString("yyyy-MM-dd"), txtLicenseKey, KeyClass.VesselName);
                                                 }
                                                 else
                                                 {
                                                        productMessage = "Invalid License Key";
                                                        RaisePropertyChanged("ProductMessage");
                                                 }

                                          }

                                          else if (KeyClass.Dt16.Date == ct.Date)
                                          {
                                                 if (txtLicenseKey == KeyClass.Key16)
                                                 {

                                                        expirydate = KeyClass.Dt17;
                                                        CreateKeyMethod(RecordArr[0], expirydate.ToString("yyyy-MM-dd"), txtLicenseKey, KeyClass.VesselName);
                                                 }
                                                 else
                                                 {
                                                        productMessage = "Invalid License Key";
                                                        RaisePropertyChanged("ProductMessage");
                                                 }

                                          }

                                          else if (KeyClass.Dt17.Date == ct.Date)
                                          {
                                                 if (txtLicenseKey == KeyClass.Key17)
                                                 {

                                                        expirydate = KeyClass.Dt18;
                                                        CreateKeyMethod(RecordArr[0], expirydate.ToString("yyyy-MM-dd"), txtLicenseKey, KeyClass.VesselName);
                                                 }
                                                 else
                                                 {
                                                        productMessage = "Invalid License Key";
                                                        RaisePropertyChanged("ProductMessage");
                                                 }

                                          }

                                          else if (KeyClass.Dt18.Date == ct.Date)
                                          {
                                                 if (txtLicenseKey == KeyClass.Key18)
                                                 {

                                                        expirydate = KeyClass.Dt19;
                                                        CreateKeyMethod(RecordArr[0], expirydate.ToString("yyyy-MM-dd"), txtLicenseKey, KeyClass.VesselName);
                                                 }
                                                 else
                                                 {
                                                        productMessage = "Invalid License Key";
                                                        RaisePropertyChanged("ProductMessage");
                                                 }

                                          }
                                          else if (KeyClass.Dt19.Date == ct.Date)
                                          {
                                                 if (txtLicenseKey == KeyClass.Key19)
                                                 {

                                                        expirydate = KeyClass.Dt20;
                                                        CreateKeyMethod(RecordArr[0], expirydate.ToString("yyyy-MM-dd"), txtLicenseKey, KeyClass.VesselName);
                                                 }
                                                 else
                                                 {
                                                        productMessage = "Invalid License Key";
                                                        RaisePropertyChanged("ProductMessage");
                                                 }

                                          }

                                          else if (KeyClass.Dt20.Date == ct.Date)
                                          {
                                                 if (txtLicenseKey == KeyClass.Key20)
                                                 {

                                                        expirydate = KeyClass.Dt21;
                                                        CreateKeyMethod(RecordArr[0], expirydate.ToString("yyyy-MM-dd"), txtLicenseKey, KeyClass.VesselName);
                                                 }
                                                 else
                                                 {
                                                        productMessage = "Invalid License Key";
                                                        RaisePropertyChanged("ProductMessage");
                                                 }

                                          }
                                          else if (KeyClass.Dt21.Date == ct.Date)
                                          {
                                                 if (txtLicenseKey == KeyClass.Key21)
                                                 {

                                                        expirydate = KeyClass.Dt22;
                                                        CreateKeyMethod(RecordArr[0], expirydate.ToString("yyyy-MM-dd"), txtLicenseKey, KeyClass.VesselName);
                                                 }
                                                 else
                                                 {
                                                        productMessage = "Invalid License Key";
                                                        RaisePropertyChanged("ProductMessage");
                                                 }

                                          }
                                          else if (KeyClass.Dt22.Date == ct.Date)
                                          {
                                                 if (txtLicenseKey == KeyClass.Key22)
                                                 {

                                                        expirydate = KeyClass.Dt23;
                                                        CreateKeyMethod(RecordArr[0], expirydate.ToString("yyyy-MM-dd"), txtLicenseKey, KeyClass.VesselName);
                                                 }
                                                 else
                                                 {
                                                        productMessage = "Invalid License Key";
                                                        RaisePropertyChanged("ProductMessage");
                                                 }

                                          }
                                          else if (KeyClass.Dt23.Date == ct.Date)
                                          {
                                                 if (txtLicenseKey == KeyClass.Key23)
                                                 {

                                                        expirydate = KeyClass.Dt24;
                                                        CreateKeyMethod(RecordArr[0], expirydate.ToString("yyyy-MM-dd"), txtLicenseKey, KeyClass.VesselName);
                                                 }
                                                 else
                                                 {
                                                        productMessage = "Invalid License Key";
                                                        RaisePropertyChanged("ProductMessage");
                                                 }

                                          }
                                          else if (KeyClass.Dt24.Date == ct.Date)
                                          {
                                                 if (txtLicenseKey == KeyClass.Key24)
                                                 {

                                                        expirydate = KeyClass.Dt25;
                                                        CreateKeyMethod(RecordArr[0], expirydate.ToString("yyyy-MM-dd"), txtLicenseKey, KeyClass.VesselName);
                                                 }
                                                 else
                                                 {
                                                        productMessage = "Invalid License Key";
                                                        RaisePropertyChanged("ProductMessage");
                                                 }

                                          }
                                          else if (KeyClass.Dt25.Date == ct.Date)
                                          {
                                                 if (txtLicenseKey == KeyClass.Key25)
                                                 {

                                                        expirydate = KeyClass.Dt26;
                                                        CreateKeyMethod(RecordArr[0], expirydate.ToString("yyyy-MM-dd"), txtLicenseKey, KeyClass.VesselName);
                                                 }
                                                 else
                                                 {
                                                        productMessage = "Invalid License Key";
                                                        RaisePropertyChanged("ProductMessage");
                                                 }

                                          }
                                          else if (KeyClass.Dt26.Date == ct.Date)
                                          {
                                                 if (txtLicenseKey == KeyClass.Key26)
                                                 {

                                                        expirydate = KeyClass.Dt27;
                                                        CreateKeyMethod(RecordArr[0], expirydate.ToString("yyyy-MM-dd"), txtLicenseKey, KeyClass.VesselName);
                                                 }
                                                 else
                                                 {
                                                        productMessage = "Invalid License Key";
                                                        RaisePropertyChanged("ProductMessage");
                                                 }

                                          }
                                          else if (KeyClass.Dt27.Date == ct.Date)
                                          {
                                                 if (txtLicenseKey == KeyClass.Key27)
                                                 {

                                                        expirydate = KeyClass.Dt28;
                                                        CreateKeyMethod(RecordArr[0], expirydate.ToString("yyyy-MM-dd"), txtLicenseKey, KeyClass.VesselName);
                                                 }
                                                 else
                                                 {
                                                        productMessage = "Invalid License Key";
                                                        RaisePropertyChanged("ProductMessage");
                                                 }

                                          }
                                          else if (KeyClass.Dt28.Date == ct.Date)
                                          {
                                                 if (txtLicenseKey == KeyClass.Key28)
                                                 {

                                                        expirydate = KeyClass.Dt29;
                                                        CreateKeyMethod(RecordArr[0], expirydate.ToString("yyyy-MM-dd"), txtLicenseKey, KeyClass.VesselName);
                                                 }
                                                 else
                                                 {
                                                        productMessage = "Invalid License Key";
                                                        RaisePropertyChanged("ProductMessage");
                                                 }

                                          }
                                          else if (KeyClass.Dt29.Date == ct.Date)
                                          {
                                                 if (txtLicenseKey == KeyClass.Key29)
                                                 {

                                                        expirydate = KeyClass.Dt30;
                                                        CreateKeyMethod(RecordArr[0], expirydate.ToString("yyyy-MM-dd"), txtLicenseKey, KeyClass.VesselName);
                                                 }
                                                 else
                                                 {
                                                        productMessage = "Invalid License Key";
                                                        RaisePropertyChanged("ProductMessage");
                                                 }

                                          }
                                          else if (KeyClass.Dt30.Date == ct.Date)
                                          {
                                                 if (txtLicenseKey == KeyClass.Key30)
                                                 {

                                                        expirydate = KeyClass.Dt31;
                                                        CreateKeyMethod(RecordArr[0], expirydate.ToString("yyyy-MM-dd"), txtLicenseKey, KeyClass.VesselName);
                                                 }
                                                 else
                                                 {
                                                        productMessage = "Invalid License Key";
                                                        RaisePropertyChanged("ProductMessage");
                                                 }

                                          }
                                          else
                                          {
                                                 productMessage = "Invalid License Key";
                                                 RaisePropertyChanged("ProductMessage");

                                          }


                                   }


                            }
                     }
                     catch (Exception ex)
                     {
                            sc.ErrorLog(ex);
                     }
              }
              public void CancelLicence()
              {
                     ProductValidate = new ProductInfoClass();
                     ChildWindowManager.Instance.CloseChildWindow();


              }

              private void CreateKeyMethod(string FirstDate, string NextDate, string LicenseKey, string vesselName)
              {
                     try
                     {
                            string exp = NextDate;
                            var txtpath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "RecordDigiMoor.txt");
                            if (File.Exists(txtpath) && new FileInfo(txtpath).Length > 0)
                            {

                                   StreamReader srd = new StreamReader(txtpath);

                                   string LastYearRecordWork49Data = srd.ReadLine();
                                   string OldRecordData = sc.Decrypt(LastYearRecordWork49Data, StaticHelper.Key);
                                   srd.Close();

                                   string[] lastRecord = OldRecordData.Split(',');
                                   string kpl = lastRecord[2].ToString();
                                   string shipnm = lastRecord[3].ToString();

                                   if (shipnm.ToLower() == vesselName.ToLower())
                                   {
                                          StreamWriter writer = new StreamWriter(txtpath);
                                          //Encrypt method has first parameter as data to be encrypted and second as key for encryption

                                          writer.Write(sc.Encrypt(FirstDate + "," + NextDate + "," + LicenseKey + "," + vesselName, StaticHelper.Key));
                                          writer.Close();

                                          string Recordwork49file = sc.Encrypt(FirstDate + "," + NextDate + "," + LicenseKey + "," + vesselName, StaticHelper.Key);

                                          var data =  sc.AdminLogins.FirstOrDefault();
                                          if (data != null)
                                          {
                                                 data.productinfo = Recordwork49file;
                                                 sc.Entry(data).State = EntityState.Modified;
                                                 sc.SaveChanges();
                                                                                                 
                                                 MessageBox.Show("Your License is validated. Thank You!", "DigiMoor-X7 Login", MessageBoxButton.OK, MessageBoxImage.Information);
                                                 LicenceViewModel._CommonExpiry.ExpiryMessages = "Expiring On  " + Convert.ToDateTime(NextDate).ToString("dd-MMM-yyyy");
                                                 CancelLicence();
                                          }
                                   }
                                   else
                                   {

                                          MessageBox.Show("It is not Valid Ship, Your License is terminated!!!", "DigiMoor-X7 Login", MessageBoxButton.OK, MessageBoxImage.Error);
                                   }

                            }
                            else
                            {
                                   MessageBox.Show("Error - 404, Registry File is missing ! Please contact support team", "DigiMoor-X7 Login", MessageBoxButton.OK, MessageBoxImage.Error);

                            }
                     }
                     catch (Exception ex)
                     {
                            sc.ErrorLog(ex);
                     }
              }


              new public event PropertyChangedEventHandler PropertyChanged;
              protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
              {
                     PropertyChanged?.Invoke(this, e);
              }
       }
}
