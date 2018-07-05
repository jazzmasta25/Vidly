namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MoviesChanges : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Movies", "MovieGenreId", "dbo.MovieGenres");
            DropIndex("dbo.Movies", new[] { "MovieGenreId" });
            DropPrimaryKey("dbo.MovieGenres");
            AlterColumn("dbo.MovieGenres", "Id", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.Movies", "MovieGenreId", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.MovieGenres", "Id");
            CreateIndex("dbo.Movies", "MovieGenreId");
            AddForeignKey("dbo.Movies", "MovieGenreId", "dbo.MovieGenres", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Movies", "MovieGenreId", "dbo.MovieGenres");
            DropIndex("dbo.Movies", new[] { "MovieGenreId" });
            DropPrimaryKey("dbo.MovieGenres");
            AlterColumn("dbo.Movies", "MovieGenreId", c => c.Byte(nullable: false));
            AlterColumn("dbo.MovieGenres", "Id", c => c.Byte(nullable: false));
            AddPrimaryKey("dbo.MovieGenres", "Id");
            CreateIndex("dbo.Movies", "MovieGenreId");
            AddForeignKey("dbo.Movies", "MovieGenreId", "dbo.MovieGenres", "Id", cascadeDelete: true);
        }
    }
}
