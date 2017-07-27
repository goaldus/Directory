namespace Directory.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class nextone : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.People", "IN", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.People", "IN", c => c.String(maxLength: 8));
        }
    }
}
