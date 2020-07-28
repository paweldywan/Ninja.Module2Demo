namespace NinjaDomain.DataModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StringLengthForName : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Ninjas", "Name", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Ninjas", "Name", c => c.String());
        }
    }
}
