namespace DataBuildingLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migrations : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AdminLogin",
                c => new
                    {
                        Aid = c.Int(nullable: false, identity: true),
                        uname = c.String(maxLength: 4000),
                        pswd = c.String(maxLength: 4000),
                        LastLogin = c.DateTime(nullable: false),
                        Loginfo = c.String(maxLength: 4000),
                        productinfo = c.String(maxLength: 4000),
                    })
                .PrimaryKey(t => t.Aid);
            
            CreateTable(
                "dbo.InfoLog",
                c => new
                    {
                        LogId = c.Int(nullable: false, identity: true),
                        dt = c.DateTime(nullable: false),
                        UserName = c.String(maxLength: 4000),
                        ModuleName = c.String(maxLength: 4000),
                        ActionName = c.String(maxLength: 4000),
                        Description = c.String(maxLength: 4000),
                        Editdate = c.DateTime(),
                    })
                .PrimaryKey(t => t.LogId);
            
            CreateTable(
                "dbo.International",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        RDate = c.DateTime(nullable: false),
                        RET_ADV = c.String(maxLength: 4000),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.OverTime",
                c => new
                    {
                        cid = c.Int(nullable: false, identity: true),
                        name = c.String(maxLength: 4000),
                        UserName = c.String(maxLength: 4000),
                        NrmlWHrs = c.Int(nullable: false),
                        SatWHrs = c.Int(nullable: false),
                        SunWhrs = c.Int(nullable: false),
                        HolidayWHrs = c.Int(nullable: false),
                        FixedOverTime = c.Int(nullable: false),
                        HourlyRate = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Currency = c.String(maxLength: 4000),
                        holidays = c.String(maxLength: 4000),
                        did = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.cid);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.OverTime");
            DropTable("dbo.International");
            DropTable("dbo.InfoLog");
            DropTable("dbo.AdminLogin");
        }
    }
}
