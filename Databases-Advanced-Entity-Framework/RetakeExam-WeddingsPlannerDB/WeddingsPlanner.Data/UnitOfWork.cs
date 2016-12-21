using WeddingsPlanner.Data.Interfaces;
using WeddingsPlanner.Models;

namespace WeddingsPlanner.Data
{

    public class UnitOfWork : IUnitOfWork
    {
        private WeddingsPlannerContext context;
        private IRepository<Agency> agencies;
        private IRepository<Cash> cash;
        private IRepository<Gift> gifts;
        private IRepository<Invitation> invitations;
        private IRepository<Person> people;
        private IRepository<Venue> venues;
        private IRepository<Wedding> weddings;

        public UnitOfWork()
        {
            this.context = new WeddingsPlannerContext();
        }

        public IRepository<Agency> Agencies => this.agencies ?? (this.agencies = new Repository<Agency>(this.context.Agencies));

        public IRepository<Cash> Cash => this.cash ?? (this.cash = new Repository<Cash>(this.context.Cash));

        public IRepository<Gift> Gifts => this.gifts ?? (this.gifts = new Repository<Gift>(this.context.Gifts));

        public IRepository<Invitation> Invitations => this.invitations ?? (this.invitations = new Repository<Invitation>(this.context.Invitations));

        public IRepository<Person> People => this.people ?? (this.people = new Repository<Person>(this.context.People));

        public IRepository<Venue> Venues => this.venues ?? (this.venues = new Repository<Venue>(this.context.Venues));

        public IRepository<Wedding> Weddings => this.weddings ?? (this.weddings = new Repository<Wedding>(this.context.Weddings));

        public void Commit()
        {
            this.context.SaveChanges();
        }
    }
}
