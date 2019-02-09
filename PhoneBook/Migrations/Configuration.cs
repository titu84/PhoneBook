namespace PhoneBook.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Logger;
    using Models.Book;
    using PhoneBook.Models.Repos;

    internal sealed class Configuration : DbMigrationsConfiguration<PhoneBook.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(PhoneBook.Models.ApplicationDbContext context)
        {
            var repo = new PersonRepo(context, new ErrorLogger()).GetSamplePeople();
            repo.ForEach(p => context.People.AddOrUpdate(p)); //TODO
        }
    }
}
