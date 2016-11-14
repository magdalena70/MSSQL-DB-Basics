namespace HospitalDB.Migrations
{
    using Model;
    using System;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<HospitalContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "H_04_EntityFramework_CodeFirst.HospitalContext";
        }

        protected override void Seed(HospitalContext context)
        {
                Random random = new Random();

                string[] patientFirstNames = { "Georgi", "Penka", "Vasil", "Marko" };
                string[] patientLastNames = { "Ivanov", "Minkova", "Todorov", "Stefanov" };
                string[] emails = { "georgi@abv.bg", "pen_ka@minkova.ggg", "v-t@asdf.fg", "mar90ko@ko.bg" };
                int[] years = { 1990, 1978, 2002, 1987 };

                for (int i = 0; i < 4; i++)
                {
                    context.Patients.AddOrUpdate(p => p.FirstName, new Patient()
                    {
                        FirstName = patientFirstNames[i],
                        LastName = patientLastNames[i],
                        Address = "some address...",
                        Email = emails[i],
                        DateOfBirth = new DateTime(years[i], random.Next(12), random.Next(22)),
                        HasMedicalInsurance = true
                    });
                }
            
            Patient[] patients = context.Patients.Local.ToArray();

                string[] medicamentName = { "Phlebodia 600", "Vitamin C", "Magnesium 375", "Vitamin B-Complex Depo" };
                string[] diagnoseNames = { "Masdfgh lkjh", "QWER", "Qasd-Qfghj", "lkj-RT" };
                string[] doctorNames = { "Nevena Mindareva", "Nikola Georgiev", "Todor Hristov", "Iana Popova" };

                for (int i = 0; i < patients.Length; i++)
                {
                    context.Medicaments.AddOrUpdate(m => m.Name, new Medicament()
                    {
                        Name = medicamentName[i],
                        Patient = patients[i]
                    });

                context.Diagnoses.AddOrUpdate(d => d.Name, new Diagnose()
                {
                    Name = diagnoseNames[i],
                    Comments = "some comments...",
                    Patient = patients[i]
                });

                context.Doctors.AddOrUpdate(d => d.Name, new Doctor()
                    {
                        Name = doctorNames[i],
                        Specialty = "some specialty"
                    });
                }
           
            Doctor[] doctors = context.Doctors.Local.ToArray();
                for (int i = 0; i < doctors.Length; i++)
                {
                    context.Visitations.AddOrUpdate(new Visitation()
                    {
                        Date = new DateTime(2016, 9, 10 + i),
                        Patient = patients[i],
                        Doctor = doctors[i],
                        Comments = "some comments about..."
                    });
                }

                context.SaveChanges();
        }
    }
}
