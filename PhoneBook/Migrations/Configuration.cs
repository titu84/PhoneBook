namespace PhoneBook.Migrations
{
    using System.Data.Entity.Migrations;
    using PhoneBook.Models.Repos;

    internal sealed class Configuration : DbMigrationsConfiguration<PhoneBook.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(PhoneBook.Models.ApplicationDbContext context)
        {
            var repo = new PersonRepo(context).GetSamplePeople();
            repo.ForEach(p => context.People.AddOrUpdate(p)); //TODO
        }
    }
}
