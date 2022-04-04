using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBuildingLayer
{
       public interface IRepository<T> where T : class
       {
              IEnumerable<T> GetList();
              T GetListById(int Id);
              void InsertEntity(T model);
              void UpdateEntity(T model);
              void DeleteEntity(T model);
              void Save();

       }
}
