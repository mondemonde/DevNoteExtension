using BaiCrawler.MODEL;
using BaiTextFilterClassLibrary;
using DevNote.Interface.Models;
using MyCommonLib.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaiCrawler.DAL
{
    public class MyDBContext : DbContext
    {
        static MyDBContext()
        {


            Database.SetInitializer<MyDBContext>(new DbInitializer2());

            //Database.SetInitializer<MyDbContext>(null);
            using (MyDBContext db = new MyDBContext())
                db.Database.Initialize(false);
        }

        public IDbSet<TableConfig> TableConfigs { get; set; }
         public DbSet<WebItem> WebItems { get; set; }

        public DbSet<WFProfile> WFProfiles { get; set; }

        public DbSet<WFProfileParameter> WFProfileParameters { get; set; }






        public class DbInitializer1 : DropCreateDatabaseAlways<MyDBContext>
        {
            protected override void Seed(MyDBContext context)
            {
                //initialieze here...
                context.TableConfigs.Add(new TableConfig
                {
                    BatchNo = 1,
                    RecPerBatch = 10


                });

                //int length = 1000;

                //for (int i = 0; i < length; i++)
                //{
                //    context.GeoAddresses.Add(new GeoAddress
                //    {
                //        AddressId = i,
                //        Created = DateTime.Now,
                //        HFAddress_TypeID = "P1",
                //        HFAddress_Type_Desc = "test",
                //        HostFamilyId = "HF" + i.ToString(),
                //        Modified = DateTime.Now,
                //        New_Address = "New_Address" + i.ToString(),
                //        Old_Address = "Old_Address" + i.ToString()
                //    });
                //}
                ///
                base.Seed(context);
                context.SaveChanges();
            }
        }


        public class DbInitializer2 : CreateDatabaseIfNotExists<MyDBContext>
        {
            protected override void Seed(MyDBContext context)
            {
                //initialieze here...
                context.TableConfigs.Add(new TableConfig
                {
                    //ApplicationName = "AIFS Project Manager",
                    BatchNo = 1,
                    RecPerBatch = 10


                });

                //int length = 1000;

                //for (int i = 0; i < length; i++)
                //{
                //    context.GeoAddresses.Add(new GeoAddress {
                //        AddressId=i,
                //        Created=DateTime.Now,
                //        HFAddress_TypeID="P1",
                //        HFAddress_Type_Desc="test",
                //        HostFamilyId ="HF"+ i.ToString(),
                //        Modified = DateTime.Now(),
                //        New_Address = "New_Address" + i.ToString(),
                //        Old_Address = "Old_Address" + i.ToString()
                //    });
                //}
                ///
                base.Seed(context);
                context.SaveChanges();
            }
        }

    }
}
