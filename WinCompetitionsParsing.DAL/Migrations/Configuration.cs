namespace WinCompetitionsParsing.DAL.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<WinCompetitionsParsing.DAL.MakeUpContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "WinCompetitionsParsing.DAL.MakeUpContext";
        }

        protected override void Seed(WinCompetitionsParsing.DAL.MakeUpContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}