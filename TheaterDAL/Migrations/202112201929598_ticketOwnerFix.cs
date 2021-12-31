namespace TheaterDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ticketOwnerFix : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Ticket", "Owner", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Ticket", "Owner");
        }
    }
}
