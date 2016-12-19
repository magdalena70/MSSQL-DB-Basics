using PhotographyWorkshops.Data;
using System.Linq;
using System.Xml.Linq;
using System;

namespace PhotographyWorkshops.ExportXml
{
    class Program
    {
        static void Main()
        {
            UnitOfWork unit = new UnitOfWork();
            ExportingToXML(unit);
        }

        private static void ExportingToXML(UnitOfWork unit)
        {
            PhotographersWithSameCameraMake(unit);
        }

        private static void PhotographersWithSameCameraMake(UnitOfWork unit)
        {
            var photographers = unit.Photographers
                .GetAll(ph => ph.PrimaryCamera.Make == ph.SecondaryCamera.Make)
                .Select(ph => new
                {
                    FullName = ph.FirstName + " " + ph.LastName,
                    PrimaryCamera = ph.PrimaryCamera.Make + " " + ph.PrimaryCamera.Model,
                    Lenses = ph.Lenses.Select(l => new
                    {
                        LensData = l.Make + " " + l.FocalLength + "mm f" + l.MaxAperture
                    })
                });

            XElement xml = new XElement("photographers");
            foreach (var photographer in photographers)
            {
                XElement photographerXelem = new XElement("photographer");
                photographerXelem.SetAttributeValue("name", photographer.FullName);
                photographerXelem.SetAttributeValue("primary-camera", photographer.PrimaryCamera);
                if (photographer.Lenses.Count() > 0)
                {
                    XElement lensesXelem = new XElement("lenses");
                    foreach (var lens in photographer.Lenses)
                    {
                        XElement lensXelem = new XElement("lens");
                        lensXelem.Value = lens.LensData;
                        lensesXelem.Add(lensXelem);
                    }

                    photographerXelem.Add(lensesXelem);
                }

                xml.Add(photographerXelem);
            }
           
            xml.Save("../../../exportedXml/same-cameras-photographers.xml");
        }
    }
}
