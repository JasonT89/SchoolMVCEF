namespace SchoolMVCEF.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<SchoolMVCEF.Models.SchoolSystemDb>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

       
    }
}