using System;
using PhotographyWorkshops.Data;
using System.Linq;
using Newtonsoft.Json;
using System.IO;
using PhotographyWorkshops.Models;

namespace PhotographyWorkshops.ExportJson
{
    class Program
    {
        static void Main()
        {
            UnitOfWork unit = new UnitOfWork();
            //ExportingToJSON(unit);
        }

        private static void ExportingToJSON(UnitOfWork unit)
        {
            OrderedPhotographers(unit);
            LandscapePhotographers(unit);
        }

        private static void LandscapePhotographers(UnitOfWork unit)
        {
            var photographers = unit.Photographers
                .GetAll(ph => ph.PrimaryCamera is DSLRCamera && 
                        ph.Lenses.All(l => l.FocalLength <= 30) && 
                        ph.Lenses.Count > 0)
                .OrderBy(ph => ph.FirstName)
                .Select(ph => new
                {
                    ph.FirstName,
                    ph.LastName,
                    PrimaryCameraMake = ph.PrimaryCamera.Make,
                    LensesCount = ph.Lenses.Count
                });

            var photographersAsJson = JsonConvert.SerializeObject(photographers, Formatting.Indented);
            File.WriteAllText("../../../exportedJson/landscape-photographers.json", photographersAsJson);
        }

        private static void OrderedPhotographers(UnitOfWork unit)
        {
            var photographers = unit.Photographers
                .GetAll()
                .OrderBy(ph => ph.FirstName)
                .ThenByDescending(ph => ph.LastName)
                .Select(ph => new
                {
                    ph.FirstName,
                    ph.LastName,
                    ph.Phone
                });

            var photographersAsJson = JsonConvert.SerializeObject(photographers, Formatting.Indented);
            File.WriteAllText("../../../exportedJson/photographers-ordered.json", photographersAsJson);
        }
    }
}
