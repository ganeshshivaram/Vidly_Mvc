namespace Vidly.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AddMoviesData : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO MOVIES (Name, ReleaseDate, DateAdded, NumberInStock, GenreId) " +
                "VALUES ('Die Hard', '1990-01-08 08:29:38.527', '2017-11-08 08:29:38.527', 5, 2 )");

            Sql("INSERT INTO MOVIES (Name, ReleaseDate, DateAdded, NumberInStock, GenreId) " +
               "VALUES ('Se7en', '1997-01-08 08:29:38.527', '2016-11-08 08:29:38.527', 3, 5 )");

            Sql("INSERT INTO MOVIES (Name, ReleaseDate, DateAdded, NumberInStock, GenreId) " +
               "VALUES ('The Beautiful Mind', '2001-01-08 08:29:38.527', '2010-11-08 08:29:38.527', 10, 3 )");

            Sql("INSERT INTO MOVIES (Name, ReleaseDate, DateAdded, NumberInStock, GenreId) " +
               "VALUES ('Valkyrie', '1990-01-08 08:29:38.527', '2013-11-08 08:29:38.527', 2, 4 )");
        }

        public override void Down()
        {
        }
    }
}
