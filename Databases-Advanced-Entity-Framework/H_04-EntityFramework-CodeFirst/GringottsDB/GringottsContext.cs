namespace GringottsDB
{
    using Models;
    using System.Data.Entity;

    public class GringottsContext : DbContext
    {
        public GringottsContext()
            : base("name=GringottsContext")
        {
        }

        public virtual DbSet<WizardDeposit> WizardDeposits { get; set; }
    }
}