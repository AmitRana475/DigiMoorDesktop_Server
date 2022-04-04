using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DataBuildingLayer
{
       public class Repository<T> : IDisposable, IRepository<T> where T : class
       {
              private ShipmentContaxt contaxt;
              private DbSet<T> entity;

              public Repository()
              {
                     contaxt = new ShipmentContaxt();
                     entity = contaxt.Set<T>();

              }

              public IEnumerable<T> GetList()
              {
                     return entity.ToList();
              }


              public T GetListById(int Id)
              {
                     return entity.Find(Id);
              }

              public void InsertEntity(T model)
              {
                     entity.Add(model);
              }


              public void UpdateEntity(T model)
              {
                     contaxt.Entry(model).State = EntityState.Modified;
              }

              public void DeleteEntity(T model)
              {
                     entity.Remove(model);
              }

              public void Save()
              {
                     contaxt.SaveChanges();
              }


              public void Dispose()
              {
                     contaxt.Dispose();
              }
       }
}
