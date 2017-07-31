using Directory.DAL.Entities;

namespace Directory.DAL.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Directory.DAL.DirectoryDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Directory.DAL.DirectoryDbContext context)
        {
            var peter = new Person
            {
                Id = new Guid("5abdfee1-c970-4afd-aff8-aa3cfef8b1ac"),
                FirstName = "Petr",
                LastName = "Rychlý",
                IN = 26308789,
                TIN = "CZ26308789"
            };

            var john = new Person
            {
                Id = new Guid("371d7cfe-61c1-4600-ad82-a62554bed8c5"),
                FirstName = "Jan",
                LastName = "Novotný",
                IN = 26702924,
                TIN = "CZ26702924"
            };

            context.Persons.AddOrUpdate(p => p.Id, peter, john);
            context.SaveChanges();

            peter = context.Persons.First(p => p.Id == peter.Id);
            john = context.Persons.First(p => p.Id == john.Id);

            var firstAddress = new Address
            {
                Id = new Guid("012ac89a-94e3-4bc2-94b5-c9b05fc83375"),
                City = "Brno",
                Street = "Pes 123",
                ZipCode = "60167",
                Person = john
            };

            var secondAddress = new Address
            {
                Id = new Guid("41ba4c9a-f5bc-48b7-ab54-9dd14ae76790"),
                City = "Praha",
                Street = "Mariánské nám. 2/2",
                ZipCode = "11001",
                Person = peter
            };

            context.Adresses.AddOrUpdate(i => i.Id, firstAddress, secondAddress);
            context.SaveChanges();
        }
    }
}
