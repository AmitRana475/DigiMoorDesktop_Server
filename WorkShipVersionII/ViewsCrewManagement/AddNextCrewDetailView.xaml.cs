using DataBuildingLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace WorkShipVersionII.ViewsCrewManagement
{

       public partial class AddNextCrewDetailView : UserControl
       {
              Border pnlTemp1;
              Border pnlTemp2;
              Border pnlTemp3;
              Border pnlTemp4;
              string lblWorkhour1 = string.Empty;
              string lblWorkhour2 = string.Empty;
              public string MouseState { get; set; } = "Left";
              private readonly ShipmentContaxt sc;
              public AddNextCrewDetailView()
              {
                     InitializeComponent();

                     if (sc == null)
                     {
                            sc = new ShipmentContaxt();
                     }

                     for (int intCount = 0; intCount < 48; intCount++)
                     {
                            lblNWkSea.Text += "0" + ",";
                            lblNWkSea1.Text += "0" + ",";
                            lblNWkPort.Text += "0" + ",";
                            lblNWkPort1.Text += "0" + ",";
                     }

              }

              private void UserControl_Loaded(object sender, System.Windows.RoutedEventArgs e)
              {
                     try
                     {
                           
                            var count1 = WKGridAtsea.RowDefinitions.Count;
                            var count2 = WKGridAtsea.ColumnDefinitions.Count;



                            for (int i = 0; i < count1; i++)
                            {
                                   for (int j = 0; j < count2; j++)            //it will creates panels dynamically according number of cells of tablelayoutpanel
                                   {
                                          // Border pnlTemp = new Border();
                                          pnlTemp1 = new Border()
                                          {
                                                 BorderThickness = new Thickness()
                                                 {
                                                        Bottom = 1,
                                                        Left = 1,
                                                        Right = 0,
                                                        Top = 1
                                                 },
                                                 BorderBrush = new SolidColorBrush(Colors.Gray)
                                          };
                                          pnlTemp1.Name = "B" + j;
                                          pnlTemp1.AllowDrop = true;

                                          if (pnlTemp1.Name == "B47")
                                                 pnlTemp1.BorderThickness = new Thickness(1);

                                          if (StaticHelper.Wathckeeping == true)
                                          {
                                                 pnlTemp1.Background = new SolidColorBrush(Colors.White);
                                                 pnlTemp1.MouseDown += PnlTemp1_MouseDown;
                                                 pnlTemp1.DragEnter += PnlTemp1_DragEnter;
                                          }
                                          else
                                          {
                                                 pnlTemp1.Background = new SolidColorBrush(Colors.LightGray);

                                          }


                                          pnlTemp1.SetValue(Grid.ColumnProperty, j);
                                          pnlTemp1.SetValue(Grid.RowProperty, i);
                                          WKGridAtsea.Children.Add(pnlTemp1);


                                   }

                            }

                            var count3 = NonWKGridAtsea.RowDefinitions.Count;
                            var count4 = NonWKGridAtsea.ColumnDefinitions.Count;

                            for (int i = 0; i < count3; i++)
                            {
                                   for (int j = 0; j < count4; j++)            //it will creates panels dynamically according number of cells of tablelayoutpanel
                                   {
                                          // Border pnlTemp = new Border();
                                          pnlTemp2 = new Border()
                                          {
                                                 BorderThickness = new Thickness()
                                                 {
                                                        Bottom = 1,
                                                        Left = 1,
                                                        Right = 0,
                                                        Top = 1
                                                 },
                                                 BorderBrush = new SolidColorBrush(Colors.Gray)
                                          };
                                          pnlTemp2.Name = "B" + j;
                                          pnlTemp2.AllowDrop = true;

                                          if (pnlTemp2.Name == "B47")
                                                 pnlTemp2.BorderThickness = new Thickness(1);

                                          pnlTemp2.Background = new SolidColorBrush(Colors.White);


                                          pnlTemp2.MouseDown += PnlTemp2_MouseDown;
                                          pnlTemp2.DragEnter += PnlTemp2_DragEnter;

                                          pnlTemp2.SetValue(Grid.ColumnProperty, j);
                                          pnlTemp2.SetValue(Grid.RowProperty, i);
                                          NonWKGridAtsea.Children.Add(pnlTemp2);

                                          // LayoutGrid.Children.Add(myBorder)// here we add panel control in each jth cell of TLP.
                                   }
                            }



                            var count5 = WKGridAtPort.RowDefinitions.Count;
                            var count6 = WKGridAtPort.ColumnDefinitions.Count;



                            for (int i = 0; i < count5; i++)
                            {
                                   for (int j = 0; j < count6; j++)
                                   {
                                          pnlTemp3 = new Border()
                                          {
                                                 BorderThickness = new Thickness()
                                                 {
                                                        Bottom = 1,
                                                        Left = 1,
                                                        Right = 0,
                                                        Top = 1
                                                 },
                                                 BorderBrush = new SolidColorBrush(Colors.Gray)
                                          };
                                          pnlTemp3.Name = "B" + j;
                                          pnlTemp3.AllowDrop = true;

                                          if (pnlTemp3.Name == "B47")
                                                 pnlTemp3.BorderThickness = new Thickness(1);

                                          if (StaticHelper.Wathckeeping == true)
                                          {
                                                 pnlTemp3.Background = new SolidColorBrush(Colors.White);
                                                 pnlTemp3.MouseDown += PnlTemp3_MouseDown;
                                                 pnlTemp3.DragEnter += PnlTemp3_DragEnter;
                                          }
                                          else
                                          {
                                                 pnlTemp3.Background = new SolidColorBrush(Colors.LightGray);
                                          }



                                          pnlTemp3.SetValue(Grid.ColumnProperty, j);
                                          pnlTemp3.SetValue(Grid.RowProperty, i);
                                          WKGridAtPort.Children.Add(pnlTemp3);

                                   }

                            }

                            var count7 = NonWKGridAtPort.RowDefinitions.Count;
                            var count8 = NonWKGridAtPort.ColumnDefinitions.Count;

                            for (int i = 0; i < count7; i++)
                            {
                                   for (int j = 0; j < count8; j++)
                                   {

                                          pnlTemp4 = new Border()
                                          {
                                                 BorderThickness = new Thickness()
                                                 {
                                                        Bottom = 1,
                                                        Left = 1,
                                                        Right = 0,
                                                        Top = 1
                                                 },
                                                 BorderBrush = new SolidColorBrush(Colors.Gray)
                                          };
                                          pnlTemp4.Name = "B" + j;
                                          pnlTemp4.AllowDrop = true;

                                          if (pnlTemp4.Name == "B47")
                                                 pnlTemp4.BorderThickness = new Thickness(1);

                                          pnlTemp4.Background = new SolidColorBrush(Colors.White);


                                          pnlTemp4.MouseDown += PnlTemp4_MouseDown;
                                          pnlTemp4.DragEnter += PnlTemp4_DragEnter;

                                          pnlTemp4.SetValue(Grid.ColumnProperty, j);
                                          pnlTemp4.SetValue(Grid.RowProperty, i);
                                          NonWKGridAtPort.Children.Add(pnlTemp4);


                                   }
                            }


                            string[] value1 = lblNWkSea1.Text.TrimEnd(',').Split(',');
                            string[] value2 = lblNWkSea.Text.TrimEnd(',').Split(',');

                            string[] value3 = lblNWkPort1.Text.TrimEnd(',').Split(',');
                            string[] value4 = lblNWkPort.Text.TrimEnd(',').Split(',');

                            var itemMain1 = WKGridAtsea.Children.OfType<Border>().ToList();
                            var itemMain2 = NonWKGridAtsea.Children.OfType<Border>().ToList();
                            var itemMain3 = WKGridAtPort.Children.OfType<Border>().ToList();
                            var itemMain4 = NonWKGridAtPort.Children.OfType<Border>().ToList();

                            var coun1 = value1.Length;
                            var coun2 = value2.Length;
                            var coun3 = value3.Length;
                            var coun4 = value4.Length;

                            for (int i = 0; i < coun1; i++)
                            {
                                   //if (coun > i)
                                   //{
                                   var tag = "B" + i.ToString();
                                   Border item1 = itemMain1.Where(x => x.Name.Equals(tag.ToString())).FirstOrDefault();


                                   if (value1[i] == "1")
                                   {
                                          if (item1 != null)
                                          {
                                                 if (StaticHelper.Wathckeeping == true)
                                                 {

                                                        item1.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#003366"));
                                                 }
                                                 else
                                                 {
                                                        item1.Background = new SolidColorBrush(Colors.LightGray);
                                                       
                                                 }
                                          }


                                   }
                                   if (value1[i] == "2")
                                   {
                                          if (item1 != null)
                                          {
                                                 if (StaticHelper.Wathckeeping == true)
                                                 {

                                                        item1.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#808080"));
                                                 }
                                                 else
                                                 {
                                                        item1.Background = new SolidColorBrush(Colors.LightGray);
                                                       
                                                 }
                                          }
                                               
                                         
                                   }

                                   if (value1[i] == "0")
                                   {
                                          if (item1 != null)
                                          {
                                                 if (StaticHelper.Wathckeeping == true)
                                                 {
                                                        item1.Background = new SolidColorBrush(Colors.White);
                                                 }
                                                 else
                                                 {
                                                        item1.Background = new SolidColorBrush(Colors.LightGray);
                                                 }
                                          }
                                   }




                                   //}
                            }

                            for (int i = 0; i < coun2; i++)
                            {
                                   var tag = "B" + i.ToString();
                                   Border item2 = itemMain2.Where(x => x.Name.Equals(tag.ToString())).FirstOrDefault();

                                   if (value2[i] == "1")
                                   {
                                          if (item2 != null)
                                                 item2.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#003366"));

                                   }
                                   if (value2[i] == "2")
                                   {
                                          if (item2 != null)
                                          {
                                                 if (StaticHelper.Wathckeeping == true)
                                                 {

                                                        item2.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#808080"));
                                                 }
                                                 else
                                                 {
                                                        item2.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#003366"));
                                                       
                                                 }
                                          }
                                                
                                         
                                   }

                                   if (value2[i] == "0")
                                   {
                                          if (item2 != null)
                                                 item2.Background = new SolidColorBrush(Colors.White);
                                   }


                            }
                            for (int i = 0; i < coun3; i++)
                            {
                                   var tag = "B" + i.ToString();
                                   Border item3 = itemMain3.Where(x => x.Name.Equals(tag.ToString())).FirstOrDefault();

                                   if (value3[i] == "1")
                                   {
                                          if (item3 != null)
                                          {
                                                 if (StaticHelper.Wathckeeping == true)
                                                 {

                                                        item3.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#003366"));
                                                 }
                                                 else
                                                 {
                                                        item3.Background = new SolidColorBrush(Colors.LightGray);

                                                 }
                                          }


                                   }
                                   if (value3[i] == "2")
                                   {
                                          if (item3 != null)
                                          {
                                                 if (StaticHelper.Wathckeeping == true)
                                                 {

                                                        item3.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#808080"));
                                                 }
                                                 else
                                                 {
                                                        item3.Background = new SolidColorBrush(Colors.LightGray);

                                                 }
                                          }


                                   }

                                   if (value3[i] == "0")
                                   {
                                          if (item3 != null)
                                          {
                                                 if (StaticHelper.Wathckeeping == true)
                                                 {
                                                        item3.Background = new SolidColorBrush(Colors.White);
                                                 }
                                                 else
                                                 {
                                                        item3.Background = new SolidColorBrush(Colors.LightGray);
                                                 }
                                          }
                                   }

                            }
                            for (int i = 0; i < coun4; i++)
                            {
                                   var tag = "B" + i.ToString();
                                   Border item4 = itemMain4.Where(x => x.Name.Equals(tag.ToString())).FirstOrDefault();

                                   if (value4[i] == "1")
                                   {
                                          if (item4 != null)
                                                 item4.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#003366"));

                                   }
                                   if (value4[i] == "2")
                                   {
                                          if (item4 != null)
                                          {
                                                 if (StaticHelper.Wathckeeping == true)
                                                 {

                                                        item4.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#808080"));
                                                 }
                                                 else
                                                 {
                                                        item4.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#003366"));

                                                 }
                                          }
                                                                                         
                                   }

                                   if (value4[i] == "0")
                                   {
                                          if (item4 != null)
                                                 item4.Background = new SolidColorBrush(Colors.White);
                                   }

                            }


                     }
                     catch (Exception ex)
                     {
                            sc.ErrorLog(ex);
                     }

              }

              private void PnlTemp1_DragEnter(object sender, DragEventArgs e)
              {
                     try
                     {

                            string lblNWkSe = "";
                            string lblNWkSeab = "";
                            var aa = (SolidColorBrush)(new BrushConverter().ConvertFrom("#003366"));
                            var bb = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFFFFF"));
                            //var cc = (SolidColorBrush)(new BrushConverter().ConvertFrom("#666666"));
                            var cc = (SolidColorBrush)(new BrushConverter().ConvertFrom("#808080"));

                            string colorValue1 = ((SolidColorBrush)aa).Color.ToString(); //blue
                            string colorValue2 = ((SolidColorBrush)bb).Color.ToString();//White
                            string colorValue3 = ((SolidColorBrush)cc).Color.ToString();//Grey

                            //if (MouseState == "Left")
                            //{
                            //    ((Border)sender).Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#003366"));
                            //    e.Effects = DragDropEffects.Copy;

                            //    var tag = ((Border)sender).Name;
                            //    Border item = this.NonWKGridAtsea.Children.OfType<Border>().Where(x => x.Name.Equals(tag.ToString())).FirstOrDefault();


                            //    item.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#666666"));//new SolidColorBrush(Colors.DarkGray);
                            //    item.IsEnabled = false;

                            //}
                            ////=========================== right move
                            //if (MouseState == "Right")
                            //{
                            //    ((Border)sender).Background = new SolidColorBrush(Colors.White);//FromArgb(0, 51, 102); //Color.DarkSlateBlue;
                            //    e.Effects = DragDropEffects.Copy;

                            //    var tag = ((Border)sender).Name;
                            //    Border item = this.NonWKGridAtsea.Children.OfType<Border>().Where(x => x.Name.Equals(tag.ToString())).FirstOrDefault();

                            //    item.Background = new SolidColorBrush(Colors.White);
                            //    item.IsEnabled = true;

                            //}


                            var bs = ((Border)sender).Background;
                            if (((SolidColorBrush)bs).Color.ToString() == colorValue2)
                            {
                                   ((Border)sender).Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#003366")); //FromArgb(0, 51, 102); //Color.DarkSlateBlue;
                                   e.Effects = DragDropEffects.Copy;

                                   var tag = ((Border)sender).Name;
                                   Border item = this.NonWKGridAtsea.Children.OfType<Border>().Where(x => x.Name.Equals(tag.ToString())).FirstOrDefault();


                                   //item.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#666666"));//new SolidColorBrush(Colors.DarkGray);
                                   item.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#808080"));
                                   item.IsEnabled = false;
                            }
                            else if (((SolidColorBrush)bs).Color.ToString() == colorValue1)
                            {
                                   ((Border)sender).Background = new SolidColorBrush(Colors.White);//FromArgb(0, 51, 102); //Color.DarkSlateBlue;
                                   e.Effects = DragDropEffects.Copy;

                                   var tag = ((Border)sender).Name;

                                   Border item = this.NonWKGridAtsea.Children.OfType<Border>().Where(x => x.Name.Equals(tag.ToString())).FirstOrDefault();


                                   item.Background = new SolidColorBrush(Colors.White);
                                   item.IsEnabled = true;


                            }



                            foreach (Border item in this.WKGridAtsea.Children.OfType<Border>())
                            {
                                   var ss = item.Background;
                                   if (((SolidColorBrush)ss).Color.ToString() == colorValue3)
                                   {
                                          lblNWkSe += "2" + ",";
                                   }
                                   else if (((SolidColorBrush)ss).Color.ToString() == colorValue1)
                                   {
                                          lblNWkSe += "1" + ",";
                                   }
                                   else
                                   {
                                          lblNWkSe += "0" + ",";
                                   }
                            }
                            lblNWkSea1.Text = lblNWkSe;

                            foreach (Border item in this.NonWKGridAtsea.Children.OfType<Border>())
                            {
                                   var ss = item.Background;
                                   if (((SolidColorBrush)ss).Color.ToString() == colorValue3)
                                   {
                                          lblNWkSeab += "2" + ",";
                                   }
                                   else if (((SolidColorBrush)ss).Color.ToString() == colorValue1)
                                   {
                                          lblNWkSeab += "1" + ",";
                                   }
                                   else
                                   {
                                          lblNWkSeab += "0" + ",";
                                   }
                            }

                            lblNWkSea.Text = lblNWkSeab;
                            //lblWorkhour1 = Morethan1hrsNormal(lblNWkSea.Text);

                            var checkcondition = CheckNormalConditions(lblNWkSea.Text);
                            TxtWorkHrsNon.Text = checkcondition.Item1;
                            NCWatchKeeping.Text = checkcondition.Item2;

                     }
                     catch (Exception ex)
                     {
                            sc.ErrorLog(ex);
                     }
              }

              private void PnlTemp1_MouseDown(object sender, MouseButtonEventArgs e)
              {

                     DragDrop.DoDragDrop(pnlTemp1, pnlTemp1.Background, DragDropEffects.Copy);

                     //if (e.ChangedButton == MouseButton.Left)
                     //{
                     //    MouseState = "Left";
                     //    DragDrop.DoDragDrop(pnlTemp1, pnlTemp1.Background, DragDropEffects.Copy);
                     //}

                     //if (e.ChangedButton == MouseButton.Right)
                     //{
                     //    MouseState = "Right";
                     //    DragDrop.DoDragDrop(pnlTemp1, pnlTemp1.Background, DragDropEffects.Copy);
                     //}


              }

              private void PnlTemp2_DragEnter(object sender, DragEventArgs e)
              {
                     try
                     {
                            string lblNWkSea11 = "";
                            string lblNWkSea2 = "";
                            var aa = (SolidColorBrush)(new BrushConverter().ConvertFrom("#003366"));
                            var bb = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFFFFF"));
                            // var cc = (SolidColorBrush)(new BrushConverter().ConvertFrom("#666666"));
                            var cc = (SolidColorBrush)(new BrushConverter().ConvertFrom("#808080"));

                            string colorValue1 = ((SolidColorBrush)aa).Color.ToString(); //blue
                            string colorValue2 = ((SolidColorBrush)bb).Color.ToString();//White
                            string colorValue3 = ((SolidColorBrush)cc).Color.ToString();//Grey

                            //if (MouseState == "Left")
                            //{

                            //    ((Border)sender).Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#003366"));
                            //    e.Effects = DragDropEffects.Copy;


                            //}
                            //if (MouseState == "Right")
                            //{
                            //    ((Border)sender).Background = new SolidColorBrush(Colors.White);//FromArgb(0, 51, 102); //Color.DarkSlateBlue;
                            //    e.Effects = DragDropEffects.Copy;


                            //}


                            var bs = ((Border)sender).Background;
                            if (((SolidColorBrush)bs).Color.ToString() == colorValue2)
                            {
                                   ((Border)sender).Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#003366"));//FromArgb(0, 51, 102); //Color.DarkSlateBlue;
                                   e.Effects = DragDropEffects.Copy;


                            }
                            else if (((SolidColorBrush)bs).Color.ToString() == colorValue1)
                            {
                                   ((Border)sender).Background = new SolidColorBrush(Colors.White);//FromArgb(0, 51, 102); //Color.DarkSlateBlue;
                                   e.Effects = DragDropEffects.Copy;
                            }



                            foreach (Border item in this.NonWKGridAtsea.Children.OfType<Border>())
                            {
                                   var ss = item.Background;
                                   if (((SolidColorBrush)ss).Color.ToString() == colorValue3)
                                   {
                                          lblNWkSea11 += "2" + ",";
                                   }
                                   else if (((SolidColorBrush)ss).Color.ToString() == colorValue1)
                                   {
                                          lblNWkSea11 += "1" + ",";
                                   }
                                   else
                                   {
                                          lblNWkSea11 += "0" + ",";
                                   }
                            }

                            lblNWkSea.Text = lblNWkSea11;

                            foreach (Border item in this.WKGridAtsea.Children.OfType<Border>())
                            {
                                   var ss = item.Background;
                                   if (((SolidColorBrush)ss).Color.ToString() == colorValue3)
                                   {
                                          lblNWkSea2 += "2" + ",";
                                   }
                                   else if (((SolidColorBrush)ss).Color.ToString() == colorValue1)
                                   {
                                          lblNWkSea2 += "1" + ",";
                                   }
                                   else
                                   {
                                          lblNWkSea2 += "0" + ",";
                                   }
                            }
                            lblNWkSea1.Text = lblNWkSea2;


                            var checkcondition = CheckNormalConditions(lblNWkSea.Text);
                            TxtWorkHrsNon.Text = checkcondition.Item1;
                            NCWatchKeeping.Text = checkcondition.Item2;

                     }
                     catch (Exception ex)
                     {
                            sc.ErrorLog(ex);
                     }

              }
              private void PnlTemp2_MouseDown(object sender, MouseButtonEventArgs e)
              {

                     DragDrop.DoDragDrop(pnlTemp2, pnlTemp2.Background, DragDropEffects.Copy);

                     //if (e.ChangedButton == MouseButton.Left)
                     //{
                     //    MouseState = "Left";
                     //    DragDrop.DoDragDrop(pnlTemp2, pnlTemp2.Background, DragDropEffects.Copy);
                     //}

                     //if (e.ChangedButton == MouseButton.Right)
                     //{
                     //    MouseState = "Right";
                     //    DragDrop.DoDragDrop(pnlTemp2, pnlTemp2.Background, DragDropEffects.Copy);
                     //}



              }



              private void PnlTemp3_DragEnter(object sender, DragEventArgs e)
              {
                     try
                     {
                            string lblNWkSe = "";
                            string lblNWkSeab = "";
                            var aa = (SolidColorBrush)(new BrushConverter().ConvertFrom("#003366"));
                            var bb = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFFFFF"));
                            var cc = (SolidColorBrush)(new BrushConverter().ConvertFrom("#808080"));
                            //var cc = (SolidColorBrush)(new BrushConverter().ConvertFrom("#666666"));

                            string colorValue1 = ((SolidColorBrush)aa).Color.ToString(); //blue
                            string colorValue2 = ((SolidColorBrush)bb).Color.ToString();//White
                            string colorValue3 = ((SolidColorBrush)cc).Color.ToString();//Grey


                            //if (MouseState == "Left")
                            //{
                            //    ((Border)sender).Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#003366"));
                            //    e.Effects = DragDropEffects.Copy;

                            //    var tag = ((Border)sender).Name;
                            //    Border item = this.NonWKGridAtPort.Children.OfType<Border>().Where(x => x.Name.Equals(tag.ToString())).FirstOrDefault();

                            //    item.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#666666"));//new SolidColorBrush(Colors.DarkGray);
                            //    item.IsEnabled = false;

                            //}
                            ////=========================== right move
                            //if (MouseState == "Right")
                            //{
                            //    ((Border)sender).Background = new SolidColorBrush(Colors.White);//FromArgb(0, 51, 102); //Color.DarkSlateBlue;
                            //    e.Effects = DragDropEffects.Copy;

                            //    var tag = ((Border)sender).Name;
                            //    Border item = this.NonWKGridAtPort.Children.OfType<Border>().Where(x => x.Name.Equals(tag.ToString())).FirstOrDefault();

                            //    item.Background = new SolidColorBrush(Colors.White);
                            //    item.IsEnabled = true;

                            //}

                            var bs = ((Border)sender).Background;
                            if (((SolidColorBrush)bs).Color.ToString() == colorValue2)
                            {
                                   ((Border)sender).Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#003366")); //FromArgb(0, 51, 102); //Color.DarkSlateBlue;
                                   e.Effects = DragDropEffects.Copy;

                                   var tag = ((Border)sender).Name;
                                   Border item = this.NonWKGridAtPort.Children.OfType<Border>().Where(x => x.Name.Equals(tag.ToString())).FirstOrDefault();


                                   //item.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#666666"));//new SolidColorBrush(Colors.DarkGray);
                                   item.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#808080"));
                                   item.IsEnabled = false;
                            }
                            else if (((SolidColorBrush)bs).Color.ToString() == colorValue1)
                            {
                                   ((Border)sender).Background = new SolidColorBrush(Colors.White);//FromArgb(0, 51, 102); //Color.DarkSlateBlue;
                                   e.Effects = DragDropEffects.Copy;

                                   var tag = ((Border)sender).Name;

                                   Border item = this.NonWKGridAtPort.Children.OfType<Border>().Where(x => x.Name.Equals(tag.ToString())).FirstOrDefault();


                                   item.Background = new SolidColorBrush(Colors.White);
                                   item.IsEnabled = true;


                            }



                            foreach (Border item in this.WKGridAtPort.Children.OfType<Border>())
                            {
                                   var ss = item.Background;
                                   if (((SolidColorBrush)ss).Color.ToString() == colorValue3)
                                   {
                                          lblNWkSe += "2" + ",";
                                   }
                                   else if (((SolidColorBrush)ss).Color.ToString() == colorValue1)
                                   {
                                          lblNWkSe += "1" + ",";
                                   }
                                   else
                                   {
                                          lblNWkSe += "0" + ",";
                                   }
                            }
                            lblNWkPort1.Text = lblNWkSe;

                            foreach (Border item in this.NonWKGridAtPort.Children.OfType<Border>())
                            {
                                   var ss = item.Background;
                                   if (((SolidColorBrush)ss).Color.ToString() == colorValue3)
                                   {
                                          lblNWkSeab += "2" + ",";
                                   }
                                   else if (((SolidColorBrush)ss).Color.ToString() == colorValue1)
                                   {
                                          lblNWkSeab += "1" + ",";
                                   }
                                   else
                                   {
                                          lblNWkSeab += "0" + ",";
                                   }
                            }

                            lblNWkPort.Text = lblNWkSeab;

                            var checkcondition = CheckNormalConditions(lblNWkPort.Text);
                            TxtWorkHrsPort.Text = checkcondition.Item1;
                            NCWatchKeepingPort.Text = checkcondition.Item2;

                     }
                     catch (Exception ex)
                     {
                            sc.ErrorLog(ex);
                     }
              }

              private void PnlTemp3_MouseDown(object sender, MouseButtonEventArgs e)
              {
                     DragDrop.DoDragDrop(pnlTemp3, pnlTemp3.Background, DragDropEffects.Copy);

                     //if (e.ChangedButton == MouseButton.Left)
                     //{
                     //    MouseState = "Left";
                     //    DragDrop.DoDragDrop(pnlTemp3, pnlTemp3.Background, DragDropEffects.Copy);
                     //}

                     //if (e.ChangedButton == MouseButton.Right)
                     //{
                     //    MouseState = "Right";
                     //    DragDrop.DoDragDrop(pnlTemp3, pnlTemp3.Background, DragDropEffects.Copy);
                     //}
              }

              private void PnlTemp4_DragEnter(object sender, DragEventArgs e)
              {
                     try
                     {
                            string lblNWkSea11 = "";
                            string lblNWkSea2 = "";
                            var aa = (SolidColorBrush)(new BrushConverter().ConvertFrom("#003366"));
                            var bb = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFFFFF"));
                            var cc = (SolidColorBrush)(new BrushConverter().ConvertFrom("#808080"));
                            //var cc = (SolidColorBrush)(new BrushConverter().ConvertFrom("#666666"));

                            string colorValue1 = ((SolidColorBrush)aa).Color.ToString(); //blue
                            string colorValue2 = ((SolidColorBrush)bb).Color.ToString();//White
                            string colorValue3 = ((SolidColorBrush)cc).Color.ToString();//Grey

                            //if (MouseState == "Left")
                            //{

                            //    ((Border)sender).Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#003366"));
                            //    e.Effects = DragDropEffects.Copy;


                            //}
                            //if (MouseState == "Right")
                            //{
                            //    ((Border)sender).Background = new SolidColorBrush(Colors.White);//FromArgb(0, 51, 102); //Color.DarkSlateBlue;
                            //    e.Effects = DragDropEffects.Copy;

                            //}

                            var bs = ((Border)sender).Background;
                            if (((SolidColorBrush)bs).Color.ToString() == colorValue2)
                            {
                                   ((Border)sender).Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#003366"));//FromArgb(0, 51, 102); //Color.DarkSlateBlue;
                                   e.Effects = DragDropEffects.Copy;


                            }
                            else if (((SolidColorBrush)bs).Color.ToString() == colorValue1)
                            {
                                   ((Border)sender).Background = new SolidColorBrush(Colors.White);//FromArgb(0, 51, 102); //Color.DarkSlateBlue;
                                   e.Effects = DragDropEffects.Copy;
                            }


                            foreach (Border item in this.NonWKGridAtPort.Children.OfType<Border>())
                            {
                                   var ss = item.Background;
                                   if (((SolidColorBrush)ss).Color.ToString() == colorValue3)
                                   {
                                          lblNWkSea11 += "2" + ",";
                                   }
                                   else if (((SolidColorBrush)ss).Color.ToString() == colorValue1)
                                   {
                                          lblNWkSea11 += "1" + ",";
                                   }
                                   else
                                   {
                                          lblNWkSea11 += "0" + ",";
                                   }
                            }

                            lblNWkPort.Text = lblNWkSea11;

                            foreach (Border item in this.WKGridAtPort.Children.OfType<Border>())
                            {
                                   var ss = item.Background;
                                   if (((SolidColorBrush)ss).Color.ToString() == colorValue3)
                                   {
                                          lblNWkSea2 += "2" + ",";
                                   }
                                   else if (((SolidColorBrush)ss).Color.ToString() == colorValue1)
                                   {
                                          lblNWkSea2 += "1" + ",";
                                   }
                                   else
                                   {
                                          lblNWkSea2 += "0" + ",";
                                   }
                            }
                            lblNWkPort1.Text = lblNWkSea2;


                            var checkcondition = CheckNormalConditions(lblNWkPort.Text);
                            TxtWorkHrsPort.Text = checkcondition.Item1;
                            NCWatchKeepingPort.Text = checkcondition.Item2;

                     }
                     catch (Exception ex)
                     {
                            sc.ErrorLog(ex);
                     }


              }
              private void PnlTemp4_MouseDown(object sender, MouseButtonEventArgs e)
              {
                     DragDrop.DoDragDrop(pnlTemp4, pnlTemp4.Background, DragDropEffects.Copy);

                     //if (e.ChangedButton == MouseButton.Left)
                     //{
                     //    MouseState = "Left";
                     //    DragDrop.DoDragDrop(pnlTemp4, pnlTemp4.Background, DragDropEffects.Copy);
                     //}

                     //if (e.ChangedButton == MouseButton.Right)
                     //{
                     //    MouseState = "Right";
                     //    DragDrop.DoDragDrop(pnlTemp4, pnlTemp4.Background, DragDropEffects.Copy);
                     //}

              }


              private string Morethan1hrsNormal(string label42)
              {
                     string lbl = label42;
                     //........New Code Implementation For more than an hour of rest.........
                     try
                     {

                            label42 = label42 + label42 + label42;

                            string[] arraybb = null;
                            arraybb = label42.Substring(0, label42.Length - 1).Split(',');
                            string bmsnewimp = string.Empty;
                            int jqn = 0;
                            int count = arraybb.Length;
                            for (jqn = 0; jqn < count; jqn++)
                            {
                                   if (arraybb.Length > Convert.ToInt32(jqn + 3))
                                   {
                                          if (arraybb[jqn] == "1" && arraybb[jqn + 1] == "0" && arraybb[jqn + 2] == "0" && arraybb[jqn + 3] == "0")
                                          {

                                                 bmsnewimp += arraybb[jqn].ToString() + ',';
                                          }
                                          else if (arraybb[jqn] == "1" && arraybb[jqn + 1] == "0" && arraybb[jqn + 2] == "0")
                                          {

                                                 bmsnewimp += arraybb[jqn].ToString() + ',' + "1" + ',' + "1" + ',';

                                                 jqn = jqn + 2;
                                          }
                                          else if (arraybb[jqn] == "1" && arraybb[jqn + 1] == "0")
                                          {

                                                 bmsnewimp += arraybb[jqn].ToString() + ',' + "1" + ',';

                                                 jqn = jqn + 1;
                                          }

                                          else
                                          {
                                                 bmsnewimp += arraybb[jqn].ToString() + ',';
                                          }
                                   }
                                   else if (arraybb.Length >= Convert.ToInt32(jqn + 2))
                                   {
                                          if (arraybb[jqn] == "0" && arraybb[jqn + 1] == "1")
                                          {
                                                 if (arraybb[jqn - 1] == "0" && arraybb[jqn - 2] == "0")
                                                 {
                                                        bmsnewimp += "0" + ',' + "1" + ',';
                                                        jqn = jqn + 1;
                                                 }
                                                 else
                                                 {
                                                        bmsnewimp += "1" + ',' + "1" + ',';

                                                        jqn = jqn + 1;
                                                 }


                                          }
                                          else
                                          {
                                                 bmsnewimp += arraybb[jqn].ToString() + ',';
                                          }
                                   }

                                   else
                                   {
                                          bmsnewimp += arraybb[jqn].ToString() + ',';
                                   }

                            }

                            string[] arr = bmsnewimp.TrimEnd(',').Split(',');
                            arr = arr.Skip(48).Take(48).Select(i => i.ToString()).ToArray();
                            string bms = string.Join(",", arr);


                            return bms + ",";
                     }
                     catch (Exception ex)
                     {
                            sc.ErrorLog(ex);
                            return lbl;
                     }
              }

              private Tuple<string, string> CheckNormalConditions(string label42)
              {
                     label42 = label42.Replace("2", "1");



                     label42 = Morethan1hrsNormal(label42);
                     string[] s2 = null;
                     s2 = label42.Substring(0, label42.Length - 1).Split(',');

                     string lbl1main = "";
                     int intindex = 0;
                     int tblcount = s2.Length;

                     for (int s = 0; s < tblcount; s++)
                     {
                            if (s2[s] == "1")
                            {
                                   intindex = s;

                                   lbl1main += s.ToString() + ",";
                            }

                     }

                     string[] nw = null;
                     nw = lbl1main.Split(',');
                     int result = nw.Count() - 1;
                     double result1 = result / 2.0;
                     string txtWH = "0.0";
                     txtWH = result1.ToString();


                     StringBuilder ncs = new StringBuilder();
                     ncs.Clear();



                     string lblmain = "";
                     double totalmain = 0;
                     int intindex1 = 0;
                     int b = 0;

                     for (int s = 0; s <= 47; s++)
                     {

                            if (s2[s] == "1")
                            {
                                   intindex1 = s;



                                   if (totalmain == 0)
                                   {
                                          totalmain = s;

                                          b = s;

                                          lblmain += b.ToString() + ",";

                                          totalmain = s + 1;

                                   }

                            }
                            else if (s2[s] == "0")
                            {
                                   if (totalmain != 0)
                                   {
                                          lblmain += intindex1.ToString() + ",";

                                          intindex1 = 0;
                                          totalmain = 0;
                                   }

                            }
                            if (s == s2.Length - 1)
                            {
                                   if (totalmain != 0)
                                   {
                                          lblmain += intindex1.ToString() + ",";
                                          intindex1 = 0;
                                          totalmain = 0;
                                   }
                            }

                     }
                     //..............


                     string[] st4bms = s2.Concat(s2).ToArray();
                     string nc6hrs = string.Empty;
                     string nc10hrs = string.Empty;

                     string[] today = null;
                     today = lblmain.TrimEnd(',').Split(new[] { ',' });

                     int lenth = string.IsNullOrEmpty(lblmain) ? 0 : today.Length;
                     for (int y = 0; y < lenth; y++)
                     {


                            int index1bm = Convert.ToInt32(today[y]) + 1;
                            int countbm = index1bm + 48;


                            double totalbm = 0;
                            List<double> listbbm = new List<double>();
                            listbbm.Clear();
                            string[] numsbm = st4bms.Skip(index1bm).Take(48).ToArray();


                            int norm16 = 0;
                            norm16 = numsbm.Length;
                            for (long i = 0; i < norm16; i++)
                            {

                                   if (Convert.ToInt32(numsbm[i]) == 0)
                                   {
                                          totalbm += 0.5;
                                   }
                                   else if (Convert.ToInt32(numsbm[i]) == 1)
                                   {
                                          if (totalbm != 0)
                                          {
                                                 listbbm.Add(totalbm);
                                                 totalbm = 0;
                                          }
                                   }
                                   if (i == numsbm.Length - 1)
                                   {
                                          if (totalbm != 0)
                                          {
                                                 listbbm.Add(totalbm);
                                                 totalbm = 0;
                                          }
                                   }

                            }

                            if (listbbm.Count() == 0)
                                   listbbm.Add(0);


                            var asd = listbbm.Where(element => element >= 6).FirstOrDefault();

                            if (asd == 0 && listbbm.Count > 0)
                            {
                                   nc6hrs = "No minimum 6 hour consecutive rest period";

                            }
                            var hrs10Count = listbbm.Sum();


                            if (listbbm.Count >= 2 && hrs10Count >= 10)
                            {
                                   double A = 0, B = 0, C = 0, D = 0, E = 0, F = 0, G = 0, H = 0, I = 0, J = 0, K = 0, L = 0, M = 0, N = 0, O = 0, P = 0, Q = 0, R = 0, S = 0, A1 = 0, B1 = 0, C1 = 0, D1 = 0, E1 = 0, F1 = 0, G1 = 0, H1 = 0, I1 = 0, J1 = 0, K1 = 0, L1 = 0, M1 = 0, N1 = 0, O1 = 0, P1 = 0, Q1 = 0, R1 = 0, S1 = 0;

                                   int aa1 = 0;
                                   foreach (var abc1 in listbbm)
                                   {

                                          if (abc1 >= 10)
                                          {
                                                 aa1 = aa1 + 1;
                                                 break;
                                          }

                                          else
                                          {
                                                 if (abc1 == 9.5)
                                                 {
                                                        if (A == 9.5)
                                                        {
                                                               A1 = abc1;
                                                        }
                                                        else
                                                        {
                                                               A = abc1;
                                                        }
                                                 }
                                                 else if (abc1 == 9.0)
                                                 {
                                                        if (B == 9.0)
                                                        {
                                                               B1 = abc1;
                                                        }
                                                        else
                                                        {
                                                               B = abc1;
                                                        }
                                                 }

                                                 else if (abc1 == 8.5)
                                                 {
                                                        if (C == 8.5)
                                                        {
                                                               C1 = abc1;
                                                        }
                                                        else
                                                        {
                                                               C = abc1;
                                                        }
                                                 }
                                                 else if (abc1 == 8.0)
                                                 {
                                                        if (D == 8.0)
                                                        {
                                                               D1 = abc1;
                                                        }
                                                        else
                                                        {
                                                               D = abc1;
                                                        }
                                                 }
                                                 else if (abc1 == 7.5)
                                                 {
                                                        if (E == 7.5)
                                                        {
                                                               E1 = abc1;
                                                        }
                                                        else
                                                        {
                                                               E = abc1;
                                                        }
                                                 }
                                                 else if (abc1 == 7.0)
                                                 {
                                                        if (F == 7.0)
                                                        {
                                                               F1 = abc1;
                                                        }
                                                        else
                                                        {
                                                               F = abc1;
                                                        }
                                                 }
                                                 else if (abc1 == 6.5)
                                                 {
                                                        if (G == 6.5)
                                                        {
                                                               G1 = abc1;
                                                        }
                                                        else
                                                        {
                                                               G = abc1;
                                                        }
                                                 }
                                                 else if (abc1 == 6.0)
                                                 {
                                                        if (H == 6.0)
                                                        {
                                                               H1 = abc1;
                                                        }
                                                        else
                                                        {
                                                               H = abc1;
                                                        }
                                                 }
                                                 else if (abc1 == 5.5)
                                                 {
                                                        if (I == 5.5)
                                                        {
                                                               I1 = abc1;
                                                        }
                                                        else
                                                        {
                                                               I = abc1;
                                                        }
                                                 }
                                                 else if (abc1 == 5.0)
                                                 {
                                                        if (J == 5.0)
                                                        {
                                                               J1 = abc1;
                                                        }
                                                        else
                                                        {
                                                               J = abc1;
                                                        }
                                                 }
                                                 else if (abc1 == 4.5)
                                                 {
                                                        if (K == 4.5)
                                                        {
                                                               K1 = abc1;
                                                        }
                                                        else
                                                        {
                                                               K = abc1;
                                                        }
                                                 }
                                                 else if (abc1 == 4.0)
                                                 {
                                                        if (L == 4.0)
                                                        {
                                                               L1 = abc1;
                                                        }
                                                        else
                                                        {
                                                               L = abc1;
                                                        }
                                                 }
                                                 else if (abc1 == 3.5)
                                                 {
                                                        if (M == 3.5)
                                                        {
                                                               M1 = abc1;
                                                        }
                                                        else
                                                        {
                                                               M = abc1;
                                                        }
                                                 }
                                                 else if (abc1 == 3.0)
                                                 {
                                                        if (N == 3.0)
                                                        {
                                                               N1 = abc1;
                                                        }
                                                        else
                                                        {
                                                               N1 = abc1;
                                                        }
                                                 }
                                                 else if (abc1 == 2.5)
                                                 {
                                                        if (O == 2.5)
                                                        {
                                                               O1 = abc1;
                                                        }
                                                        else
                                                        {
                                                               O = abc1;
                                                        }
                                                 }
                                                 else if (abc1 == 2.0)
                                                 {
                                                        if (P == 2.0)
                                                        {
                                                               P1 = abc1;
                                                        }
                                                        else
                                                        {
                                                               P = abc1;
                                                        }
                                                 }

                                                 else if (abc1 == 1.5)
                                                 {
                                                        if (Q == 1.5)
                                                        {
                                                               Q1 = abc1;
                                                        }
                                                        else
                                                        {
                                                               Q = abc1;
                                                        }
                                                 }
                                                 else if (abc1 == 1.0)
                                                 {
                                                        if (R == 1.0)
                                                        {
                                                               R1 = abc1;
                                                        }
                                                        else
                                                        {
                                                               R = abc1;
                                                        }
                                                 }
                                                 else if (abc1 == 0.5)
                                                 {
                                                        if (S == 0.5)
                                                        {
                                                               S1 = abc1;
                                                        }
                                                        else
                                                        {
                                                               S = abc1;
                                                        }
                                                 }

                                          }

                                   }
                                   if (aa1 != 1)
                                   {
                                          if ((A + B) >= 10 || (A + C) >= 10 || (A + D) >= 10 || (A + E) >= 10 || (A + F) >= 10 || (A + G) >= 10 || (A + H) >= 10 || (A + I) >= 10 || (A + J) >= 10 || (A + K) >= 10 || (A + L) >= 10 || (A + M) >= 10 || (A + N) >= 10 || (A + O) >= 10 || (A + P) >= 10 || (A + Q) >= 10 || (A + R) >= 10 || (A + S) >= 10 || (A + A1) >= 10 || (A + B1) >= 10 || (A + C1) >= 10 || (A + D1) >= 10 || (A + E1) >= 10 || (A + F1) >= 10 || (A + G1) >= 10 || (A + H1) >= 10 || (A + I1) >= 10 || (A + J1) >= 10 || (A + K1) >= 10 || (A + L1) >= 10 || (A + M1) >= 10 || (A + N1) >= 10 || (A + O1) >= 10 || (A + P1) >= 10 || (A + Q1) >= 10 || (A + R1) >= 10 || (A + S1) >= 10 ||
                                              (B + C) >= 10 || (B + D) >= 10 || (B + E) >= 10 || (B + F) >= 10 || (B + G) >= 10 || (B + H) >= 10 || (B + I) >= 10 || (B + J) >= 10 || (B + K) >= 10 || (B + L) >= 10 || (B + M) >= 10 || (B + N) >= 10 || (B + O) >= 10 || (B + P) >= 10 || (B + Q) >= 10 || (B + R) >= 10 || (B + S) >= 10 || (B + A1) >= 10 || (B + B1) >= 10 || (B + C1) >= 10 || (B + D1) >= 10 || (B + E1) >= 10 || (B + F1) >= 10 || (B + G1) >= 10 || (B + H1) >= 10 || (B + I1) >= 10 || (B + J1) >= 10 || (B + K1) >= 10 || (B + L1) >= 10 || (B + M1) >= 10 || (B + N1) >= 10 || (B + O1) >= 10 || (B + P1) >= 10 || (B + Q1) >= 10 || (B + R1) >= 10 || (B + S1) >= 10 ||
                                              (C + D) >= 10 || (C + E) >= 10 || (C + F) >= 10 || (C + F) >= 10 || (C + G) >= 10 || (C + H) >= 10 || (C + I) >= 10 || (C + J) >= 10 || (C + K) >= 10 || (C + L) >= 10 || (C + M) >= 10 || (C + N) >= 10 || (C + O) >= 10 || (C + P) >= 10 || (C + Q) >= 10 || (C + R) >= 10 || (C + S) >= 10 || (C + A1) >= 10 || (C + B1) >= 10 || (C + C1) >= 10 || (C + D1) >= 10 || (C + E1) >= 10 || (C + F1) >= 10 || (C + G1) >= 10 || (C + H1) >= 10 || (C + I1) >= 10 || (C + J1) >= 10 || (C + K1) >= 10 || (C + L1) >= 10 || (C + M1) >= 10 || (C + N1) >= 10 || (C + O1) >= 10 || (C + P1) >= 10 || (C + Q1) >= 10 || (C + R1) >= 10 || (C + S1) >= 10 ||
                                              (D + E) >= 10 || (D + F) >= 10 || (D + G) >= 10 || (D + H) >= 10 || (D + I) >= 10 || (D + J) >= 10 || (D + K) >= 10 || (D + L) >= 10 || (D + M) >= 10 || (D + N) >= 10 || (D + O) >= 10 || (D + P) >= 10 || (D + Q) >= 10 || (D + R) >= 10 || (D + S) >= 10 || (D + A1) >= 10 || (D + B1) >= 10 || (D + C1) >= 10 || (D + D1) >= 10 || (D + E1) >= 10 || (D + F1) >= 10 || (D + G1) >= 10 || (D + H1) >= 10 || (D + I1) >= 10 || (D + J1) >= 10 || (D + K1) >= 10 || (D + L1) >= 10 || (D + M1) >= 10 || (D + N1) >= 10 || (D + O1) >= 10 || (D + P1) >= 10 || (D + Q1) >= 10 || (D + R1) >= 10 || (D + S1) >= 10 ||
                                              (E + F) >= 10 || (E + G) >= 10 || (E + H) >= 10 || (E + I) >= 10 || (E + J) >= 10 || (E + K) >= 10 || (E + L) >= 10 || (E + M) >= 10 || (E + N) >= 10 || (E + O) >= 10 || (E + P) >= 10 || (E + Q) >= 10 || (E + R) >= 10 || (E + S) >= 10 || (E + A1) >= 10 || (E + B1) >= 10 || (E + C1) >= 10 || (E + D1) >= 10 || (E + E1) >= 10 || (E + F1) >= 10 || (E + G1) >= 10 || (E + H1) >= 10 || (E + I1) >= 10 || (E + J1) >= 10 || (E + K1) >= 10 || (E + L1) >= 10 || (E + M1) >= 10 || (E + N1) >= 10 || (E + O1) >= 10 || (E + P1) >= 10 || (E + Q1) >= 10 || (E + R1) >= 10 || (E + S1) >= 10 ||
                                              (F + G) >= 10 || (F + H) >= 10 || (F + I) >= 10 || (F + J) >= 10 || (F + K) >= 10 || (F + L) >= 10 || (F + M) >= 10 || (F + N) >= 10 || (F + O) >= 10 || (F + P) >= 10 || (F + Q) >= 10 || (F + R) >= 10 || (F + S) >= 10 || (F + A1) >= 10 || (F + B1) >= 10 || (F + C1) >= 10 || (F + D1) >= 10 || (F + E1) >= 10 || (F + F1) >= 10 || (F + G1) >= 10 || (F + H1) >= 10 || (F + I1) >= 10 || (F + J1) >= 10 || (F + K1) >= 10 || (F + L1) >= 10 || (F + M1) >= 10 || (F + N1) >= 10 || (F + O1) >= 10 || (F + P1) >= 10 || (F + Q1) >= 10 || (F + R1) >= 10 || (F + S1) >= 10 ||
                                              (G + H) >= 10 || (G + I) >= 10 || (G + J) >= 10 || (G + K) >= 10 || (G + L) >= 10 || (G + M) >= 10 || (G + N) >= 10 || (G + O) >= 10 || (G + P) >= 10 || (G + Q) >= 10 || (G + R) >= 10 || (G + S) >= 10 || (G + A1) >= 10 || (G + B1) >= 10 || (G + C1) >= 10 || (G + D1) >= 10 || (G + E1) >= 10 || (G + F1) >= 10 || (G + G1) >= 10 || (G + H1) >= 10 || (G + I1) >= 10 || (G + J1) >= 10 || (G + K1) >= 10 || (G + L1) >= 10 || (G + M1) >= 10 || (G + N1) >= 10 || (G + O1) >= 10 || (G + P1) >= 10 || (G + Q1) >= 10 || (G + R1) >= 10 || (G + S1) >= 10 ||
                                              (H + I) >= 10 || (H + J) >= 10 || (H + K) >= 10 || (H + L) >= 10 || (H + M) >= 10 || (H + N) >= 10 || (H + O) >= 10 || (H + P) >= 10 || (H + Q) >= 10 || (H + R) >= 10 || (H + S) >= 10 || (H + A1) >= 10 || (H + B1) >= 10 || (H + C1) >= 10 || (H + D1) >= 10 || (H + E1) >= 10 || (H + F1) >= 10 || (H + G1) >= 10 || (H + H1) >= 10 || (H + I1) >= 10 || (H + J1) >= 10 || (H + K1) >= 10 || (H + L1) >= 10 || (H + M1) >= 10 || (H + N1) >= 10 || (H + O1) >= 10 || (H + P1) >= 10 || (H + Q1) >= 10 || (H + R1) >= 10 || (H + S1) >= 10 ||
                                              (I + J) >= 10 || (I + K) >= 10 || (I + L) >= 10 || (I + M) >= 10 || (I + N) >= 10 || (I + O) >= 10 || (I + P) >= 10 || (I + Q) >= 10 || (I + R) >= 10 || (I + S) >= 10 || (I + A1) >= 10 || (I + B1) >= 10 || (I + C1) >= 10 || (I + D1) >= 10 || (I + E1) >= 10 || (I + F1) >= 10 || (I + G1) >= 10 || (I + H1) >= 10 || (I + I1) >= 10 || (I + J1) >= 10 || (I + K1) >= 10 || (I + L1) >= 10 || (I + M1) >= 10 || (I + N1) >= 10 || (I + O1) >= 10 || (I + P1) >= 10 || (I + Q1) >= 10 || (I + R1) >= 10 || (I + S1) >= 10 ||
                                              (J + K) >= 10 || (J + L) >= 10 || (J + M) >= 10 || (J + N) >= 10 || (J + O) >= 10 || (J + P) >= 10 || (J + Q) >= 10 || (J + R) >= 10 || (J + S) >= 10 || (J + A1) >= 10 || (J + B1) >= 10 || (J + C1) >= 10 || (J + D1) >= 10 || (J + E1) >= 10 || (J + F1) >= 10 || (J + G1) >= 10 || (J + H1) >= 10 || (J + I1) >= 10 || (J + J1) >= 10 || (J + K1) >= 10 || (J + L1) >= 10 || (J + M1) >= 10 || (J + N1) >= 10 || (J + O1) >= 10 || (J + P1) >= 10 || (J + Q1) >= 10 || (J + R1) >= 10 || (J + S1) >= 10 ||
                                              (K + L) >= 10 || (K + M) >= 10 || (K + N) >= 10 || (K + O) >= 10 || (K + P) >= 10 || (K + Q) >= 10 || (K + R) >= 10 || (K + S) >= 10 || (K + A1) >= 10 || (K + B1) >= 10 || (K + C1) >= 10 || (K + D1) >= 10 || (K + E1) >= 10 || (K + F1) >= 10 || (K + G1) >= 10 || (K + H1) >= 10 || (K + I1) >= 10 || (K + J1) >= 10 || (K + K1) >= 10 || (K + L1) >= 10 || (K + M1) >= 10 || (K + N1) >= 10 || (K + O1) >= 10 || (K + P1) >= 10 || (K + Q1) >= 10 || (K + R1) >= 10 || (K + S1) >= 10 ||
                                              (L + M) >= 10 || (L + N) >= 10 || (L + O) >= 10 || (L + P) >= 10 || (L + Q) >= 10 || (L + R) >= 10 || (L + S) >= 10 || (L + A1) >= 10 || (L + B1) >= 10 || (L + C1) >= 10 || (L + D1) >= 10 || (L + E1) >= 10 || (L + F1) >= 10 || (L + G1) >= 10 || (L + H1) >= 10 || (L + I1) >= 10 || (L + J1) >= 10 || (L + K1) >= 10 || (L + L1) >= 10 || (L + M1) >= 10 || (L + N1) >= 10 || (L + O1) >= 10 || (L + P1) >= 10 || (L + Q1) >= 10 || (L + R1) >= 10 || (L + S1) >= 10 ||
                                              (M + N) >= 10 || (M + O) >= 10 || (M + P) >= 10 || (M + Q) >= 10 || (M + R) >= 10 || (M + S) >= 10 || (M + A1) >= 10 || (M + B1) >= 10 || (M + C1) >= 10 || (M + D1) >= 10 || (M + E1) >= 10 || (M + F1) >= 10 || (M + G1) >= 10 || (M + H1) >= 10 || (M + I1) >= 10 || (M + J1) >= 10 || (M + K1) >= 10 || (M + L1) >= 10 || (M + M1) >= 10 || (M + N1) >= 10 || (M + O1) >= 10 || (M + P1) >= 10 || (M + Q1) >= 10 || (M + R1) >= 10 || (M + S1) >= 10 ||
                                              (N + O) >= 10 || (N + P) >= 10 || (N + Q) >= 10 || (N + R) >= 10 || (N + S) >= 10 || (N + A1) >= 10 || (N + B1) >= 10 || (N + C1) >= 10 || (N + D1) >= 10 || (N + E1) >= 10 || (N + F1) >= 10 || (N + G1) >= 10 || (N + H1) >= 10 || (N + I1) >= 10 || (N + J1) >= 10 || (N + K1) >= 10 || (N + L1) >= 10 || (N + M1) >= 10 || (N + N1) >= 10 || (N + O1) >= 10 || (N + P1) >= 10 || (N + Q1) >= 10 || (N + R1) >= 10 || (N + S1) >= 10 ||
                                              (O + P) >= 10 || (O + Q) >= 10 || (O + R) >= 10 || (O + S) >= 10 || (O + A1) >= 10 || (O + B1) >= 10 || (O + C1) >= 10 || (O + D1) >= 10 || (O + E1) >= 10 || (O + F1) >= 10 || (O + G1) >= 10 || (O + H1) >= 10 || (O + I1) >= 10 || (O + J1) >= 10 || (O + K1) >= 10 || (O + L1) >= 10 || (O + M1) >= 10 || (O + N1) >= 10 || (O + O1) >= 10 || (O + P1) >= 10 || (O + Q1) >= 10 || (O + R1) >= 10 || (O + S1) >= 10 ||
                                              (P + Q) >= 10 || (P + R) >= 10 || (P + S) >= 10 || (P + A1) >= 10 || (P + B1) >= 10 || (P + C1) >= 10 || (P + D1) >= 10 || (P + E1) >= 10 || (P + F1) >= 10 || (P + G1) >= 10 || (P + H1) >= 10 || (P + I1) >= 10 || (P + J1) >= 10 || (P + K1) >= 10 || (P + L1) >= 10 || (P + M1) >= 10 || (P + N1) >= 10 || (P + O1) >= 10 || (P + P1) >= 10 || (P + Q1) >= 10 || (P + R1) >= 10 || (P + S1) >= 10 ||
                                              (Q + R) >= 10 || (Q + S) >= 10 || (Q + A1) >= 10 || (Q + B1) >= 10 || (Q + C1) >= 10 || (Q + D1) >= 10 || (Q + E1) >= 10 || (Q + F1) >= 10 || (Q + G1) >= 10 || (Q + H1) >= 10 || (Q + I1) >= 10 || (Q + J1) >= 10 || (Q + K1) >= 10 || (Q + L1) >= 10 || (Q + M1) >= 10 || (Q + N1) >= 10 || (Q + O1) >= 10 || (Q + P1) >= 10 || (Q + Q1) >= 10 || (Q + R1) >= 10 || (Q + S1) >= 10 ||
                                              (R + S) >= 10 || (R + A1) >= 10 || (R + B1) >= 10 || (R + C1) >= 10 || (R + D1) >= 10 || (R + E1) >= 10 || (R + F1) >= 10 || (R + G1) >= 10 || (R + H1) >= 10 || (R + I1) >= 10 || (R + J1) >= 10 || (R + K1) >= 10 || (R + L1) >= 10 || (R + M1) >= 10 || (R + N1) >= 10 || (R + O1) >= 10 || (R + P1) >= 10 || (R + Q1) >= 10 || (R + R1) >= 10 || (R + S1) >= 10 ||
                                              (S + A1) >= 10 || (S + B1) >= 10 || (S + C1) >= 10 || (S + D1) >= 10 || (S + E1) >= 10 || (S + F1) >= 10 || (S + G1) >= 10 || (S + H1) >= 10 || (S + I1) >= 10 || (S + J1) >= 10 || (S + K1) >= 10 || (S + L1) >= 10 || (S + M1) >= 10 || (S + N1) >= 10 || (S + O1) >= 10 || (S + P1) >= 10 || (S + Q1) >= 10 || (S + R1) >= 10 || (S + S1) >= 10 ||
                                              (A1 + B1) >= 10 || (A1 + C1) >= 10 || (A1 + D1) >= 10 || (A1 + E1) >= 10 || (A1 + F1) >= 10 || (A1 + G1) >= 10 || (A1 + H1) >= 10 || (A1 + I1) >= 10 || (A1 + J1) >= 10 || (A1 + K1) >= 10 || (A1 + L1) >= 10 || (A1 + M1) >= 10 || (A1 + N1) >= 10 || (A1 + O1) >= 10 || (A1 + P1) >= 10 || (A1 + Q1) >= 10 || (A1 + R1) >= 10 || (A1 + S1) >= 10 ||
                                              (B1 + C1) >= 10 || (B1 + D1) >= 10 || (B1 + E1) >= 10 || (B1 + F1) >= 10 || (B1 + G1) >= 10 || (B1 + H1) >= 10 || (B1 + I1) >= 10 || (B1 + J1) >= 10 || (B1 + K1) >= 10 || (B1 + L1) >= 10 || (B1 + M1) >= 10 || (B1 + N1) >= 10 || (B1 + O1) >= 10 || (B1 + P1) >= 10 || (B1 + Q1) >= 10 || (B1 + R1) >= 10 || (B1 + S1) >= 10 ||
                                              (C1 + D1) >= 10 || (C1 + E1) >= 10 || (C1 + F1) >= 10 || (C1 + G1) >= 10 || (C1 + H1) >= 10 || (C1 + I1) >= 10 || (C1 + J1) >= 10 || (C1 + K1) >= 10 || (C1 + L1) >= 10 || (C1 + M1) >= 10 || (C1 + N1) >= 10 || (C1 + O1) >= 10 || (C1 + P1) >= 10 || (C1 + Q1) >= 10 || (C1 + R1) >= 10 || (C1 + S1) >= 10 ||
                                              (D1 + E1) >= 10 || (D1 + F1) >= 10 || (D1 + G1) >= 10 || (D1 + H1) >= 10 || (D1 + I1) >= 10 || (D1 + J1) >= 10 || (D1 + K1) >= 10 || (D1 + L1) >= 10 || (D1 + M1) >= 10 || (D1 + N1) >= 10 || (D1 + O1) >= 10 || (D1 + P1) >= 10 || (D1 + Q1) >= 10 || (D1 + R1) >= 10 || (D1 + S1) >= 10 ||
                                              (E1 + F1) >= 10 || (E1 + G1) >= 10 || (E1 + H1) >= 10 || (E1 + I1) >= 10 || (E1 + J1) >= 10 || (E1 + K1) >= 10 || (E1 + L1) >= 10 || (E1 + M1) >= 10 || (E1 + N1) >= 10 || (E1 + O1) >= 10 || (E1 + P1) >= 10 || (E1 + Q1) >= 10 || (E1 + R1) >= 10 || (E1 + S1) >= 10 ||
                                              (F1 + G1) >= 10 || (F1 + H1) >= 10 || (F1 + I1) >= 10 || (F1 + J1) >= 10 || (F1 + K1) >= 10 || (F1 + L1) >= 10 || (F1 + M1) >= 10 || (F1 + N1) >= 10 || (F1 + O1) >= 10 || (F1 + P1) >= 10 || (F1 + Q1) >= 10 || (F1 + R1) >= 10 || (F1 + S1) >= 10 ||
                                              (G1 + H1) >= 10 || (G1 + I1) >= 10 || (G1 + J1) >= 10 || (G1 + K1) >= 10 || (G1 + L1) >= 10 || (G1 + M1) >= 10 || (G1 + N1) >= 10 || (G1 + O1) >= 10 || (G1 + P1) >= 10 || (G1 + Q1) >= 10 || (G1 + R1) >= 10 || (G1 + S1) >= 10 ||
                                              (H1 + I1) >= 10 || (H1 + J1) >= 10 || (H1 + K1) >= 10 || (H1 + L1) >= 10 || (H1 + M1) >= 10 || (H1 + N1) >= 10 || (H1 + O1) >= 10 || (H1 + P1) >= 10 || (H1 + Q1) >= 10 || (H1 + R1) >= 10 || (H1 + S1) >= 10 ||
                                              (I1 + J1) >= 10 || (I1 + K1) >= 10 || (I1 + L1) >= 10 || (I1 + M1) >= 10 || (I1 + N1) >= 10 || (I1 + O1) >= 10 || (I1 + P1) >= 10 || (I1 + Q1) >= 10 || (I1 + R1) >= 10 || (I1 + S1) >= 10 ||
                                              (J1 + K1) >= 10 || (J1 + L1) >= 10 || (J1 + M1) >= 10 || (J1 + N1) >= 10 || (J1 + O1) >= 10 || (J1 + P1) >= 10 || (J1 + Q1) >= 10 || (J1 + R1) >= 10 || (J1 + S1) >= 10 ||
                                              (K1 + L1) >= 10 || (K1 + M1) >= 10 || (K1 + N1) >= 10 || (K1 + O1) >= 10 || (K1 + P1) >= 10 || (K1 + Q1) >= 10 || (K1 + R1) >= 10 || (K1 + S1) >= 10 ||
                                              (L1 + M1) >= 10 || (L1 + N1) >= 10 || (L1 + O1) >= 10 || (L1 + P1) >= 10 || (L1 + Q1) >= 10 || (L1 + R1) >= 10 || (L1 + S1) >= 10 ||
                                              (M1 + N1) >= 10 || (M1 + O1) >= 10 || (M1 + P1) >= 10 || (M1 + Q1) >= 10 || (M1 + R1) >= 10 || (M1 + S1) >= 10 ||
                                              (N1 + O1) >= 10 || (N1 + P1) >= 10 || (N1 + Q1) >= 10 || (N1 + R1) >= 10 || (N1 + S1) >= 10 ||
                                              (O1 + P1) >= 10 || (O1 + Q1) >= 10 || (O1 + R1) >= 10 || (O1 + S1) >= 10 ||
                                              (P1 + Q1) >= 10 || (P1 + R1) >= 10 || (P1 + S1) >= 10 ||
                                              (Q1 + R1) >= 10 || (Q1 + S1) >= 10 ||
                                              (R1 + S1) >= 10)
                                          {

                                          }
                                          else
                                          {
                                                 nc10hrs = "Minimum total rest comprises more than 2 period";

                                          }
                                   }
                            }


                     }

                     if (!string.IsNullOrEmpty(nc6hrs))
                     {
                            ncs.Append(nc6hrs);
                            ncs.Append("; ");
                     }
                     if (!string.IsNullOrEmpty(nc10hrs))
                     {
                            ncs.Append(nc10hrs);
                            ncs.Append("; ");
                     }

                     if (result1 > 14)
                     {
                            ncs.Append("10 Hours of rest is must in any 24 Hours");
                            ncs.Append("; ");
                     }
                     if (result1 > 13)
                     {
                            ncs.Append("Less than 77 hours of rest in 7 day period");
                            ncs.Append("; ");
                     }

                     if (ChkOPA90.IsChecked == true)
                     {
                            if (result1 > 15)
                            {
                                   ncs.Append("More than 15 hours work in any 24 hour period  in OPA 90");
                                   ncs.Append("; ");
                            }
                            if (result1 > 12)
                            {
                                   ncs.Append("More than 36 hours work in any 72 hour period in OPA 90");
                                   ncs.Append("; ");
                            }
                     }


                     string ss = ncs.ToString().TrimEnd(';');

                     return Tuple.Create(result1.ToString(), ss.TrimEnd(' ').TrimEnd(';'));

              }

              private void ChkOPA90_Checked(object sender, RoutedEventArgs e)
              {
                     try
                     {
                            var checkcondition = CheckNormalConditions(lblNWkSea.Text);
                            TxtWorkHrsNon.Text = checkcondition.Item1;
                            NCWatchKeeping.Text = checkcondition.Item2;

                            //...........................

                            var checkconditionPort = CheckNormalConditions(lblNWkPort.Text);
                            TxtWorkHrsPort.Text = checkconditionPort.Item1;
                            NCWatchKeepingPort.Text = checkconditionPort.Item2;
                     }
                     catch (Exception ex)
                     {
                            sc.ErrorLog(ex);
                     }

              }

              private void ChkOPA90_Unchecked(object sender, RoutedEventArgs e)
              {
                     try
                     {
                            var checkcondition = CheckNormalConditions(lblNWkSea.Text);
                            TxtWorkHrsNon.Text = checkcondition.Item1;
                            NCWatchKeeping.Text = checkcondition.Item2;


                            //.....................

                            var checkconditionPort = CheckNormalConditions(lblNWkPort.Text);
                            TxtWorkHrsPort.Text = checkconditionPort.Item1;
                            NCWatchKeepingPort.Text = checkconditionPort.Item2;
                     }
                     catch (Exception ex)
                     {
                            sc.ErrorLog(ex);
                     }

              }
       }
}
