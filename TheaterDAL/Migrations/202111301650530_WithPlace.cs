namespace TheaterDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class WithPlace : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Author",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                        Surname = c.String(),
                        PhoneNumber = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Place",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        PriceMultiplier = c.Double(nullable: false),
                        PlaceType = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Show",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                        ShowTime = c.DateTime(nullable: false),
                        Genre = c.Int(nullable: false),
                        AuthorId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Author", t => t.AuthorId, cascadeDelete: true)
                .Index(t => t.AuthorId);
            
            CreateTable(
                "dbo.Ticket",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Price = c.Int(nullable: false),
                        ShowId = c.Guid(),
                        PlaceId = c.Guid(nullable: false),
                        State = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Place", t => t.PlaceId, cascadeDelete: true)
                .ForeignKey("dbo.Show", t => t.ShowId, cascadeDelete: true)
                .Index(t => t.ShowId)
                .Index(t => t.PlaceId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Ticket", "ShowId", "dbo.Show");
            DropForeignKey("dbo.Ticket", "PlaceId", "dbo.Place");
            DropForeignKey("dbo.Show", "AuthorId", "dbo.Author");
            DropIndex("dbo.Ticket", new[] { "PlaceId" });
            DropIndex("dbo.Ticket", new[] { "ShowId" });
            DropIndex("dbo.Show", new[] { "AuthorId" });
            DropTable("dbo.Ticket");
            DropTable("dbo.Show");
            DropTable("dbo.Place");
            DropTable("dbo.Author");
        }
    }
}
