namespace HospitalDB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Diagnoses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        Comments = c.String(),
                        Patient_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Patients", t => t.Patient_Id, cascadeDelete: true)
                .Index(t => t.Patient_Id);
            
            CreateTable(
                "dbo.Patients",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 30),
                        LastName = c.String(nullable: false, maxLength: 30),
                        Address = c.String(nullable: false, maxLength: 100),
                        Email = c.String(),
                        DateOfBirth = c.DateTime(nullable: false),
                        Picture = c.Binary(),
                        HasMedicalInsurance = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Medicaments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        Patient_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Patients", t => t.Patient_Id, cascadeDelete: true)
                .Index(t => t.Patient_Id);
            
            CreateTable(
                "dbo.Visitations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        Comments = c.String(nullable: false),
                        Patient_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Patients", t => t.Patient_Id, cascadeDelete: true)
                .Index(t => t.Patient_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Diagnoses", "Patient_Id", "dbo.Patients");
            DropForeignKey("dbo.Visitations", "Patient_Id", "dbo.Patients");
            DropForeignKey("dbo.Medicaments", "Patient_Id", "dbo.Patients");
            DropIndex("dbo.Visitations", new[] { "Patient_Id" });
            DropIndex("dbo.Medicaments", new[] { "Patient_Id" });
            DropIndex("dbo.Diagnoses", new[] { "Patient_Id" });
            DropTable("dbo.Visitations");
            DropTable("dbo.Medicaments");
            DropTable("dbo.Patients");
            DropTable("dbo.Diagnoses");
        }
    }
}
