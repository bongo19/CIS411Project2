using System.Web.Security;
using WebMatrix.WebData;

namespace MeetUs.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<MeetUs.Context.DataContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(MeetUs.Context.DataContext context)
        {

            WebSecurity.InitializeDatabaseConnection(
                "DefaultConnection",
                "UserProfile",
                "UserId",
                "UserName", autoCreateTables: true);

            if(!Roles.RoleExists("Administrator"))
                Roles.CreateRole("Administrator");

            if (!WebSecurity.UserExists("channing"))
                WebSecurity.CreateUserAndAccount(
                    "channing",
                    "password"
                    );

            if (!WebSecurity.UserExists("guest"))
                WebSecurity.CreateUserAndAccount(
                    "guest",
                    "password"
                    );

            if(!Roles.GetRolesForUser("channing").Contains("Administrator"))
                Roles.AddUsersToRoles(new [] {"channing"}, new [] {"Administrator"});




            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}
