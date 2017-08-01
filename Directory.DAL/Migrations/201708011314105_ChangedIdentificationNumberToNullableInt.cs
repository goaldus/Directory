namespace Directory.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedIdentificationNumberToNullableInt : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.People", "IN", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.People", "IN", c => c.Int(nullable: false));
        }
    }
}
