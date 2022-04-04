using System.Data;

namespace DataAccessLayer
{
       public class DatasetBuilder : IDatasetBuilder
    {
        private static DBManager dm;
        public DatasetBuilder()
        {
            dm = new DBManager();
        }

        public DataSet GetAddGroup()
        {
            dm.Open();
            var data = dm.ExecuteDataSet(CommandType.StoredProcedure, "SPGetAddGroup");
            dm.Close();

            return data;

        }

        public DataSet GetAdminLogin()
        {
            dm.Open();
            var data = dm.ExecuteDataSet(CommandType.StoredProcedure, "SPGetAdminLogin");
            dm.Close();

            return data;
        }

        public DataSet GetCertificateNotification()
        {
            dm.Open();
            var data = dm.ExecuteDataSet(CommandType.StoredProcedure, "SPGetCertificateNotification");
            dm.Close();

            return data;
        }

        public DataSet Getcertificates()
        {
            dm.Open();
            var data = dm.ExecuteDataSet(CommandType.StoredProcedure, "SPGetcertificates");
            dm.Close();

            return data;
        }

        public DataSet GetcertificateOrder()
        {
            dm.Open();
            var data = dm.ExecuteDataSet(CommandType.StoredProcedure, "SPcertificate_order");
            dm.Close();

            return data;
        }

        public DataSet GetcommentofVS()
        {
            dm.Open();
            var data = dm.ExecuteDataSet(CommandType.StoredProcedure, "SPGetcomment_ofVSAddGroup");
            dm.Close();

            return data;
        }

        public DataSet GetCrewDetail()
        {
            dm.Open();
            var data = dm.ExecuteDataSet(CommandType.StoredProcedure, "SPGetCrewDetail");
            dm.Close();

            return data;
        }

        public DataSet GetCrewRank()
        {
            dm.Open();
            var data = dm.ExecuteDataSet(CommandType.StoredProcedure, "SPGetCrewRank");
            dm.Close();

            return data;
        }

        public DataSet GetCurrency()
        {
            dm.Open();
            var data = dm.ExecuteDataSet(CommandType.StoredProcedure, "SPGetCurrency");
            dm.Close();

            return data;
        }

        public DataSet GetDefineRules()
        {
            dm.Open();
            var data = dm.ExecuteDataSet(CommandType.StoredProcedure, "SPGetDefineRules");
            dm.Close();

            return data;
        }

        public DataSet GetDepartment()
        {
            dm.Open();
            var data = dm.ExecuteDataSet(CommandType.StoredProcedure, "SPGetDepartment");
            dm.Close();

            return data;
        }

        public DataSet GetFreeze()
        {
            dm.Open();
            var data = dm.ExecuteDataSet(CommandType.StoredProcedure, "SPGetFreeze");
            dm.Close();

            return data;
        }

        public DataSet GetGroupPlanner()
        {
            dm.Open();
            var data = dm.ExecuteDataSet(CommandType.StoredProcedure, "SPGetGroupPlanner");
            dm.Close();

            return data;
        }

        public DataSet GetHolidays()
        {
            dm.Open();
            var data = dm.ExecuteDataSet(CommandType.StoredProcedure, "SPGetHolidays");
            dm.Close();

            return data;
        }

        public DataSet GetInfoLog()
        {
            dm.Open();
            var data = dm.ExecuteDataSet(CommandType.StoredProcedure, "SPGetInfoLog");
            dm.Close();

            return data;
        }

        public DataSet GetInternational()
        {
            dm.Open();
            var data = dm.ExecuteDataSet(CommandType.StoredProcedure, "SPGetInternational");
            dm.Close();

            return data;
        }

        public DataSet GetLogbook()
        {
            dm.Open();
            var data = dm.ExecuteDataSet(CommandType.StoredProcedure, "SPGetLogbook");
            dm.Close();

            return data;
        }

        public DataSet GetOPAStartStop()
        {
            dm.Open();
            var data = dm.ExecuteDataSet(CommandType.StoredProcedure, "SPGetOPAStartStop");
            dm.Close();

            return data;
        }

        public DataSet GetOverTime()
        {
            dm.Open();
            var data = dm.ExecuteDataSet(CommandType.StoredProcedure, "SPGetOverTime");
            dm.Close();

            return data;
        }

        public DataSet GetRetardtblClass()
        {
            dm.Open();
            var data = dm.ExecuteDataSet(CommandType.StoredProcedure, "SPGetRetardtblClass");
            dm.Close();

            return data;
        }

        public DataSet GetRuffCode()
        {
            dm.Open();
            var data = dm.ExecuteDataSet(CommandType.StoredProcedure, "SPGetRuffCode");
            dm.Close();

            return data;
        }

        public DataSet GetRuleTable()
        {
            dm.Open();
            var data = dm.ExecuteDataSet(CommandType.StoredProcedure, "SPGetRuleTable");
            dm.Close();

            return data;
        }

        public DataSet GetUserAccess()
        {
            dm.Open();
            var data = dm.ExecuteDataSet(CommandType.StoredProcedure, "SPGetUserAccess");
            dm.Close();

            return data;
        }

        public DataSet Getversion_tbl()
        {
            dm.Open();
            var data = dm.ExecuteDataSet(CommandType.StoredProcedure, "SPGetversion_tbl");
            dm.Close();

            return data;
        }

        public DataSet GetVessel()
        {
            dm.Open();
            var data = dm.ExecuteDataSet(CommandType.StoredProcedure, "SPGetVessel");
            dm.Close();

            return data;
        }

        public DataSet GetWorkHours()
        {
            dm.Open();
            var data = dm.ExecuteDataSet(CommandType.StoredProcedure, "SPGetWorkHours");
            dm.Close();

            return data;
        }

        public DataSet GetWorkHoursPlanner()
        {
            dm.Open();
            var data = dm.ExecuteDataSet(CommandType.StoredProcedure, "SPGetWorkHoursPlanner");
            dm.Close();

            return data;
        }

        public DataSet GetHoliDayGroupName()
        {
            dm.Open();
            var data = dm.ExecuteDataSet(CommandType.StoredProcedure, "SPSPGetHoliDayGroupName");
            dm.Close();

            return data;
        }
    }
}
