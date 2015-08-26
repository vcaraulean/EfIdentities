namespace EfIdentities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Next : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.OrderLines");
            AlterColumn("dbo.OrderLines", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.OrderLines", new[] { "Id", "OrderId" });
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.OrderLines");
            AlterColumn("dbo.OrderLines", "Id", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.OrderLines", new[] { "Id", "OrderId" });
        }
    }
}
