using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exceptional.DatabaseMigrations
{
    [Migration(20140206)]
    public class V20140206_Initial : Migration
    {
        public override void Down()
        {
            Delete.Table("ApplicationUser");
            Delete.Table("Application");
            Delete.Table("UserSecurity");
            Delete.Table("UserLogon");
            Delete.Table("User");
        }

        public override void Up()
        {
            Create.Table("User")
                .WithColumn("UserId").AsGuid().PrimaryKey()
                .WithColumn("DateCreated").AsDateTime()
                .WithColumn("Email").AsAnsiString(255).Unique()
                .WithColumn("Name").AsAnsiString(255);

            Create.Table("UserSecurity")
                .WithColumn("UserId").AsGuid().PrimaryKey()
                .WithColumn("Password").AsAnsiString(255)
                .WithColumn("DateCreated").AsDateTime();

            Create.Table("UserLogon")
                .WithColumn("UserId").AsGuid().PrimaryKey().ForeignKey("User", "UserId")
                .WithColumn("DateLoggedOn").AsDateTime().PrimaryKey();

            Create.Table("Application")
                .WithColumn("ApplicationId").AsGuid().PrimaryKey()
                .WithColumn("Name").AsAnsiString(255)
                .WithColumn("DateCreated").AsDateTime()
                .WithColumn("Website").AsAnsiString(255)
                .WithColumn("ApiKey").AsGuid().Unique();

            Create.Table("ApplicationUser")
                .WithColumn("ApplicationId").AsGuid().PrimaryKey().ForeignKey("Application", "ApplicationId")
                .WithColumn("UserId").AsGuid().PrimaryKey().ForeignKey("User", "UserId");
        }
    }
}