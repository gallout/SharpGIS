using System.Data;
using Migrator.Framework;

namespace ITS.DbMigration.Spectrum
{
    [Migration(201803231358)]
    public class Migration201803231358 : Migration
    {
        public override void Up()
        {
            Database.AddColumn("so_some_object", "some_object_status", DbType.String, "'Set'");
        }

        public override void Down()
        {
            Database.RemoveColumn("so_some_object", "some_object_status");
        }
    }
}
