namespace HospitalDB
{
    using Model;
    using System.Data.Entity;

    public class HospitalContext : DbContext
    {
        public HospitalContext()
            : base("name=HospitalContext")
        {
        }

        public virtual DbSet<Patient> Patients { get; set; }
        public virtual DbSet<Visitation> Visitations { get; set; }
        public virtual DbSet<Diagnose> Diagnoses { get; set; }
        public virtual DbSet<Medicament> Medicaments { get; set; }
        public virtual DbSet<Doctor> Doctors { get; set; }
    }
}