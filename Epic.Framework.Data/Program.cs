using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Epic.Data.Schema;
using System.Linq.Expressions;

namespace Epic.Data
{
    public class User
    {
        public int ID
        {
            get;
            set;
        }
        public int LevelID
        {
            get;
            set;
        }
        public UserLevel Level
        {
            get;
            set;
        }
        public string Name;
        public string Password;
        public DateTime CreateDate;
    }

    public class UserLevel
    {
        public int ID
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }
    }

    public static class Program
    {
        public static void Main()
        {



            ModelBuilder.Entity<User>().ToTable("DB_User");
            ModelBuilder.Entity<User>().Column(e => e.ID)
                .Name("ID")
                .Order(0)
                .Type("int")
                .Generated()
                .IsRequired();

            ModelBuilder.Entity<User>().Column(e => e.Name)
               .Name("Name")
               .Order(1)
               .Type("nvarchar")
               .Generated()
               .MaxLength(50)
               .IsUnicode()
               .IsVariableLength()
               .IsRequired();


            ModelBuilder.Entity<User>().PrimaryKeys(e => e.ID);
            ModelBuilder.Entity<User>().PrimaryKeys(e => new { e.ID, e.Name });
        }
    }
}
