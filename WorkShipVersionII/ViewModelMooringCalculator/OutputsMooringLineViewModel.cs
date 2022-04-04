using GalaSoft.MvvmLight;
using DataBuildingLayer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Command;
using System.Windows.Input;
using System.Data.SqlClient;
using System.Data;

namespace WorkShipVersionII.ViewModelMooringCalculator
{
       public class OutputsMooringLineViewModel : ViewModelBase
       {
              private ShipmentContaxt sc;
              private IRepository<MooringLines> MooringLiness;
              private IRepository<VesselP> VesselPs;
              public ICommand HelpCommand { get; private set; }
              public OutputsMooringLineViewModel()
              {
                     sc = new ShipmentContaxt();
                     MooringLiness = new Repository<MooringLines>();
                     VesselPs = new Repository<VesselP>();

                     HelpCommand = new RelayCommand(() => StaticHelper.HelpMethod(StaticHelper.HelpFor));
                     loadMoorings.Clear();
                     sc.ObservableCollectionList(loadMoorings, GetLoadMooring());
                     GetFinalMooringLines();
                     RaisePropertyChanged("LoadMoorings");
              }


              private decimal fYkN;
              public decimal FYkN
              {
                     get
                     {
                            return fYkN;
                     }
                     set
                     {
                            fYkN = value;
                            RaisePropertyChanged("FYkN");
                     }
              }

              private decimal mrkN_m;
              public decimal MrkN_m
              {
                     get
                     {
                            return mrkN_m;
                     }
                     set
                     {
                            mrkN_m = value;
                            RaisePropertyChanged("MrkN_m");
                     }
              }

              private decimal xcg_m;
              public decimal Xcg_m
              {
                     get
                     {
                            return xcg_m;
                     }
                     set
                     {
                            xcg_m = value;
                            RaisePropertyChanged("Xcg_m");
                     }
              }

              private decimal aKN;
              public decimal AKN
              {
                     get
                     {
                            return aKN;
                     }
                     set
                     {
                            aKN = value;
                            RaisePropertyChanged("AKN");
                     }
              }

              private decimal bKN;
              public decimal BKN
              {
                     get
                     {
                            return bKN;
                     }
                     set
                     {
                            bKN = value;
                            RaisePropertyChanged("BKN");
                     }
              }

              private decimal cKN;
              public decimal CKN
              {
                     get
                     {
                            return cKN;
                     }
                     set
                     {
                            cKN = value;
                            RaisePropertyChanged("CKN");
                     }
              }

              private decimal sYM;
              public decimal SYM
              {
                     get
                     {
                            return sYM;
                     }
                     set
                     {
                            sYM = value;
                            RaisePropertyChanged("SYM");
                     }
              }

              private decimal yradian;
              public decimal Yradian
              {
                     get
                     {
                            return yradian;
                     }
                     set
                     {
                            yradian = value;
                            RaisePropertyChanged("Yradian");
                     }
              }


              public List<MooringLines> GetInputMooringLines()
              {
                     List<MooringLines> inputs = new List<MooringLines>();
                     DateTime dd = DateTime.Now.Date;

                     string qry = "MooringLineInputCalulator";
                     SqlDataAdapter sda = new SqlDataAdapter(qry, sc.con);
                     sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                     sda.SelectCommand.Parameters.AddWithValue("@RopeTail", 0);
                     System.Data.DataTable datatbl = new System.Data.DataTable();
                     sda.Fill(datatbl);

                     var ranklist = new ObservableCollection<MooringLines>();

                     foreach (DataRow row in datatbl.Rows)
                     {
                            inputs.Add(new MooringLines()
                            {
                                   AssignNumber = (row["AssignedNumber"] == DBNull.Value) ? "Not Assigned" : row["AssignedNumber"].ToString(),
                                   Location = (row["Location"] == DBNull.Value) ? "Not Assigned" : row["Location"].ToString(),
                                   Certi_No = (string)row["CertificateNumber"],
                                   UniqueId = (string)row["UniqueId"],
                                   RopeId = (int)row["RopeId"],
                                   Xch = Convert.ToDecimal((row["Xch"] == DBNull.Value) ? 0 : row["Xch"]),
                                   Ych = Convert.ToDecimal((row["Ych"] == DBNull.Value) ? 0 : row["Ych"]),
                                   Zch = Convert.ToDecimal((row["Zch"] == DBNull.Value) ? 0 : row["Zch"]),
                                   Xbl = Convert.ToDecimal((row["Xbl"] == DBNull.Value) ? 0 : row["Xbl"]),
                                   Ybl = Convert.ToDecimal((row["Ybl"] == DBNull.Value) ? 0 : row["Ybl"]),
                                   Zbl = Convert.ToDecimal((row["Zbl"] == DBNull.Value) ? 0 : row["Zbl"]),
                                   l0 = Convert.ToDecimal((row["l0"] == DBNull.Value) ? 0 : row["l0"]),
                                   E = Convert.ToDecimal((row["E"] == DBNull.Value) ? 0 : row["E"]),
                                   n = Convert.ToDecimal((row["n"] == DBNull.Value) ? 0 : row["n"]),
                                   a = Convert.ToDecimal((row["a"] == DBNull.Value) ? 0 : row["a"]),
                                   MBSrope = Convert.ToDecimal((row["MBSrope"] == DBNull.Value) ? 0 : row["MBSrope"]),
                                   //WinchId = (int)row["WinchId"],
                                    Id = Convert.ToInt32((row["Id"] == DBNull.Value) ? 0 : row["Id"]),

                                   //===============================

                                   Xch1 = Convert.ToDecimal((row["Xch"] == DBNull.Value) ? 0 : row["Xch"]),
                                   Ych1 = Convert.ToDecimal((row["Ych"] == DBNull.Value) ? 0 : row["Ych"]),
                                   Zch1 = Convert.ToDecimal((row["Zch"] == DBNull.Value) ? 0 : row["Zch"]),
                                   Xbl1 = Convert.ToDecimal((row["Xbl"] == DBNull.Value) ? 0 : row["Xbl"]),
                                   Ybl1 = Convert.ToDecimal((row["Ybl"] == DBNull.Value) ? 0 : row["Ybl"]),
                                   Zbl1 = Convert.ToDecimal((row["Zbl"] == DBNull.Value) ? 0 : row["Zbl"]),
                                   l01 = Convert.ToDecimal((row["l0"] == DBNull.Value) ? 0 : row["l0"]),
                                   E1 = Convert.ToDecimal((row["E"] == DBNull.Value) ? 0 : row["E"]),
                                   n1 = Convert.ToDecimal((row["n"] == DBNull.Value) ? 0 : row["n"]),
                                   a1 = Convert.ToDecimal((row["a"] == DBNull.Value) ? 0 : row["a"]),
                                   MBSrope1 = Convert.ToDecimal((row["MBSrope"] == DBNull.Value) ? 0 : row["MBSrope"]),

                            });
                     }

                     return inputs;
                     //OnPropertyChanged(new PropertyChangedEventArgs("LoadMooring"));
              }

              private List<TempMooringLine> GetLoadMooring()
              {
                     //if  ('Inputs - Mooring Lines'!C5 == "" ? "" : 'Inputs - Mooring Lines'!C5) { x.IsChecked = true; } else { x.IsChecked = false; } 

                     // var oldsome = MooringLiness.GetList().ToList();
                     var data = GetInputMooringLines(); //MooringLiness.GetList().ToList();
                     var datakk = GetFinalMooringLines();
                     List<TempMooringLine> list = new List<TempMooringLine>();

                     try
                     {
                            foreach (var x in data)
                            {
                                   var C9 = !string.IsNullOrEmpty(x.Xch.ToString()) ? Convert.ToDouble(x.Xch) : 0;
                                   var D9 = !string.IsNullOrEmpty(x.Ych.ToString()) ? Convert.ToDouble(x.Ych) : 0;
                                   var E9 = !string.IsNullOrEmpty(x.Zch.ToString()) ? Convert.ToDecimal(x.Zch) : 0;
                                   var F9 = !string.IsNullOrEmpty(x.Xbl.ToString()) ? Convert.ToDouble(x.Xbl) : 0;
                                   var G9 = !string.IsNullOrEmpty(x.Ybl.ToString()) ? Convert.ToDouble(x.Ybl) : 0;
                                   var H9 = !string.IsNullOrEmpty(x.Zbl.ToString()) ? Convert.ToDecimal(x.Zbl) : 0;
                                   var A = !string.IsNullOrEmpty(x.a.ToString()) ? Convert.ToDecimal(x.a) : 0;
                                   var N = !string.IsNullOrEmpty(x.n.ToString()) ? Convert.ToDecimal(x.n) : 0;
                                   var MBSrope1 = !string.IsNullOrEmpty(x.MBSrope.ToString()) ? Convert.ToDecimal(x.MBSrope) : 0;
                                   var l11 = Convert.ToDecimal(Math.Round(Math.Sqrt(((C9 - F9) * (C9 - F9)) + ((D9 - G9) * (D9 - G9))), 2));
                                   var ΔZi1 = !string.IsNullOrEmpty(C9.ToString()) ? (E9 - H9) : 0;
                                   var Ei1 = !string.IsNullOrEmpty(x.E.ToString()) ? Convert.ToDecimal(x.E) : 0;
                                   var R9 = Convert.ToDouble(l11) > 0 ? Math.Round(Convert.ToDecimal((Math.Atan(Convert.ToDouble(ΔZi1) / Convert.ToDouble(l11))) * (180.0 / Math.PI)), 3) : Math.Round(Convert.ToDecimal((Math.Atan(0)) * (180.0 / Math.PI)), 3);
                                   var I9 = !string.IsNullOrEmpty(x.l0.ToString()) ? Convert.ToDecimal(x.l0) : 0;
                                   var S9 = R9 > 0 ? Convert.ToDecimal(Math.Round(Convert.ToDouble(Math.Cos(Convert.ToDouble(R9) * (Math.PI / 180.0))), 5)) : 0;
                                   var M9 = Math.Round(N * A, 3);

                                   // var Li1 = I9 == 0 ? 0 : Math.Round((I9 + l11 / S9), 8);
                                   var Li1 = S9 == 0 ? 0 : Math.Round((I9 + l11 / S9), 8);
                                   var V9 = Li1 == 0 ? 0 : Math.Round(M9 * Ei1 / Li1, 3);
                                   var KYI9 = Convert.ToDouble(l11) > 0 ? V9 * Convert.ToDecimal((G9 - D9) / Convert.ToDouble(l11)) * S9 : V9 * 0 * S9;

                                   list.Add(new TempMooringLine()
                                   {


                                          RopeId = x.RopeId,
                                          Id = x.Id,

                                          AssignNumber = x.AssignNumber,
                                          Location = x.Location,
                                          Certi_No = x.Certi_No,
                                          UniqueId = x.UniqueId,

                                          Xch = !string.IsNullOrEmpty(x.Xch.ToString()) ? Convert.ToDecimal(x.Xch) : 0,
                                          Ych = !string.IsNullOrEmpty(x.Ych.ToString()) ? Convert.ToDecimal(x.Ych) : 0,

                                          Zch = !string.IsNullOrEmpty(x.Zch.ToString()) ? Convert.ToDecimal(x.Zch) : 0,
                                          Xbl = !string.IsNullOrEmpty(x.Xbl.ToString()) ? Convert.ToDecimal(x.Xbl) : 0,

                                          Ybl = !string.IsNullOrEmpty(x.Ybl.ToString()) ? Convert.ToDecimal(x.Ybl) : 0,
                                          Zbl = !string.IsNullOrEmpty(x.Zbl.ToString()) ? Convert.ToDecimal(x.Zbl) : 0,
                                          l1 = l11,
                                          ΔZi = !string.IsNullOrEmpty(C9.ToString()) ? (E9 - H9) : 0,
                                          a = A,
                                          n = N,
                                          ai = Math.Round((N * A), 2),
                                          MBSrope = MBSrope1,
                                          MBS = Math.Round((MBSrope1 * N), 2),
                                          Ei = Math.Round(Ei1, 3),
                                          cosθi = Convert.ToDouble(l11) > 0 ? Math.Round(Convert.ToDecimal((G9 - D9) / Convert.ToDouble(l11)), 3) : 0,
                                          //cosθi = 0,
                                          φi = Math.Round(R9, 3),
                                          cosφi = Math.Round(S9, 3),
                                          l0 = Math.Round(I9, 2),
                                          Li = Math.Round(Li1, 3),
                                          ki = Math.Round(V9, 3),
                                          kyi = Math.Round(KYI9, 3),
                                          kyi_Xch = Math.Round(KYI9 * Convert.ToDecimal(C9), 3),
                                          kyi_Xch2 = Math.Round((KYI9 * Convert.ToDecimal(C9) * Convert.ToDecimal(C9)), 3)



                                   });
                            }

                            var vesselsinput = Convert.ToDecimal(VesselPs.GetList().Where(x => x.Description == "Xcg").Select(s => s.MainValue).FirstOrDefault());
                            new OutputsFinalForcesViewModel();
                            var finalforce = OutputsFinalForcesViewModel.loadWindLoad2.ToList();
                            fYkN = Convert.ToDecimal(finalforce.Where(x => x.Description1 == "CYW").Select(s => s.Values).FirstOrDefault()) * Convert.ToDecimal(9.81);
                            RaisePropertyChanged("FYkN");
                            mrkN_m = Convert.ToDecimal(finalforce.Where(x => x.Description1 == "CXYW").Select(s => s.Values).FirstOrDefault()) * Convert.ToDecimal(9.81);
                            RaisePropertyChanged("MrkN_m");
                            xcg_m = vesselsinput;
                            RaisePropertyChanged("Xcg_m");

                            aKN = list.Sum(s => Convert.ToDecimal(s.kyi));
                            RaisePropertyChanged("AKN");
                            bKN = list.Sum(s => Convert.ToDecimal(s.kyi_Xch));
                            RaisePropertyChanged("BKN");
                            cKN = list.Sum(s => Math.Round(Convert.ToDecimal(s.kyi_Xch2), 2));
                            RaisePropertyChanged("CKN");
                            sYM = (fYkN * cKN - (mrkN_m + fYkN * xcg_m) * bKN) / (aKN * cKN - bKN * bKN);
                            RaisePropertyChanged("SYM");
                            yradian = (fYkN * bKN - (mrkN_m + fYkN * xcg_m) * aKN) / ((bKN * bKN) - aKN * cKN);
                            RaisePropertyChanged("yradian");




                            foreach (var item in list)
                            {
                                   //var ropes = datakk.Where(x => x.RopeId == item.RopeId).FirstOrDefault();

                                   //if (ropes != null)
                                   //{

                                   //item.AssignNumber = ropes.AssignNumber;
                                   //item.Location = ropes.Location;
                                   //item.Certi_No = ropes.Certi_No;
                                   //item.UniqueId = ropes.UniqueId;

                                   var kyi1 = item.kyi == 0 ? 0 : Convert.ToDecimal(item.kyi * (sYM + item.Xch * yradian)) / 9.81m;
                                   var Ti1 = item.kyi == 0 ? 0 : kyi1 / (item.cosθi * item.cosφi);
                                   var FSi1 = Ti1 == 0 ? 0 : item.MBS / Ti1;
                                   list.Where(x => x.Id == item.Id && x.RopeId == item.RopeId).ToList().ForEach(x => { x.Fyi = Math.Round(kyi1, 3); x.Ti = Math.Round(Ti1, 3); x.FSi = Math.Round(FSi1, 3); });

                                   // }

                            }
                     }
                     catch //(Exception ex)
                     {
                           // throw ex;
                     }
                     return list;



              }


              public List<MooringLines> GetFinalMooringLines()
              {
                     List<MooringLines> LoadMooring = new List<MooringLines>();
                     DateTime dd = DateTime.Now.Date;

                     string qry = "MooringLineInputCalulator";
                     SqlDataAdapter sda = new SqlDataAdapter(qry, sc.con);
                     sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                     sda.SelectCommand.Parameters.AddWithValue("@RopeTail", 0);
                     System.Data.DataTable datatbl = new System.Data.DataTable();
                     sda.Fill(datatbl);



                     foreach (DataRow row in datatbl.Rows)
                     {
                            LoadMooring.Add(new MooringLines()
                            {
                                   AssignNumber = (row["AssignedNumber"] == DBNull.Value) ? "Not Assigned" : row["AssignedNumber"].ToString(),
                                   Location = (row["Location"] == DBNull.Value) ? "Not Assigned" : row["Location"].ToString(),
                                   Certi_No = (string)row["CertificateNumber"],
                                   UniqueId = (string)row["UniqueId"],
                                   RopeId = (int)row["RopeId"],
                                   Xch = Convert.ToDecimal((row["Xch"] == DBNull.Value) ? 0 : row["Xch"]),
                                   Ych = Convert.ToDecimal((row["Ych"] == DBNull.Value) ? 0 : row["Ych"]),
                                   Zch = Convert.ToDecimal((row["Zch"] == DBNull.Value) ? 0 : row["Zch"]),
                                   Xbl = Convert.ToDecimal((row["Xbl"] == DBNull.Value) ? 0 : row["Xbl"]),
                                   Ybl = Convert.ToDecimal((row["Ybl"] == DBNull.Value) ? 0 : row["Ybl"]),
                                   Zbl = Convert.ToDecimal((row["Zbl"] == DBNull.Value) ? 0 : row["Zbl"]),
                                   l0 = Convert.ToDecimal((row["l0"] == DBNull.Value) ? 0 : row["l0"]),
                                   E = Convert.ToDecimal((row["E"] == DBNull.Value) ? 0 : row["E"]),
                                   n = Convert.ToDecimal((row["n"] == DBNull.Value) ? 0 : row["n"]),
                                   a = Convert.ToDecimal((row["a"] == DBNull.Value) ? 0 : row["a"]),
                                   MBSrope = Convert.ToDecimal((row["MBSrope"] == DBNull.Value) ? 0 : row["MBSrope"]),
                                Id = Convert.ToInt32((row["Id"] == DBNull.Value) ? 0 : row["Id"]),
                                //===============================

                                Xch1 = Convert.ToDecimal((row["Xch"] == DBNull.Value) ? 0 : row["Xch"]),
                                   Ych1 = Convert.ToDecimal((row["Ych"] == DBNull.Value) ? 0 : row["Ych"]),
                                   Zch1 = Convert.ToDecimal((row["Zch"] == DBNull.Value) ? 0 : row["Zch"]),
                                   Xbl1 = Convert.ToDecimal((row["Xbl"] == DBNull.Value) ? 0 : row["Xbl"]),
                                   Ybl1 = Convert.ToDecimal((row["Ybl"] == DBNull.Value) ? 0 : row["Ybl"]),
                                   Zbl1 = Convert.ToDecimal((row["Zbl"] == DBNull.Value) ? 0 : row["Zbl"]),
                                   l01 = Convert.ToDecimal((row["l0"] == DBNull.Value) ? 0 : row["l0"]),
                                   E1 = Convert.ToDecimal((row["E"] == DBNull.Value) ? 0 : row["E"]),
                                   n1 = Convert.ToDecimal((row["n"] == DBNull.Value) ? 0 : row["n"]),
                                   a1 = Convert.ToDecimal((row["a"] == DBNull.Value) ? 0 : row["a"]),
                                   MBSrope1 = Convert.ToDecimal((row["MBSrope"] == DBNull.Value) ? 0 : row["MBSrope"]),
                            });
                     }

                     return LoadMooring;
              }

              private ObservableCollection<TempMooringLine> loadMoorings = new ObservableCollection<TempMooringLine>();
              public ObservableCollection<TempMooringLine> LoadMoorings
              {
                     get
                     {
                            return loadMoorings;
                     }
                     set
                     {
                            loadMoorings = value;
                            RaisePropertyChanged("LoadMoorings");
                     }
              }
       }
}

