using System.Data;
using Migrator.Framework;

namespace ITS.DbMigration.Spectrum
{
    /// <summary>
    /// Миграция для создания схемы.
    /// </summary>
    [Migration(201805071451)]
    public class Migration201805071451 : Migration
    {
        public override void Up()
        {
            //Создание таблиц
            Database.AddTable("vo_velo_object", new[]
                                                       {
                                                           new Column("id", DbType.Int64, ColumnProperty.PrimaryKey),                                                           
                                                           new Column("velo_type", DbType.String),
                                                           new Column("velo_view", DbType.String),
                                                           new Column("velo_field", DbType.Int32),                                                          
                                                           new Column("feature_object_id", DbType.Int64),
                                                       });

            Database.AddColumn("vo_velo_object", new Column("velo_length", DbType.Int32));
            Database.AddColumn("vo_velo_object", new Column("velo_width", DbType.Int32));
            Database.AddColumn("vo_velo_object", new Column("data_set", DbType.DateTime));
            Database.AddColumn("vo_velo_object", new Column("data_check", DbType.DateTime));
            Database.AddColumn("vo_velo_object", new Column("velo_object_status", DbType.String));

            //Создание ограничений по внешнему ключу
            Database.AddForeignKey("vo_featureobject_veloobject", "vo_velo_object", "feature_object_id", "featureobject", "id");
            //Удаление ограничений по внешнему ключу
            Database.RemoveForeignKey("so_some_object", "so_featureobject_someobject");

            //Удаление таблиц
            Database.RemoveTable("so_some_object");
        }

        public override void Down()
        {          

            //Удаление ограничений по внешнему ключу
            Database.RemoveForeignKey("vo_velo_object", "vo_featureobject_veloobject");

            //Удаление таблиц
            Database.RemoveTable("vo_veloobject");
        }
    }
}