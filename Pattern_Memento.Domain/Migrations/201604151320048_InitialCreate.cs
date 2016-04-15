namespace Pattern_Memento.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Canvas",
                c => new
                    {
                        CanvasID = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.CanvasID);
            
            CreateTable(
                "dbo.Figures",
                c => new
                    {
                        FigureID = c.Int(nullable: false, identity: true),
                        CanvasID = c.Int(nullable: false),
                        Color = c.String(),
                        X = c.Int(nullable: false),
                        Y = c.Int(nullable: false),
                        Radius = c.Int(),
                        Width = c.Int(),
                        Height = c.Int(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.FigureID)
                .ForeignKey("dbo.Canvas", t => t.CanvasID, cascadeDelete: true)
                .Index(t => t.CanvasID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Figures", "CanvasID", "dbo.Canvas");
            DropIndex("dbo.Figures", new[] { "CanvasID" });
            DropTable("dbo.Figures");
            DropTable("dbo.Canvas");
        }
    }
}
