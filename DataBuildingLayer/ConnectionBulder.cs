using System.Configuration;
using System.Data.SqlClient;
using System.Data.SqlServerCe;

namespace DataBuildingLayer
{
    public class ConnectionBulder
    {
        public readonly static SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ShipmentContaxt"].ConnectionString);
    }
}
