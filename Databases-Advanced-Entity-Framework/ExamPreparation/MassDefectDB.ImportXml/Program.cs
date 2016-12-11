using System;
using MassDefectDB.Data;
using System.Xml.Linq;
using System.Xml.XPath;
using MassDefectDB.Models;
using System.Collections.Generic;

namespace MassDefectDB.ImportXml
{
    class Program
    {
        private const string NEW_ANOMALIES_PATH = "../../../datasets/new-anomalies.xml";
        private const string ERROR_MESSAGE = "Error: Invalid data.";

        static void Main()
        {
            UnitOfWork unit = new UnitOfWork();
            ImportNewAnomaliesFromXml(unit);
        }

        private static void ImportNewAnomaliesFromXml(UnitOfWork unit)
        {
            XDocument xml = XDocument.Load(NEW_ANOMALIES_PATH);
            IEnumerable<XElement> anomalies = xml.Descendants("anomaly");
            foreach (XElement anomaly in anomalies)
            {
                var originPlanetAttr = anomaly.Attribute("origin-planet");
                var teleportPlanetAttr = anomaly.Attribute("teleport-planet");

                if (originPlanetAttr == null || teleportPlanetAttr == null)
                {
                    Console.WriteLine(ERROR_MESSAGE);
                    continue;
                }

                string originPlanetName = originPlanetAttr.Value;
                string teleportPlanetName = teleportPlanetAttr.Value;

                Planet teleportPlanet = unit.Planets.First(p => p.Name == teleportPlanetName);
                Planet originPlanet = unit.Planets.First(p => p.Name == originPlanetName);
                if (teleportPlanet == null && originPlanet == null)
                {
                    Console.WriteLine(ERROR_MESSAGE);
                    continue;
                }

                IList<Person> anomalyVictims = new List<Person>();
                IEnumerable<XElement> victims = anomaly.Descendants("victim");
                foreach (XElement victim in victims)
                {
                    var victimNameAttr = victim.Attribute("name");
                    if (victimNameAttr == null)
                    {
                        continue;
                    }

                    string victimName = victimNameAttr.Value;
                    Person personEntity = unit.Persons.First(p => p.Name == victimName);
                    if (personEntity == null)
                    {
                        continue;
                    }

                    anomalyVictims.Add(personEntity);
                }

                Anomaly anomalyEntity = new Anomaly()
                {
                    TeleportPlanet = teleportPlanet,
                    OriginPlanet = originPlanet,
                    Victims = anomalyVictims
                };

                unit.Anomalies.Add(anomalyEntity);
                unit.Commit();

                Console.WriteLine("Successfully imported anomaly.");

            }
        }
    }
}
