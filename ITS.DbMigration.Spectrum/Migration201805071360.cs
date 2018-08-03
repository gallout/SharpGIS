using System.Data;
using Migrator.Framework;

namespace ITS.DbMigration.Spectrum
{
    [Migration(201805071360)]
    public class Migration201805071360 : Migration
    {
        public override void Up()
        {
            //Удаление таблиц
            Database.RemoveTable("so_some_object");
        }

        public override void Down()
        {
           
        }
    }
}
