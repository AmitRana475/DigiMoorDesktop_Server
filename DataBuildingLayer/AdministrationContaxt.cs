using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DataBuildingLayer
{
    public class AdministrationContaxt : ShipmentContaxt
    {

        //public DbSet<UserAccessClass> UserAccessHOD { get; set; }
        //public DbSet<CommentofVSClass> CommentofVs { get; set; }
        //public DbSet<CertificatesClass> Certificates { get; set; }
        //public DbSet<CrewDetailClass> CrewDetails { get; set; }
        //public DbSet<WorkHoursClass> sc.WorkHourss { get; set; }
        //public DbSet<RuffCodeClass> RuffCodes { get; set; }
        //public DbSet<versionClass> Versions { get; set; }
        //public DbSet<VesselClass> Vessels { get; set; }



        public List<RuffCodeClass> GetLicencekeys()
        {
            try
            {
                List<RuffCodeClass> objHashTable = new List<RuffCodeClass>();
                var data = RuffCodes.ToList();

                foreach (var item in data)
                {

                    objHashTable.Add(new RuffCodeClass { keyno = Decrypt(item.keyno.ToString(), "KKPrajapat"), keycode = Decrypt(item.keycode.ToString(), "KKPrajapat") });

                }


                return objHashTable;
            }
            catch (Exception ex)
            {
                ErrorLog(ex);
                return null;
            }
        }


     



    }
}
