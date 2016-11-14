namespace H_04_EntityFramework_CodeFirst.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddBornTownAndCurrentlyLivingTownToUsers : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "bornTown_Id", c => c.Int());
            AddColumn("dbo.Users", "currentlyLivingTown_Id", c => c.Int());
            CreateIndex("dbo.Users", "bornTown_Id");
            CreateIndex("dbo.Users", "currentlyLivingTown_Id");
            AddForeignKey("dbo.Users", "bornTown_Id", "dbo.Towns", "Id");
            AddForeignKey("dbo.Users", "currentlyLivingTown_Id", "dbo.Towns", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "currentlyLivingTown_Id", "dbo.Towns");
            DropForeignKey("dbo.Users", "bornTown_Id", "dbo.Towns");
            DropIndex("dbo.Users", new[] { "currentlyLivingTown_Id" });
            DropIndex("dbo.Users", new[] { "bornTown_Id" });
            DropColumn("dbo.Users", "currentlyLivingTown_Id");
            DropColumn("dbo.Users", "bornTown_Id");
        }
    }
}
