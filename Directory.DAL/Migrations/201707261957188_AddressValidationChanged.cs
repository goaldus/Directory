namespace Directory.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddressValidationChanged : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Addresses", "Person_Id", "dbo.People");
            DropIndex("dbo.Addresses", new[] { "Person_Id" });
            AlterColumn("dbo.Addresses", "Person_Id", c => c.Guid());
            CreateIndex("dbo.Addresses", "Person_Id");
            AddForeignKey("dbo.Addresses", "Person_Id", "dbo.People", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Addresses", "Person_Id", "dbo.People");
            DropIndex("dbo.Addresses", new[] { "Person_Id" });
            AlterColumn("dbo.Addresses", "Person_Id", c => c.Guid(nullable: false));
            CreateIndex("dbo.Addresses", "Person_Id");
            AddForeignKey("dbo.Addresses", "Person_Id", "dbo.People", "Id", cascadeDelete: true);
        }
    }
}
