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
            Delete.Table("ExceptionInstanceStateType");
            Delete.Table("ExceptionInstanceState");
            Delete.Table("ExceptionInstance");
            Delete.Table("ExceptionType");
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
                .WithColumn("UserId").AsGuid().PrimaryKey().ForeignKey("User", "UserId")
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
                .WithColumn("ApiKey").AsGuid().Unique().Indexed();

            Create.Table("UserApplication")
                .WithColumn("UserId").AsGuid().PrimaryKey().ForeignKey("User", "UserId")
                .WithColumn("ApplicationId").AsGuid().PrimaryKey().ForeignKey("Application", "ApplicationId");

            Create.Table("ApplicationUser")
                .WithColumn("ApplicationUserId").AsGuid().PrimaryKey()
                .WithColumn("ApplicationId").AsGuid().ForeignKey("Application", "ApplicationId")
                .WithColumn("UserName").AsString();

            Create.Table("ExceptionGroup")
                .WithColumn("ExceptionGroupId").AsGuid().PrimaryKey()
                .WithColumn("ApplicationId").AsGuid().ForeignKey("Application", "ApplicationId")
                .WithColumn("ExceptionHash").AsString()
                .WithColumn("ExceptionType").AsString(1000)
                .WithColumn("Message").AsString(1000);

            Create.Table("ExceptionInstance")
                .WithColumn("ExceptionInstanceId").AsGuid().PrimaryKey()
                .WithColumn("ExceptionGropuId").AsGuid().ForeignKey("ExceptionGroup", "ExceptionGroupId")
                .WithColumn("UserId").AsGuid().ForeignKey("ApplicationUser", "ApplicationUserId")
                .WithColumn("DateOccurred").AsDateTime()
                .WithColumn("MachineName").AsString()
                .WithColumn("Url").AsString(1000)
                .WithColumn("UrlReferrer").AsString(1000)
                .WithColumn("HttpCode").AsInt32().Nullable()
                .WithColumn("HtmlErrorMessage").AsString(1000)
                .WithColumn("ExtendedData").AsString(1000000);

            Create.Table("ExceptionInstanceStateType")
                .WithColumn("ExceptionInstanceStateTypeId").AsInt32().PrimaryKey()
                .WithColumn("Name").AsString();

            Insert.IntoTable("ExceptionInstanceStateType")
                .Row(new { ExceptionInstanceStateTypeId = 0, Name = "Server" })
                .Row(new { ExceptionInstanceStateTypeId = 1, Name = "QueryString" })
                .Row(new { ExceptionInstanceStateTypeId = 2, Name = "Application" })
                .Row(new { ExceptionInstanceStateTypeId = 3, Name = "Form" })
                .Row(new { ExceptionInstanceStateTypeId = 4, Name = "Cookie" });

            Create.Table("ExceptionInstanceState")
                .WithColumn("ExceptionInstanceId").AsGuid().PrimaryKey().ForeignKey("ExceptionInstance", "ExceptionInstanceId")
                .WithColumn("StateTypeId").AsInt32().PrimaryKey().ForeignKey("ExceptionInstanceStateType", "ExceptionInstanceStateTypeId")
                .WithColumn("Key").AsString(255).PrimaryKey()
                .WithColumn("Value").AsString(1000);
        }
    }
}