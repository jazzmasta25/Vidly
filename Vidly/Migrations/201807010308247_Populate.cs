namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Populate : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO MembershipTypes (Id, SignupFee, DurationInMonths, DiscountRate) VALUES (1, 0, 0, 0)");
            Sql("INSERT INTO MembershipTypes (Id, SignupFee, DurationInMonths, DiscountRate) VALUES (2, 30, 1, 10)");
            Sql("INSERT INTO MembershipTypes (Id, SignupFee, DurationInMonths, DiscountRate) VALUES (3, 90, 3, 15)");
            Sql("INSERT INTO MembershipTypes (Id, SignupFee, DurationInMonths, DiscountRate) VALUES (4, 300, 12, 20)");

            Sql("UPDATE MembershipTypes SET name = 'Pay as You Go' WHERE Id=1");
            Sql("UPDATE MembershipTypes SET name = 'Monthly' WHERE Id=2");
            Sql("UPDATE MembershipTypes SET name = 'Quarterly' WHERE Id=3");
            Sql("UPDATE MembershipTypes SET name = 'Yearly' WHERE Id=4");

            Sql("UPDATE customers SET Birthdate='1/1/1980' WHERE Id=1");

            Sql("INSERT INTO MovieGenres (Id, Name) VALUES (1,'Action')");
            Sql("INSERT INTO MovieGenres (Id, Name) VALUES (2,'Comedy')");
            Sql("INSERT INTO MovieGenres (Id, Name) VALUES (3,'Family')");
            Sql("INSERT INTO MovieGenres (Id, Name) VALUES (4,'Romance')");



            DateTime dateAndTime = DateTime.Now;
            var date = dateAndTime.ToString("MM/dd/yyyy");
            Sql("INSERT INTO Movies (Name, Stock, ReleaseDate, DateAdded, MovieGenreId) VALUES ('Die Hard',5,'07/15/1988'," + date + ",1)");
            Sql("INSERT INTO Movies (Name, Stock, ReleaseDate, DateAdded, MovieGenreId) VALUES ('Hangover',5,'06/02/2009'," + date + ",2)");
            Sql("INSERT INTO Movies (Name, Stock, ReleaseDate, DateAdded, MovieGenreId) VALUES ('The Terminator',5,'10/26/1987'," + date + ",1)");
            Sql("INSERT INTO Movies (Name, Stock, ReleaseDate, DateAdded, MovieGenreId) VALUES ('Titanic',5,'12/19/1997'," + date + ",4)");
            Sql("INSERT INTO Movies (Name, Stock, ReleaseDate, DateAdded, MovieGenreId) VALUES ('Toy Story',5,'11/22/1995'," + date + ",3)");
        }
        
        public override void Down()
        {
        }
    }
}
