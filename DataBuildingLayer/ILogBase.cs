using System;
using System.Collections.Generic;

namespace DataBuildingLayer
{
    public interface ILogBase
    {
        void CreateLog(string modulename, string actionname, DateTime? edate);
        void ErrorLog(Exception ex);
      
        System.Data.DataTable LINQResultToDataTable<T>(IEnumerable<T> Linqlist);
    }
}
