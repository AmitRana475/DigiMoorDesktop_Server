using DataBuildingLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using WorkShipVersionII.ViewModel;


namespace WorkShipVersionII
{

       public partial class MainWindow : Window
       {
              static string tag = "";
              static IEnumerable<Button> Tabcollection;
              private readonly ShipmentContaxt sc;
              public MainWindow()
              {
                     InitializeComponent();

                     if (sc == null)
                     {
                            sc = new ShipmentContaxt();
                            sc.Configuration.ProxyCreationEnabled = false;
                     }

                     //var titleBar = this.GetForCurrentView().TitleBar;
                     //// Title Bar Content Area
                     //titleBar.BackgroundColor = Colors.DarkBlue;
                     //titleBar.ForegroundColor = Colors.White;
                     //// Title Bar Button Area
                     //titleBar.ButtonBackgroundColor = Colors.DarkBlue;





                     MyClickFunction1();

                     childWindow.DataContext = ChildWindowManager.Instance;

                     CheckFreezFunction();


              }

              private void CheckFreezFunction()
              {
                     try
                     {
                            //Set DataFreez on first runtime....
                            //MainViewModelCrewManagement dd = new MainViewModelCrewManagement();
                            var data = sc.Freezes.ToList();
                            if (data.Count() == 0)
                            {
                                   var freez = new FreezeClass
                                   {
                                          FreezeDays = 30,
                                          FreezeStatus = "Freeze",
                                          ApplyDate = Convert.ToDateTime(DateTime.Now.ToShortDateString()),
                                          DateFrom = DateTime.Now.AddDays(-30),
                                          DateTo = DateTime.Now.AddDays(+30)
                                   };

                                   sc.Freezes.Add(freez);
                                   sc.SaveChanges();
                            }
                            //.....................
                     }
                     catch (Exception ex)
                     {
                            sc.ErrorLog(ex);
                     }
              }

              private void MyClickFunction1()
              {
                     tag = "btnNotification";
                     foreach (Button item in this.grid1b.Children.OfType<Button>())
                     {
                            if (item.Name.Equals("btnNotification"))
                            {
                                   item.Background = new SolidColorBrush(Colors.WhiteSmoke);
                                   item.Foreground = new SolidColorBrush(Colors.Black);
                                   item.BorderBrush = new SolidColorBrush(Colors.Transparent);
                                   item.BorderThickness = new Thickness(0);

                            }
                            else
                            {
                                   item.Background = new SolidColorBrush(Colors.Transparent);
                                   item.Foreground = new SolidColorBrush(Colors.WhiteSmoke);
                                   item.BorderBrush = new SolidColorBrush(Colors.Transparent);
                                   item.BorderThickness = new Thickness(0);

                            }
                     }
              }

              public static void MyClickFunctionMainTabButton(string BtnName)
              {
                     tag = BtnName;
                     foreach (Button item in Tabcollection)
                     {
                            if (item.Name.Equals(BtnName))
                            {
                                   item.Background = new SolidColorBrush(Colors.WhiteSmoke);
                                   item.Foreground = new SolidColorBrush(Colors.Black);
                                   item.BorderBrush = new SolidColorBrush(Colors.Transparent);
                                   item.BorderThickness = new Thickness(0);

                            }
                            else
                            {
                                   item.Background = new SolidColorBrush(Colors.Transparent);
                                   item.Foreground = new SolidColorBrush(Colors.WhiteSmoke);
                                   item.BorderBrush = new SolidColorBrush(Colors.Transparent);
                                   item.BorderThickness = new Thickness(0);

                            }
                     }
              }

              private void MyClickFunction(object sender, RoutedEventArgs e)
              {

                     if (ChildWindowManager.Instance.XmlContent == null)
                     {

                            tag = ((Button)sender).Name;
                           // StaticHelper.TabButtonMenuName = tag;

                            Tabcollection = this.grid1b.Children.OfType<Button>();
                            foreach (Button item in this.grid1b.Children.OfType<Button>())
                            {
                                   if (item.Name.Equals(tag.ToString()))
                                   {
                                          item.Background = new SolidColorBrush(Colors.WhiteSmoke);
                                          item.Foreground = new SolidColorBrush(Colors.Black);
                                          item.BorderBrush = new SolidColorBrush(Colors.Transparent);
                                          item.BorderThickness = new Thickness(0);

                                   }
                                   else
                                   {
                                          item.Background = new SolidColorBrush(Colors.Transparent);
                                          item.Foreground = new SolidColorBrush(Colors.WhiteSmoke);
                                          item.BorderBrush = new SolidColorBrush(Colors.Transparent);
                                          item.BorderThickness = new Thickness(0);

                                   }
                            }
                     }
                     //else
                     //{

                     //    ChildWindowManager.Instance.CloseChildWindow();
                     //}

              }

              private void MouseEnterFunction(object sender, System.Windows.Input.MouseEventArgs e)
              {
                     if (ChildWindowManager.Instance.XmlContent == null)
                     {
                            var item = ((Button)sender);
                            if (item.Name.Equals(tag.ToString()))
                            {
                                   item.Background = new SolidColorBrush(Colors.WhiteSmoke);
                                   item.Foreground = new SolidColorBrush(Colors.Black);
                                   item.BorderBrush = new SolidColorBrush(Colors.Transparent);
                                   item.BorderThickness = new Thickness(0);

                            }
                            else
                            {
                                   item.Background = new SolidColorBrush(Colors.LightYellow);
                                   item.Foreground = new SolidColorBrush(Colors.Black);
                                   item.BorderBrush = new SolidColorBrush(Colors.Transparent);
                                   item.BorderThickness = new Thickness(0);

                            }
                     }

              }

              private void MouseLeaveFunction(object sender, System.Windows.Input.MouseEventArgs e)
              {
                     if (ChildWindowManager.Instance.XmlContent == null)
                     {
                            var item = ((Button)sender);
                            if (item.Name.Equals(tag.ToString()))
                            {
                                   //item.Background = new SolidColorBrush(Colors.LightYellow);
                                   //item.Foreground = new SolidColorBrush(Colors.Black);
                                   item.BorderBrush = new SolidColorBrush(Colors.Transparent);
                                   item.BorderThickness = new Thickness(0);

                            }
                            else
                            {
                                   item.Background = new SolidColorBrush(Colors.Transparent);
                                   item.Foreground = new SolidColorBrush(Colors.WhiteSmoke);
                                   item.BorderBrush = new SolidColorBrush(Colors.Transparent);
                                   item.BorderThickness = new Thickness(0);

                            }
                     }
              }



              //private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
              //{
              //    //this.WindowState = System.Windows.WindowState.Minimized;
              //    //System.GC.Collect();


              //    //this.Dispose();
              //    //Process[] pp = Process.GetProcessesByName("WorkShipVersionII");
              //    //foreach (Process pp1 in pp)
              //    //{
              //    //    pp1.Kill();
              //    //}
              //}

              private void Window_Loaded(object sender, RoutedEventArgs e)
              {
                     new MainViewModel();
              }

              //private void Window_Loaded(object sender, RoutedEventArgs e)
              //{
              //    LoginWindow login = new LoginWindow();
              //    login.Hide();
              //}
       }
}
