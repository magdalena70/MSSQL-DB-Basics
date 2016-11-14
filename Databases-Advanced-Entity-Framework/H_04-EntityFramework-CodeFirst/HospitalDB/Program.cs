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
            
            Patient[] collection = context.Patients.ToArray();
            foreach (var item in collection)
            {
                Console.WriteLine($"Patient: {item.FirstName} {item.LastName}");
                foreach (var i in item.Visitations)
                {
                    Console.WriteLine($"\tVisitation date: {i.Date}, Comments: {i.Comments}");
                    Console.WriteLine($"Has Doctor: {i.Doctor != null}");
                }

                foreach (var i in item.Diagnoses)
                {
                    Console.WriteLine($"\tDiagnose: {i.Name}");
                }

                foreach (var i in item.Medicaments)
                {
                    Console.WriteLine($"\t Medicament: {i.Name}");
                }
            }
        }
    }
}
