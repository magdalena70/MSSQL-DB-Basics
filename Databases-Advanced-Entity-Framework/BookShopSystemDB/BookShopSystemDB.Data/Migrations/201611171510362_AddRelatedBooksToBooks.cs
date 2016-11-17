namespace BookShopSystemDB.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRelatedBooksToBooks : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BooksRelatedBooks",
                c => new
                    {
                        BookId = c.Int(nullable: false),
                        RelatedBookId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.BookId, t.RelatedBookId })
                .ForeignKey("dbo.Books", t => t.BookId)
                .ForeignKey("dbo.Books", t => t.RelatedBookId)
                .Index(t => t.BookId)
                .Index(t => t.RelatedBookId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BooksRelatedBooks", "RelatedBookId", "dbo.Books");
            DropForeignKey("dbo.BooksRelatedBooks", "BookId", "dbo.Books");
            DropIndex("dbo.BooksRelatedBooks", new[] { "RelatedBookId" });
            DropIndex("dbo.BooksRelatedBooks", new[] { "BookId" });
            DropTable("dbo.BooksRelatedBooks");
        }
    }
}
