using AutoMapper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.IO;
using WeddingsPlanner.Data;
using WeddingsPlanner.InportJson.Dtos;
using WeddingsPlanner.Models;

namespace WeddingsPlanner.InportJson
{
    public class Program
    {
        private const string AGENCIES_PATH = "../../../datasets/agencies.json";
        private const string PEOPLE_PATH = "../../../datasets/people.json";
        private const string WEDDINGS_PATH = "../../../datasets/weddings.json";

        private const string ERROR_MESSAGE = "Error. Invalid data provided";

        static void Main()
        {
            var context = new WeddingsPlannerContext();
            context.Database.Initialize(true);

            UnitOfWork unit = new UnitOfWork();
            ConfigureMapping(unit);

            ImportingData(unit);
        }

        private static void ConfigureMapping(UnitOfWork unit)
        {
            Mapper.Initialize(m => 
            {
                //
            });
        }

        private static void ImportingData(UnitOfWork unit)
        {
            //ImportAgencies(unit);
            //ImportPeople(unit);
            //ImportWeddingsAndInvitations(unit);
        }

        private static void ImportWeddingsAndInvitations(UnitOfWork unit)
        {
            string json = File.ReadAllText(WEDDINGS_PATH);
            IEnumerable<WeddingDto> weddingsDtos = JsonConvert
               .DeserializeObject<IEnumerable<WeddingDto>>(json);
            foreach (var weddingDto in weddingsDtos)
            {
                if (weddingDto.Bride == null ||
                    weddingDto.Bridegroom == null ||
                    weddingDto.Date == null ||
                    weddingDto.Agency == null)
                {
                    Console.WriteLine(ERROR_MESSAGE);
                    continue;
                }

                Wedding wedding = new Wedding()
                {
                    Bride = GetPerson(weddingDto.Bride, unit),
                    Bridegroom = GetPerson(weddingDto.Bridegroom, unit),
                    Date = DateTime.Parse(weddingDto.Date),
                    Agency = GetAgency(weddingDto.Agency, unit)
                };

                foreach (var guest in weddingDto.Guests)
                {
                    Person validGuest = GetPerson(guest.Name, unit);
                    if (validGuest == null)
                    {
                        continue;
                    }

                    wedding.Guests.Add(validGuest);
                }

                unit.Weddings.Add(wedding);
                unit.Commit();

                Console.WriteLine($"Successfully imported wedding of {wedding.Bride.FirstName} and {wedding.Bridegroom.FirstName}");
            }

        }

        private static Agency GetAgency(string agencyName, UnitOfWork unit)
        {
            Agency agency = unit.Agencies
                .First(a => a.Name == agencyName);

            return agency;
        }

        private static Person GetPerson(string brideFullName, UnitOfWork unit)
        {
            string[] names = brideFullName.Split(' ');
            string firstName = names[0];
            string middleInitial = names[1];
            string lastName = names[2];

            Person person = unit.People
                .First(p => p.FirstName == firstName &&
                            p.MiddleNameInitial == middleInitial &&
                            p.LastName == lastName);

            return person;
        }

        private static void ImportPeople(UnitOfWork unit)
        {
            string json = File.ReadAllText(PEOPLE_PATH);
            IEnumerable<PersonDto> peopleDtos = JsonConvert
               .DeserializeObject<IEnumerable<PersonDto>>(json);
            foreach (var personDto in peopleDtos)
            {
                if (personDto.FirstName == null ||
                    personDto.LastName == null ||
                    personDto.MiddleInitial == null)
                {
                    Console.WriteLine(ERROR_MESSAGE);
                    continue;
                }

                Person person = new Person()
                {
                    FirstName = personDto.FirstName,
                    MiddleNameInitial = personDto.MiddleInitial,
                    LastName = personDto.LastName,
                    Gender = GetGender(personDto.Gender),
                    Birthdate = GetDate(personDto.Birthday),
                    Phone = personDto.Phone,
                    Email = personDto.Email
                };

                try
                {
                    unit.People.Add(person);
                    unit.Commit();

                    Console.WriteLine($"Successfully imported {person.FullName}");
                }
                catch (DbEntityValidationException)
                {
                    unit.People.Remove(person);
                    Console.WriteLine(ERROR_MESSAGE);
                }
            }
        }

        private static DateTime? GetDate(string birthday)
        {
            if (birthday != null)
            {
                return DateTime.Parse(birthday);
            }
            else
            {
                return null;
            }           
        }

        private static Gender GetGender(string genderDto)
        {
            if (genderDto != null)
            {
                if (genderDto == "Male")
                {
                    return Gender.Male;
                }

                if (genderDto == "Female")
                {
                    return Gender.Female;
                }

                return Gender.NotSpecified;
            }

            return Gender.NotSpecified;
        }

        private static void ImportAgencies(UnitOfWork unit)
        {
            string json = File.ReadAllText(AGENCIES_PATH);
            IEnumerable<Agency> agencies = JsonConvert
               .DeserializeObject<IEnumerable<Agency>>(json);
            foreach (var agencyJson in agencies)
            {
                Agency agency = Mapper.Map<Agency>(agencyJson);
                unit.Agencies.Add(agency);
                unit.Commit();

                Console.WriteLine($"Successfully imported {agency.Name}");
            }
        }
    }
}
