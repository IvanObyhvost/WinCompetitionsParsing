namespace WinCompetitionsParsing.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class adk : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Subcategories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Category = c.String(),
                        Row = c.Int(nullable: false),
                        Col = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Subcategories");
        }
    }
}
