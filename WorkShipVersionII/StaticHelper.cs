using System;
using System.IO;
using System.Media;
using System.Management;
using System.Windows.Controls;
using System.Net.NetworkInformation;

namespace WorkShipVersionII
{
  
    public enum NotificationAlertType
    {
        //Minimum21RopeTales = 0,
        Inspection7Day = 1,
        Inspection1Day = 2,
        InspectionOver = 3,

        Over_Cropping = 4,
        Max_running_hours_Approaching = 5,
        Max_running_hours_Exceeded = 6,
        Max_allowable_time_Approaching = 7,
        Max_allowable_time_Exceeded = 8,
        Rating_5Rope = 9,
        Rating_6Rope = 95,
        Rating_7Rope = 10,
        End_to_End_Approaching = 11,
        End_to_End_Overdue = 12,
        //Rotation_Approaching = 13,
        //Rotation_Overdue = 14,
        RopeDamage = 15,
        RopeSplicing = 16,
        Minimum21RopeCount = 17,
        Minimum21TailCount = 18,

        JoiningShackle_LooseEquipment7Day = 21,
        JoiningShackle_LooseEquipment1Day = 22,
        JoiningShackle_LooseEquipmentOver = 23,

        RopeStopper_FireWire_Messanger_LooseEq7Day = 24,
        RopeStopper_FireWire_Messanger_LooseEq1Day = 25,
        RopeStopper_FireWire_Messanger_LooseEqOver = 26,

        ChainStopper_LooseEq7Day = 27,
        ChainStopper_LooseEq1Day = 28,
        ChainStopper_LooseEqOver = 29,

        ChafeGuard_LooseEq7Day = 30,
        ChafeGuard_LooseEq1Day = 31,
        ChafeGuard_LooseEqOver = 32,

        WinchBreakTest_LooseEq7Day = 33,
        WinchBreakTest_LooseEq1Day = 34,
        WinchBreakTest_LooseEqOver = 35,


              //============== All LooseEq Approching Begin =============
              JoiningShackle_LooseEquipmentDisCard = 36,
              RopeStopper_FireWire_Messanger_LooseEquipmentDisCard = 37,
              ChainStopper_LooseEquipmentDisCard = 38,
              ChafeGuard_LooseEquipmentDisCard = 39,
              WinchBreakTest_LooseEquipmentDisCard = 40,

        TowingRope_LooseEquipmentDisCard = 41,
        SuezRope_LooseEquipmentDisCard = 42,
        PennantRope_LooseEquipmentDisCard = 43,
        GrommetRope_LooseEquipmentDisCard = 44,

        //============== All LooseEq Exceeded Begin {Approching ID + Exceeded ID} =============
        //JoiningShackle_ChainStopper_RopeStopper_ChafeGuard_WinchBreakTest_FireWire_Messanger
        All_LooseEquipmentExceeded = 100,

              OutofService_discarded_Rope = 41,
        OutofService_discarded_Tail = 42,

        RopeResidual_StrengthCheck = 43,

        Winch_Rotation_approching = 44,
        Winch_Rotation_exceed = 45,
      

    }

    public struct DateTimeSpan
    {
        public int Years { get; }
        public int Months { get; }
        public int Days { get; }
        public int Hours { get; }
        public int Minutes { get; }
        public int Seconds { get; }
        public int Milliseconds { get; }

        public DateTimeSpan(int years, int months, int days, int hours, int minutes, int seconds, int milliseconds)
        {
            Years = years;
            Months = months;
            Days = days;
            Hours = hours;
            Minutes = minutes;
            Seconds = seconds;
            Milliseconds = milliseconds;
        }

        enum Phase { Years, Months, Days, Done }

        public static DateTimeSpan CompareDates(DateTime date1, DateTime date2)
        {
            if (date2 < date1)
            {
                var sub = date1;
                date1 = date2;
                date2 = sub;
            }

            DateTime current = date1;
            int years = 0;
            int months = 0;
            int days = 0;

            Phase phase = Phase.Years;
            DateTimeSpan span = new DateTimeSpan();
            int officialDay = current.Day;

            while (phase != Phase.Done)
            {
                switch (phase)
                {
                    case Phase.Years:
                        if (current.AddYears(years + 1) > date2)
                        {
                            phase = Phase.Months;
                            current = current.AddYears(years);
                        }
                        else
                        {
                            years++;
                        }
                        break;
                    case Phase.Months:
                        if (current.AddMonths(months + 1) > date2)
                        {
                            phase = Phase.Days;
                            current = current.AddMonths(months);
                            if (current.Day < officialDay && officialDay <= DateTime.DaysInMonth(current.Year, current.Month))
                                current = current.AddDays(officialDay - current.Day);
                        }
                        else
                        {
                            months++;
                        }
                        break;
                    case Phase.Days:
                        if (current.AddDays(days + 1) > date2)
                        {
                            current = current.AddDays(days);
                            var timespan = date2 - current;
                            span = new DateTimeSpan(years, months, days, timespan.Hours, timespan.Minutes, timespan.Seconds, timespan.Milliseconds);
                            phase = Phase.Done;
                        }
                        else
                        {
                            days++;
                        }
                        break;
                }
            }

            return span;
        }
    }

    public static class StaticHelper
       {
        static StaticHelper()
        {
           // CPU_ProcessorID = getCPUId();

            CPU_ProcessorID = GetMACAddress();
        }
        public static string Key { get; set; } = "KKPrajapat";
        public static bool IsValidateShipDetail { get; set; } = true;

        public static string CPU_ProcessorID { get; set; }


        public static bool Wathckeeping { get; set; } = true;
              public static bool Editing { get; set; }
              public static ItemCollection ItemMenu { get; set; }
              public static string LastMenuItem { get; set; }
              public static string TabButtonMenuName { get; set; }

              public static int Id { get; set; }

              public static int ViewId { get; set; }
              public static int RopeTailId { get; set; }

              public static int? RopeTail { get; set; }
              public static int NotificationId { get; set; }
              public static int CommentsType { get; set; }
              public static int WinchId { get; set; }

        public static int MinimumRope { get; set; }
        public static int MinimumRopeTail { get; set; }
        public static int LooseEId { get; set; }

              public static string TbName { get; set; }

        public static string RNumber { get; set; }

        public static string SearchText { get; set; }

              public static DateTime InspectionDate { get; set; }

              public static string Photos { get; set; }
        public static bool AlarmCheck { get; set; } = true;
        public static string ServerName { get; set; } = "C:";

        public static string MenuTitle { get; set; }

        public static string Autoportname { get; set; }

        public static string ShipSpecData { get; set; } = "GData";

        public static int RopeId_Status { get; set; }

              public static int ContentID { get; set; }

        public static int MenuID { get; set; }
        public static int Inspectionid { get; set; }

        public static int LooseETypeId { get; set; }

        public static int RtailLooseEtypeid { get; set; }


        public static DateTime Insdate { get; set; }
        public static string InsBy { get; set; }
        public static int MooringInsID { get; set; }


        public static string GetMACAddress()        {            NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();            String sMacAddress = string.Empty;            foreach (NetworkInterface adapter in nics)            {


                if (adapter.Name == "Ethernet" || adapter.Name == "ETHERNET")// only return MAC Address from first card  
                {                    IPInterfaceProperties properties = adapter.GetIPProperties();                    sMacAddress = adapter.GetPhysicalAddress().ToString();                    break;                }

            }            if (sMacAddress == "")            {
                // string cpuInfo = string.Empty;
                ManagementClass mc = new ManagementClass("win32_processor");                ManagementObjectCollection moc = mc.GetInstances();                foreach (ManagementObject mo in moc)                {                    if (sMacAddress == "")                    {
                        //Get only the first CPU's ID
                        sMacAddress = mo.Properties["processorID"].Value.ToString();

                        break;                    }                }            }            return sMacAddress;        }

        public static string getCPUId()
        {
            string cpuInfo = string.Empty;
            ManagementClass mc = new ManagementClass("win32_processor");
            ManagementObjectCollection moc = mc.GetInstances();

            foreach (ManagementObject mo in moc)
            {
                if (cpuInfo == "")
                {
                    //Get only the first CPU's ID
                    cpuInfo = mo.Properties["processorID"].Value.ToString();
                    break;
                }
            }
            return cpuInfo;
        }

        public static string HelpFor { get; set; }

              public static void HelpMethod(string filename)
              {

                     try
                     {


                //4.9  ROPE RESIDUAL LAB TEST.htm
                //4.8  INSPECTION DISCARD CRITERIA.htm

                // StaticHelper.HelpFor = @"Notification\3.0 CREW MANAGEMENT\3.0.1 CREW DETAIL.htm";

                var path = AppDomain.CurrentDomain.BaseDirectory + @"WorkshipManual - Copy\" + "help.chm";
                            //var path = AppDomain.CurrentDomain.BaseDirectory + @"WorkshipManual - Copy\" + "DigiMoorX7.chm";
                            //var path = AppDomain.CurrentDomain.BaseDirectory + @"WorkshipManual - Copy\" + "Work-Ship Help.chm";
                            System.Windows.Forms.Help.ShowHelp(null, path, filename);
                     }
                     catch
                     {
                     }
              }

        public static int DateDiffInMonths(DateTime StartDate, DateTime Nowdate)
        {
            var dateSpan = DateTimeSpan.CompareDates(StartDate, Nowdate);
            int DifYear = dateSpan.Years < 0 ? 0 : dateSpan.Years;

            int mmmm = (DifYear * 12) + dateSpan.Months;

            return mmmm;
        }


        public static void AlarmFunction(int listcount,bool alarmcheck)
        {
            try
            {
                SoundPlayer soundplayer;
                if (listcount > 0 && alarmcheck == true)
                {
                    StaticHelper.AlarmCheck = false;
                    var Spath = Path.GetFullPath(AppDomain.CurrentDomain.BaseDirectory + "\\alarm.wav");
                    soundplayer = new SoundPlayer(Spath);
                    soundplayer.Load();
                    soundplayer.Play();
                }
            }
            catch (Exception ex) { }
        }

    }
}
