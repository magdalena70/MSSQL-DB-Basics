using HospitalDB.Model;
using System;
using System.Linq;

namespace HospitalDB
{
    class Program
    {
        static void Main()
        {
            HospitalContext context = new HospitalContext();
            Visitation[] collection = context.Visitations.ToArray();
            foreach (var item in collection)
            {
                Console.WriteLine($"{item.Doctor == null}");
            }
            //Patient[] collection = context.Patients.ToArray();
            //foreach (var item in collection)
            //{
            //    Console.WriteLine($"Patient: {item.FirstName} {item.LastName}");
            //    foreach (var i in item.Visitations)
            //    {
            //        Console.WriteLine($"Visitation date: {i.Date}, Comments: {i.Comments}");
            //        Console.WriteLine($"Doctor: {i.Doctor == null}");
            //    }

            //    foreach (var i in item.Medicaments)
            //    {
            //        Console.WriteLine($"Medicament: {i.Name}");
            //        Console.WriteLine($"Patient: {i.Patient == null}");
            //    }
            //}
        }
    }
}
