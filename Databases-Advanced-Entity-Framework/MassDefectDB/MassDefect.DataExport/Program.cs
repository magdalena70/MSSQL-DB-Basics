using System;
using System.Linq;
using MassDefect.Data;
using Newtonsoft.Json;
using System.IO;

namespace MassDefect.DataExport
{
    class Program
    {
        static void Main()
        {
            var context = new MassDefectContext();

            ExportPlanetsWichAreNotAnomalyOrigins(context);
            ExportPeopleWichHaveNotBeenVictims(context);
            ExportTopAnomaly(context);
        }

        private static void ExportPlanetsWichAreNotAnomalyOrigins(MassDefectContext context)
        {
            var exportedPlanets = context.Planets
                .Where(p => !p.OriginAnomalies.Any())
                .Select(p => new
                {
                    name = p.Name
                });

            var planetAsJson = JsonConvert.SerializeObject(exportedPlanets, Formatting.Indented);
            //Console.WriteLine(planetAsJson);
            File.WriteAllText("../../exportedJson/planets.json", planetAsJson);
        }

        private static void ExportPeopleWichHaveNotBeenVictims(MassDefectContext context)
        {
            var people = context.Persons
                .Where(p => p.Anomalies.Count == 0)
                .Select(p => new
                {
                    name = p.Name,
                    homePlanet = new { name = p.HomePlanet.Name}
                });

            var peopleAsJson = JsonConvert.SerializeObject(people, Formatting.Indented);
            //Console.WriteLine(peopleAsJson);
            File.WriteAllText("../../exportedJson/people.json", peopleAsJson);
        }

        private static void ExportTopAnomaly(MassDefectContext context)
        {
            var topAnomaly = context.Anomalies
                .OrderByDescending(a => a.Victims.Count)
                .Take(1)
                .Select(a => new
                {
                    id = a.Id,
                    originPlanet = new { name = a.OriginPlanet.Name},
                    teleportPlanet = new { name = a.TeleportPlanet.Name},
                    victimsCount = a.Victims.Count
                });

            var anomalyAsJson = JsonConvert.SerializeObject(topAnomaly, Formatting.Indented);
            //Console.WriteLine(anomalyAsJson);
            File.WriteAllText("../../exportedJson/topAnomaly.json", anomalyAsJson);
        }
    }
}
