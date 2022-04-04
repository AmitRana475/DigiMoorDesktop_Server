using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace DataAccessLayer
{
    public static class DataSetToListConverter
    {
        public static List<T> ConvertDataTable<T>(DataTable dt)
        {
            List<T> data = new List<T>();
            foreach (DataRow row in dt.Rows)
            {
                T item = GetItem<T>(row);
                data.Add(item);
            }
            return data;
        }
        public static T GetItem<T>(DataRow dr)
        {
            Type temp = typeof(T);
            T obj = Activator.CreateInstance<T>();

            foreach (DataColumn column in dr.Table.Columns)
            {
                foreach (PropertyInfo pro in temp.GetProperties())
                {
                    if (pro.Name == column.ColumnName)
                        pro.SetValue(obj, dr[column.ColumnName], null);
                    else
                        continue;
                }
            }
            return obj;
        }

        //public static List<T> ToList<T>(this DataTable table) where T : new()
        //{
        //    IList<PropertyInfo> properties = typeof(T).GetProperties().ToList();
        //    List<T> result = new List<T>();

        //    foreach (var row in table.Rows)
        //    {
        //        var item = CreateItemFromRow<T>((DataRow)row, properties);
        //        result.Add(item);
        //    }

        //    return result;
        //}

        //private static T CreateItemFromRow<T>(DataRow row, IList<PropertyInfo> properties) where T : new()
        //{
        //    T item = new T();
        //    foreach (var property in properties)
        //    {
        //        if (property.PropertyType == typeof(System.DayOfWeek))
        //        {
        //            DayOfWeek day = (DayOfWeek)Enum.Parse(typeof(DayOfWeek), row[property.Name].ToString());
        //            property.SetValue(item, day, null);
        //        }
        //        else
        //        {
        //            if (row[property.Name] == DBNull.Value)
        //                property.SetValue(item, null, null);
        //            else
        //                property.SetValue(item, row[property.Name], null);
        //        }
        //    }
        //    return item;
        //}
    }
}
