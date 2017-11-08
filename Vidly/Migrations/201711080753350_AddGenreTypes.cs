namespace Vidly.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AddGenreTypes : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO Genres (Name) Values ('Horror')");
            Sql("INSERT INTO Genres (Name) Values ('Action')");
            Sql("INSERT INTO Genres (Name) Values ('Romance')");
            Sql("INSERT INTO Genres (Name) Values ('War')");
            Sql("INSERT INTO Genres (Name) Values ('Thriller')");

        }

        public override void Down()
        {
        }
    }
}
