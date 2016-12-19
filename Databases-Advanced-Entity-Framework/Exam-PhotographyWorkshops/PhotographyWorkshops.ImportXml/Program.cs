using System;
using PhotographyWorkshops.Data;
using System.Xml.Linq;
using PhotographyWorkshops.Models;
using System.Linq;
using System.Xml.XPath;
using System.Data.Entity.Validation;

namespace PhotographyWorkshops.ImportXml
{
    class Program
    {
        private const string ACCESSORIES_PATH = "../../../datasets/accessories.xml";
        private const string WORKSHOPS_PATH = "../../../datasets/workshops.xml";

        private const string ERROR_MESSAGE = "Error. Invalid data provided";

        static void Main()
        {
            UnitOfWork unit = new UnitOfWork();
            //ImportDataFromXml(unit);
        }

        private static void ImportDataFromXml(UnitOfWork unit)
        {
            ImportAccessories(unit);
            ImportWorkshops(unit);
        }

        private static void ImportWorkshops(UnitOfWork unit)
        {
            var document = XDocument.Load(WORKSHOPS_PATH);
            var workshops = document.Descendants("workshop");
            foreach (var workshopXelem in workshops)
            {
                XAttribute workshopNameAttr = workshopXelem.Attribute("name");
                XAttribute workshopLocationAttr = workshopXelem.Attribute("location");
                XAttribute workshopPriceAttr = workshopXelem.Attribute("price");
                XElement trainerXelem = workshopXelem.XPathSelectElement("trainer");

                if (workshopNameAttr == null || workshopLocationAttr == null ||
                    workshopPriceAttr == null || trainerXelem == null)
                {
                    Console.WriteLine(ERROR_MESSAGE);
                    continue;
                }

                XAttribute workshopStartDateAttr = workshopXelem.Attribute("start-date");
                XAttribute workshopEndDateAttr = workshopXelem.Attribute("end-date");
                var trainer = GetTrainerByName(unit, trainerXelem);
                if (trainer == null)
                {
                    Console.WriteLine(ERROR_MESSAGE);
                    continue;
                }

                Workshop workshop = new Workshop()
                {
                    Name = workshopNameAttr.Value,
                    Location = workshopLocationAttr.Value,
                    PricePerParticipant = decimal.Parse(workshopPriceAttr.Value),
                    StartDate = GetDate(workshopStartDateAttr),
                    EndDate = GetDate(workshopEndDateAttr),
                    Trainer = trainer
                };

                var participants = workshopXelem.Descendants("participant");
                foreach (var participantXelem in participants)
                {
                    XAttribute firstNameAttr = participantXelem.Attribute("first-name");
                    XAttribute lastNameAttr = participantXelem.Attribute("last-name");
                    if (firstNameAttr == null || lastNameAttr == null)
                    {
                        Console.WriteLine(ERROR_MESSAGE);
                        continue;
                    }

                    Photographer participant = GetParticipantByName(unit, firstNameAttr, lastNameAttr);
                    if (participant == null)
                    {
                        continue;
                    }

                    workshop.Participants.Add(participant);
                }

                try
                {
                    unit.Workshops.Add(workshop);
                    unit.Commit();

                    Console.WriteLine($"Successfully imported {workshop.Name}");
                }
                catch (DbEntityValidationException)
                {
                    unit.Workshops.Remove(workshop);
                    Console.WriteLine(ERROR_MESSAGE);
                }
            }
        }

        private static DateTime? GetDate(XAttribute workshopDateAttr)
        {
            if (workshopDateAttr == null)
            {
                return null;
            }
            else
            {
                return DateTime.Parse(workshopDateAttr.Value);
            }
        }

        private static Photographer GetParticipantByName(UnitOfWork unit, XAttribute firstNameAttr, XAttribute lastNameAttr)
        {
            string firstName = firstNameAttr.Value;
            string lastName = lastNameAttr.Value;
            Photographer participant = unit.Photographers
                .First(ph => ph.FirstName == firstName && ph.LastName == lastName);

            return participant;
        }

        private static Photographer GetTrainerByName(UnitOfWork unit, XElement trainerXelem)
        {
            string[] names = trainerXelem.Value.Split(' ');
            string firstName = names[0].Trim();
            string lastName = names[1].Trim();
            Photographer trainer = unit.Photographers
                .First(ph => ph.FirstName == firstName && ph.LastName == lastName);
            
            return trainer;
        }

        private static void ImportAccessories(UnitOfWork unit)
        {
            var document = XDocument.Load(ACCESSORIES_PATH);
            var accessories = document.Descendants("accessory");
            foreach (var accessoryXelem in accessories)
            {
                XAttribute accessoryAttr = accessoryXelem.Attribute("name");
                if (accessoryAttr == null)
                {
                    Console.WriteLine(ERROR_MESSAGE);
                    continue;
                }

                string accessoryName = accessoryAttr.Value;
                if (accessoryName == null)
                {
                    Console.WriteLine(ERROR_MESSAGE);
                    continue;
                }

                Accessory accessory = new Accessory()
                {
                    Name = accessoryName,
                    Owner = GetRandomOwner(unit)
                };

                unit.Accessories.Add(accessory);
                unit.Commit();

                Console.WriteLine($"Successfully imported {accessory.Name}");
            }
        }

        private static Photographer GetRandomOwner(UnitOfWork unit)
        {
            Random rnd = new Random();
            int randomId = rnd.Next(1, unit.Photographers.GetAll().Count());
            Photographer owner = unit.Photographers.Find(randomId);
            return owner;
        }
    }
}
