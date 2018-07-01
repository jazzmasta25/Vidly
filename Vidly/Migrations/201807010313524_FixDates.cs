namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixDates : DbMigration
    {
        public override void Up()
        {
            DateTime dateAndTime = DateTime.Now;
            Sql("Update Movies SET DateAdded = '" + dateAndTime + "'");
        }
        
        public override void Down()
        {
        }
    }
}
