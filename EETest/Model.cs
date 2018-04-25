using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;

namespace EETest
{
    public class Patient
    {
        public int Id { get; set; }


        public string Name { get; set; }

        public DateTime CreateDate { get; set; }

        public virtual ICollection<LabResult> LabResults { get; set; }
    }


    public class LabResult
    {
        public int Id { get; set; }
        public string Result { get; set; }
    }

    public class HospitalContext : DbContext
    {
        public DbSet<Patient> Patients { get; set; }
        public DbSet<LabResult> LabResults { get; set; }

        public void test()
        {
            System.Data.Entity.DbModelBuilder d = new DbModelBuilder();
            d.Entity<Patient>().HasKey(e => new { e.Id, e.Name });
            d.Entity<Patient>().Property(e => e.Id);

        }
    }





}
