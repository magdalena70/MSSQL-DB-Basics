namespace StudentSystemDB.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddStudentsFriends : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.StudentsFriends",
                c => new
                    {
                        StudentId = c.Int(nullable: false),
                        FriendId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.StudentId, t.FriendId })
                .ForeignKey("dbo.Students", t => t.StudentId)
                .ForeignKey("dbo.Students", t => t.FriendId)
                .Index(t => t.StudentId)
                .Index(t => t.FriendId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StudentsFriends", "FriendId", "dbo.Students");
            DropForeignKey("dbo.StudentsFriends", "StudentId", "dbo.Students");
            DropIndex("dbo.StudentsFriends", new[] { "FriendId" });
            DropIndex("dbo.StudentsFriends", new[] { "StudentId" });
            DropTable("dbo.StudentsFriends");
        }
    }
}
