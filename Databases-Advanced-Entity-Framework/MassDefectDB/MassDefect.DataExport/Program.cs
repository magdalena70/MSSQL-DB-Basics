using System.Linq;
using MassDefect.Data;
using Newtonsoft.Json;
using System.IO;
using System.Xml.Linq;

namespace MassDefect.DataExport
{
    class Program
    {
        static void Main()
        {
            var context = new MassDefectContext();

            //export to json
            ExportPlanetsWichAreNotAnomalyOrigins(context);
            ExportPeopleWichHaveNotBeenVictims(context);
            ExportTopAnomaly(context);

            //export to xml
            ExportAnomaliesAndThePeopleAffectedByThemToXml(context);
        }

        private static void ExportAnomaliesAndThePeopleAffectedByThemToXml(MassDefectContext context)
        {
            var anomalies = context.Anomalies
                .Select(a => new
                {
                    id = a.Id,
                    originPlanetName = a.OriginPlanet.Name,
                    teleportPlanetName = a.TeleportPlanet.Name,
                    victims = a.Victims.Select(v => v.Name).ToList()
                })
                .OrderBy(a => a.id);

            var xmlDocument = new XElement("anomalies");
            foreach (var anomaly in anomalies)
            {
                var anomalyNode = new XElement("anomaly");
                anomalyNode.Add(new XAttribute("id", anomaly.id));
                anomalyNode.Add(new XAttribute("origin-planet", anomaly.originPlanetName));
                anomalyNode.Add(new XAttribute("teleport-planet", anomaly.teleportPlanetName));
                var victimsNode = new XElement("victims");

                foreach (var victim in anomaly.victims)
                {
                    var victimNode = new XElement("victim");
                    victimNode.Add(new XAttribute("name", victim));
                    victimsNode.Add(victimNode);
                }

                anomalyNode.Add(victimsNode);
                xmlDocument.Add(anomalyNode);
                xmlDocument.Save("../../exportedXml/anomalies.xml");
            }

            //Console.WriteLine(xmlDocument);
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
