using BaiCrawler.MODEL;
using DevNote.Interface.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaiCrawler.DAL
{
    public class MyDBContext : DbContext
    {
        //static  MyDBContext()
        //{

        //    //Database.SetInitializer<MyDBContext>(new DbInitializer2());

        //    //Database.SetInitializer<MyDbContext>(null);
        //    using (MyDBContext db = new MyDBContext())
        //    {
        //       // db.Database.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["BaiCrawler.Properties.Settings.ConnectionString"].ConnectionString;

        //        db.Database.Initialize(false);
        //    }
              
        //}


        public MyDBContext() : base("BaiCrawler.Properties.Settings.MyDbContextConnectionString")
        {
            //Disable initializer
            Database.SetInitializer<MyDBContext>(null);
        }


        public DbSet<WFProfile> WFProfiles { get; set; }

        public DbSet<WFProfileParameter> WFProfileParameters { get; set; }

        public DbSet<TableConfig> TableConfigs { get; set; }





        public class DbInitializer1 : DropCreateDatabaseAlways<MyDBContext>
        {
            protected override void Seed(MyDBContext context)
            {
                //initialieze here...
                //context.TableConfigs.Add(new TableConfig
                //{
                //    BatchNo = 1,
                //    RecPerBatch = 10


                //});

               
                ///
                //base.Seed(context);
                //context.SaveChanges();
            }
        }


        public class DbInitializer2 : CreateDatabaseIfNotExists<MyDBContext>
        {
            protected override void Seed(MyDBContext context)
            {
                //initialieze here...
                //context.TableConfigs.Add(new TableConfig
                //{
                //    ApplicationName = "AIFS Project Manager",

                //    BatchNo = 1,
                //    RecPerBatch = 10


                //});

               
                //base.Seed(context);
                //context.SaveChanges();
            }
        }

    }
}
