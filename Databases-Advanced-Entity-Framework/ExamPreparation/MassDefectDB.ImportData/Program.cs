using System;
using MassDefectDB.Data;
using System.IO;
using System.Collections.Generic;
using MassDefectDB.Dtos;
using Newtonsoft.Json;
using MassDefectDB.Models;
using AutoMapper;

namespace MassDefectDB.ImportData
{
    class Program
    {
        private const string SOLAR_SYSTEMS_PATH = "../../../datasets/solar-systems.json";
        private const string STARS_PATH = "../../../datasets/stars.json";
        private const string PLANETS_PATH = "../../../datasets/planets.json";
        private const string PERSONS_PATH = "../../../datasets/persons.json";
        private const string ANOMALIES_PATH = "../../../datasets/anomalies.json";
        private const string ANOMALY_VICTIMS_PATH = "../../../datasets/anomaly-victims.json";

        private const string ERROR_MESSAGE = "Error: Invalid data.";
        private const string SUCCESS_MESSAGE = "Successfully imported {0} {1}.";

        static void Main()
        {
            MassDefectContext context = new MassDefectContext();
            context.Database.Initialize(true);
            UnitOfWork unit = new UnitOfWork();
            ConfigureMapping(unit);

            ImportSolarSystems(unit);
            ImportStars(unit);
            ImportPlanets(unit);
            ImportPersons(unit);
            ImportAnomalies(unit);
            ImportAnomalyVictims(unit);
        }

        private static void ConfigureMapping(UnitOfWork unit)
        {
            Mapper.Initialize(m => 
            {
                m.CreateMap<SolarSystemDto, SolarSystem>();

                m.CreateMap<StarDto, Star>()
                    .ForMember(star => star.SolarSystem, exp => exp.MapFrom(starDto => 
                            unit.SolarSystems.First(solarSystem => solarSystem.Name == starDto.SolarSystem)));

                m.CreateMap<PlanetDto, Planet>()
                    .ForMember(planet => planet.SolarSystem, exp => exp.MapFrom(planetDto =>
                            unit.SolarSystems.First(solarSystem => solarSystem.Name == planetDto.SolarSystem)))
                    .ForMember(planet => planet.Sun, exp => exp.MapFrom(planetDto =>
                            unit.Stars.First(sun => sun.Name == planetDto.Sun)));

                m.CreateMap<PersonDto, Person>()
                    .ForMember(person => person.HomePlanet, exp => exp.MapFrom(personDto =>
                            unit.Planets.First(planet => planet.Name == personDto.HomePlanet)));

                m.CreateMap<AnomalyDto, Anomaly>()
                    .ForMember(anomaly => anomaly.OriginPlanet, exp => exp.MapFrom(anomalyDto =>
                            unit.Planets.First(planet => planet.Name == anomalyDto.OriginPlanet)))
                    .ForMember(anomaly => anomaly.TeleportPlanet, exp => exp.MapFrom(anomalyDto =>
                            unit.Planets.First(planet => planet.Name == anomalyDto.TeleportPlanet)));
            });
        }


        private static void ImportSolarSystems(UnitOfWork unit)
        {
            string json = File.ReadAllText(SOLAR_SYSTEMS_PATH);
            IEnumerable<SolarSystemDto> solarSystemDtos = JsonConvert
                .DeserializeObject<IEnumerable<SolarSystemDto>>(json);
            foreach (var solarSystemDto in solarSystemDtos)
            {
                if (solarSystemDto.Name == null)
                {
                    Console.WriteLine(ERROR_MESSAGE);
                    continue;
                }
                
                SolarSystem solarSystem = Mapper.Map<SolarSystem>(solarSystemDto);
                unit.SolarSystems.Add(solarSystem);
                unit.Commit();
                 
                Console.WriteLine(SUCCESS_MESSAGE, solarSystem.GetType().Name, solarSystem.Name);               
            }
        }

        private static void ImportStars(UnitOfWork unit)
        {
            string json = File.ReadAllText(STARS_PATH);
            IEnumerable<StarDto> starsDtos = JsonConvert
                .DeserializeObject<IEnumerable<StarDto>>(json);
            foreach (var starDto in starsDtos)
            {
                if (starDto.Name == null || starDto.SolarSystem == null)
                {
                    Console.WriteLine(ERROR_MESSAGE);
                    continue;
                }

                Star star = Mapper.Map<Star>(starDto);
                if (star.SolarSystem == null)
                {
                    Console.WriteLine(ERROR_MESSAGE);
                    continue;
                }

                unit.Stars.Add(star);
                unit.Commit();

                Console.WriteLine(SUCCESS_MESSAGE, star.GetType().Name, star.Name);
            }
        }

        private static void ImportPlanets(UnitOfWork unit)
        {
            string json = File.ReadAllText(PLANETS_PATH);
            IEnumerable<PlanetDto> planetsDtos = JsonConvert
                .DeserializeObject<IEnumerable<PlanetDto>>(json);
            foreach (var planetDto in planetsDtos)
            {
                if (planetDto.Name == null || planetDto.SolarSystem == null || planetDto.Sun == null)
                {
                    Console.WriteLine(ERROR_MESSAGE);
                    continue;
                }

                Planet planet = Mapper.Map<Planet>(planetDto);
                if (planet.SolarSystem == null || planet.Sun == null)
                {
                    Console.WriteLine(ERROR_MESSAGE);
                    continue;
                }

                unit.Planets.Add(planet);
                unit.Commit();

                Console.WriteLine(SUCCESS_MESSAGE, planet.GetType().Name, planet.Name);
            }
        }

        private static void ImportPersons(UnitOfWork unit)
        {
            string json = File.ReadAllText(PERSONS_PATH);
            IEnumerable<PersonDto> personsDtos = JsonConvert
                .DeserializeObject<IEnumerable<PersonDto>>(json);
            foreach (var personDto in personsDtos)
            {
                if (personDto.Name == null || personDto.HomePlanet == null)
                {
                    Console.WriteLine(ERROR_MESSAGE);
                    continue;
                }

                Person person = Mapper.Map<Person>(personDto);
                if (person.Name == null || person.HomePlanet == null)
                {
                    Console.WriteLine(ERROR_MESSAGE);
                    continue;
                }

                unit.Persons.Add(person);
                unit.Commit();

                Console.WriteLine(SUCCESS_MESSAGE, person.GetType().Name, person.Name);
            }
        }

        private static void ImportAnomalies(UnitOfWork unit)
        {
            string json = File.ReadAllText(ANOMALIES_PATH);
            IEnumerable<AnomalyDto> anomaliesDtos = JsonConvert
                .DeserializeObject<IEnumerable<AnomalyDto>>(json);
            foreach (var anomalyDto in anomaliesDtos)
            {
                if (anomalyDto.OriginPlanet == null || anomalyDto.TeleportPlanet == null)
                {
                    Console.WriteLine(ERROR_MESSAGE);
                    continue;
                }

                Anomaly anomaly = Mapper.Map<Anomaly>(anomalyDto);
                if (anomaly.TeleportPlanet == null || anomaly.OriginPlanet == null)
                {
                    Console.WriteLine(ERROR_MESSAGE);
                    continue;
                }

                unit.Anomalies.Add(anomaly);
                unit.Commit();

                Console.WriteLine("Successfully imported anomaly.");
            }

        }

        private static void ImportAnomalyVictims(UnitOfWork unit)
        {
            string json = File.ReadAllText(ANOMALY_VICTIMS_PATH);
            IEnumerable<AnomalyVictimsDto> anomalyVictimsDtos = JsonConvert
                .DeserializeObject<IEnumerable<AnomalyVictimsDto>>(json);
            foreach (var anomalyVictimDto in anomalyVictimsDtos)
            {
                if (anomalyVictimDto.Id < 1 || anomalyVictimDto.Person == null)
                {
                    Console.WriteLine(ERROR_MESSAGE);
                    continue;
                }

                Anomaly anomaly = unit.Anomalies.First(a => a.Id == anomalyVictimDto.Id);
                Person victim = unit.Persons.First(p => p.Name == anomalyVictimDto.Person);
                if (anomaly == null || victim == null)
                {
                    Console.WriteLine(ERROR_MESSAGE);
                    continue;
                }

                anomaly.Victims.Add(victim);
                unit.Commit();
            }
        }
    }
}
