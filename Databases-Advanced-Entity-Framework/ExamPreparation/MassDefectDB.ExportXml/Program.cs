using System;
using MassDefectDB.Data;
using System.Linq;
using System.Collections.Generic;
using System.Xml.Linq;

namespace MassDefectDB.ExportXml
{
    class Program
    {
        static void Main()
        {
            UnitOfWork unit = new UnitOfWork();
            ExportAnomaliesToXml(unit);
        }

        private static void ExportAnomaliesToXml(UnitOfWork unit)
        {
            var anomalies = unit.Anomalies
                .GetAll()
                .Select(a => new
                {
                    id = a.Id,
                    originPlanetName = a.OriginPlanet.Name,
                    teleportPlanetName = a.TeleportPlanet.Name,
                    victims = a.Victims.Select(v => v.Name)
                })
                .OrderBy(a => a.id);

            XElement anomaliesXelem = new XElement("anomalies");
            foreach (var anomaly in anomalies)
            {
                XElement anomalyXelem = new XElement("anomaly");
                anomalyXelem.SetAttributeValue("id", anomaly.id);
                anomalyXelem.SetAttributeValue("origin-planet", anomaly.originPlanetName);
                anomalyXelem.SetAttributeValue("teleport-planet", anomaly.teleportPlanetName);

                XElement victimsXelem = new XElement("victims");
                foreach (var victim in anomaly.victims)
                {
                    XElement victimXelem = new XElement("victim");
                    victimXelem.SetAttributeValue("name", victim);

                    victimsXelem.Add(victimXelem);
                }

                anomalyXelem.Add(victimsXelem);
                anomaliesXelem.Add(anomalyXelem);
            }

            anomaliesXelem.Save("../../../exportedXml/exportedAnomalies.xml");
        }
    }
}
