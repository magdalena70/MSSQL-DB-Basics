using WeddingsPlanner.Models;

namespace WeddingsPlanner.Data.Interfaces
{
    public interface IUnitOfWork
    {
        IRepository<Agency> Agencies { get; }

        IRepository<Cash> Cash { get; }

        IRepository<Gift> Gifts { get; }

        IRepository<Invitation> Invitations { get; }

        IRepository<Person> People { get; }

        IRepository<Venue> Venues { get; }

        IRepository<Wedding> Weddings { get; }

        void Commit();
    }
}
