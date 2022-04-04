using DataBuildingLayer;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using WorkShipVersionII.ViewModel;


namespace WorkShipVersionII
{

       public partial class MainWindow : Window
       {
              static string tag = "";
              static IEnumerable<Button> Tabcollection;
              private readonly ShipmentContaxt sc;
              double screenWidth = 1360;
              public MainWindow()
              {
                     this.Language = XmlLanguage.GetLanguage(CultureInfo.CurrentCulture.IetfLanguageTag);
                     InitializeComponent();

                     if (sc == null)
                     {
                            sc = new ShipmentContaxt();
                            sc.Configuration.ProxyCreationEnabled = false;
                     }


                     MyClickFunction1();

                     childWindow.DataContext = ChildWindowManager.Instance;


                     screenWidth = SystemParameters.PrimaryScreenWidth;

                     //if (screenWidth > 1366)
                     //{
                     //       this.MaxHeight = 768;// SystemParameters.MaximizedPrimaryScreenHeight;
                     //       this.Height = 768;
                     //       this.Width = 1366;
                     //       this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                     //}
                     //else
                     //{
                     //       this.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
                     //       this.WindowState = WindowState.Maximized;
                     //}


                   
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
                     lblLoading.Visibility = Visibility.Visible;
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
                     lblLoading.Visibility = Visibility.Visible;

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



          
            

              private void Mainwindow_StateChanged(object sender, EventArgs e)
              {
                     if (WindowState == WindowState.Maximized || WindowState == WindowState.Normal)
                     {


                            // screenWidth = SystemParameters.PrimaryScreenWidth;

                            //if (screenWidth > 1366)
                            //{
                            //       this.MaxHeight = 768;// SystemParameters.MaximizedPrimaryScreenHeight;
                            //       this.Height = 768;
                            //       this.Width = 1366;
                            //       this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                            //}
                            //else
                            //{
                            //       this.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
                            //       this.WindowState = WindowState.Maximized;
                            //}
                     }
              }

              
       }
}
