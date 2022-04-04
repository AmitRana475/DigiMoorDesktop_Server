using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Data.SqlServerCe;
using System.IO;
using System.Net;
using System.Security;
using System.Security.Cryptography;

namespace DataBuildingLayer
{
    public class ConnectionBulder
    {

      //  public readonly static string constring = "Data Source=.; Database=DigiMoorDB_V2; uid=sa;Password=49webstreet@";
        public readonly static string constring = ConfigurationManager.ConnectionStrings["ShipmentContaxt"].ConnectionString;


        public  static SqlConnection con;//= new SqlConnection(constring);

        public static SecureString Dconstring;
        public static SecureString NoDBCon;
        public ConnectionBulder()
        {

            //Dconstring = Decrypt(constring, "KKPrajapat");
            //NoDBCon = Dconstring.Replace("Database=shipment5;", "");
            //con = new SqlConnection(Dconstring);

            //Dconstring = new NetworkCredential("", constring).SecurePassword;
            //string Teststring = new NetworkCredential("", Dconstring).Password;
            //string TeststringNoc = Teststring.Replace("Database=DigiMoorDB_V2;", "");
            //NoDBCon = new NetworkCredential("", TeststringNoc).SecurePassword;
            //con = new SqlConnection(new NetworkCredential("", Dconstring).Password);



            Dconstring = new NetworkCredential("", Decrypt(constring, "KKPrajapat")).SecurePassword;
            string Teststring = new NetworkCredential("", Dconstring).Password;
            string TeststringNoc = Teststring.Replace("Database=DigiMoorDB_V2;", "");
            TeststringNoc = TeststringNoc.TrimEnd(',');
            NoDBCon = new NetworkCredential("", TeststringNoc).SecurePassword;
            con = new SqlConnection(new NetworkCredential("", Dconstring).Password);
        }



        public string Decrypt(string strText, string strEncrypt)
        {
            byte[] bKey = new byte[20];
            byte[] IV = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };
            try
            {
                bKey = System.Text.Encoding.UTF8.GetBytes(strEncrypt.Substring(0, 8));
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                //Byte[] inputByteArray = inputByteArray = Convert.FromBase64String(strText);
                Byte[] inputByteArray = Convert.FromBase64String(strText.Replace(" ", "+"));
                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(bKey, IV), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();
                System.Text.Encoding encoding = System.Text.Encoding.UTF8;
                return encoding.GetString(ms.ToArray());
            }
            catch// (Exception ex)
            {
                // ErrorLog(ex);
                return null;
            }
        }
    }
}

