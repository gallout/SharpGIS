using System.Data;
using Migrator.Framework;

namespace ITS.DbMigration.Spectrum
{
    /// <summary>
    /// Миграция для создания схемы.
    /// </summary>
    [Migration(201805081456)]
    public class Migration201805081456 : Migration
    {
        public override void Up()
        {
            
           
            Database.RemoveColumn("vo_velo_object", "angle");
            Database.AddColumn("vo_velo_object", new Column("angle", DbType.Double));

        }

        public override void Down()
        {          

        }
    }
}