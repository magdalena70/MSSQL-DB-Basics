using PhotographyWorkshops.Data.Interfaces;
using PhotographyWorkshops.Models;

namespace PhotographyWorkshops.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private PhotographyWorkshopsContext context;
        private IRepository<Lens> lenses;
        private IRepository<Accessory> accessories;
        private IRepository<Camera> cameras;
        private IRepository<Workshop> workshops;
        private IRepository<Photographer> photographers;

        public UnitOfWork()
        {
            this.context = new PhotographyWorkshopsContext();
        }

        public IRepository<Lens> Lenses => this.lenses ?? (this.lenses = new Repository<Lens>(this.context.Lenses));

        public IRepository<Accessory> Accessories => this.accessories ?? (this.accessories = new Repository<Accessory>(this.context.Accessories));

        public IRepository<Camera> Cameras => this.cameras ?? (this.cameras = new Repository<Camera>(this.context.Cameras));

        public IRepository<Workshop> Workshops => this.workshops ?? (this.workshops = new Repository<Workshop>(this.context.Workshops));

        public IRepository<Photographer> Photographers => this.photographers ?? (this.photographers = new Repository<Photographer>(this.context.Photographers));

        public void Commit()
        {
            this.context.SaveChanges();
        }
    }
}
