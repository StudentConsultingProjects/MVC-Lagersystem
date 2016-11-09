namespace MVC_Lagersystem.Migrations
{
    using MVC_Lagersystem.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<MVC_Lagersystem.Data_Access_Layer.StoreContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(MVC_Lagersystem.Data_Access_Layer.StoreContext context)
        {
            //  This method will be called after migrating to the latest version.
            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.

            context.Items.AddOrUpdate(
                new StockItem { Name = "Tomatsås", Description = "En burk med krossade tomater.", Price = 29.90M, Shelf = "22B" },
                new StockItem { Name = "Majs", Description = "En gul liten sak.", Price = 9.49M, Shelf = "22A" },
                new StockItem { Name = "Diskmedel", Description = "Diskmedel från YES.", Price = 199M, Shelf = "10A" },
                new StockItem { Name = "Elektrisk tandborste", Description = "I am nr.1 from Oral.B.", Price = 2999M, Shelf = "11A" },
                new StockItem { Name = "Dogge Doggelito", Description = "Cykeln på köpet.", Price = 5M, Shelf = "Vid utcheckning" }
            );
        }
    }
}
