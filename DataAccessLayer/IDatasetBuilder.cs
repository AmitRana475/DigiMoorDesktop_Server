﻿using System.Data;

namespace DataAccessLayer
{
       public interface IDatasetBuilder
    {
        DataSet GetAddGroup();
        DataSet GetAdminLogin();
        DataSet GetcertificateOrder();
        DataSet GetCertificateNotification();
        DataSet Getcertificates();
        DataSet GetcommentofVS();
        DataSet GetCrewDetail();
        DataSet GetCrewRank();
        DataSet GetCurrency();
        DataSet GetDefineRules();
        DataSet GetDepartment();

        DataSet GetFreeze();
        DataSet GetGroupPlanner();
        DataSet GetHoliDayGroupName();
        DataSet GetHolidays();
        DataSet GetInfoLog();
        DataSet GetInternational();
        DataSet GetLogbook();
        DataSet GetOPAStartStop();
        DataSet GetOverTime();
        DataSet GetRetardtblClass();
        DataSet GetRuffCode();
        DataSet GetRuleTable();
        DataSet GetUserAccess();
        DataSet Getversion_tbl();
        DataSet GetVessel();
        DataSet GetWorkHours();
        DataSet GetWorkHoursPlanner();


    }
}
