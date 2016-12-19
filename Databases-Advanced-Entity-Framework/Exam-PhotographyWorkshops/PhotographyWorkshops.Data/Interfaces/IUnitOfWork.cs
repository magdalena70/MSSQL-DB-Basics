using PhotographyWorkshops.Models;

namespace PhotographyWorkshops.Data.Interfaces
{
    public interface IUnitOfWork
    {
        IRepository<Lens> Lenses { get; }

        IRepository<Accessory> Accessories { get; }

        IRepository<Camera> Cameras { get; }

        IRepository<Workshop> Workshops { get; }

        IRepository<Photographer> Photographers { get;}

        void Commit();
    }
}
