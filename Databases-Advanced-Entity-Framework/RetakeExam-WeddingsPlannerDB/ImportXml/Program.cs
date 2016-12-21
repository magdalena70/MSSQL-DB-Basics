using System;
using System.Xml.Linq;
using System.Xml.XPath;
using WeddingsPlanner.Data;
using WeddingsPlanner.Models;

namespace ImportXml
{
    public class Program
    {
        private const string VENUES_PATH = "../../../datasets/venues.xml";
        private const string PRESENTS_PATH = "../../../datasets/presents.xml";

        private const string ERROR_MESSAGE = "Error. Invalid data provided";

        static void Main(string[] args)
        {
            UnitOfWork unit = new UnitOfWork();
            ImportingDataFromXML(unit);
        }

        private static void ImportingDataFromXML(UnitOfWork unit)
        {
            //ImportVenues(unit);
            //ImportPresents(unit); //to do
        }

        private static void ImportPresents(UnitOfWork unit)
        {
            throw new NotImplementedException();
        }

        private static void ImportVenues(UnitOfWork unit)
        {
            var document = XDocument.Load(VENUES_PATH);
            var venues = document.Descendants("venue");
            foreach (var venueXelem in venues)
            {
                string name = venueXelem.Attribute("name").Value;
                int capacity = Int32.Parse(venueXelem.XPathSelectElement("capacity").Value);
                string town = venueXelem.XPathSelectElement("town").Value;

                Venue venue = new Venue()
                {
                    Name = name,
                    Capacity = capacity,
                    Town = town
                };

                unit.Venues.Add(venue);
                unit.Commit();

                Console.WriteLine($"Successfully imported {venue.Name}");
            }
        }
    }
}
