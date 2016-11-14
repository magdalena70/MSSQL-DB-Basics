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
            foreach (var patient in collection)
            {
                Console.WriteLine($"Patient: {patient.FirstName} {patient.LastName}, date of birth: {patient.DateOfBirth}");

                Visitation[] visitations = patient.Visitations.ToArray();
                foreach (var visitation in visitations)
                {
                    Console.WriteLine($"\tVisitation date: {visitation.Date}, Comments: {visitation.Comments}");
                    Console.WriteLine($"Has Doctor: {visitation.Doctor != null}");
                }

                Diagnose[] diagnoses = patient.Diagnoses.ToArray();
                foreach (var diagnose in diagnoses)
                {
                    Console.WriteLine($"\tDiagnose: {diagnose.Name}");
                }

                Medicament[] medicaments = patient.Medicaments.ToArray();
                foreach (var medicament in medicaments)
                {
                    Console.WriteLine($"\t Medicament: {medicament.Name}");
                }
            }
        }
    }
}
