namespace H_04_EntityFramework_CodeFirst.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<H_04_EntityFramework_CodeFirst.UserContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "H_04_EntityFramework_CodeFirst.UserContext";
        }

        protected override void Seed(UserContext context)
        {
            //  This method will be called after migrating to the latest version.
        }
    }
}
