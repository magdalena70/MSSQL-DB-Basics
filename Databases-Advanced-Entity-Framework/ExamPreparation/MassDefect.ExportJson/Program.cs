using MassDefectDB.Data;
using System.Linq;
using Newtonsoft.Json;
using System.IO;

namespace MassDefect.ExportJson
{
    class Program
    {
        static void Main()
        {
            UnitOfWork unit = new UnitOfWork();

            ExportPlanetsWhichAreNotAnomalyOrigins(unit);
            ExportPeopleWhichHaveNotBeenVictims(unit);
            ExportTopAnomaly(unit);
        }

        private static void ExportTopAnomaly(UnitOfWork unit)
        {
            var anomaly = unit.Anomalies
                .GetAll()
                .OrderByDescending(a => a.Victims.Count)
                .Take(1)
                .Select(a => new
                {
                    id = a.Id,
                    originPlanet = new
                    {
                        name = a.OriginPlanet.Name
                    },
                    teleportPlanet = new
                    {
                        name = a.TeleportPlanet.Name
                    },
                    victimsCount = a.Victims.Count
                });
            string anomalyAsJson = JsonConvert.SerializeObject(anomaly, Formatting.Indented);
            File.WriteAllText("../../../exportedJson/exportedAnomaly.json", anomalyAsJson);

        }

        private static void ExportPeopleWhichHaveNotBeenVictims(UnitOfWork unit)
        {
            var persons = unit.Persons
                .GetAll(p => p.Anomalies.Count == 0)
                .Select(p => new
                {
                    name = p.Name,
                    homePlanet = new { name = p.HomePlanet.Name }
                });
            string personsAsJson = JsonConvert.SerializeObject(persons, Formatting.Indented);
            File.WriteAllText("../../../exportedJson/exportedPersons.json", personsAsJson);
        }

        private static void ExportPlanetsWhichAreNotAnomalyOrigins(UnitOfWork unit)
        {
            var planets = unit.Planets
                .GetAll(p => p.OriginAnomalies.Count == 0)
                .Select(p => new { name = p.Name });
            string planetsAsJson = JsonConvert.SerializeObject(planets, Formatting.Indented);
            File.WriteAllText("../../../exportedJson/exportedPlanets.json", planetsAsJson);
        }
    }
}
