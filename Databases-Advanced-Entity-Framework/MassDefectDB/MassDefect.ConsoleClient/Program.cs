using System;
using MassDefect.Data;
using System.IO;
using Newtonsoft.Json;
using System.Collections.Generic;
using MassDefect.ConsoleClient.LocalModels;
using System.Data.Entity.Migrations;
using MassDefect.Models;
using System.Linq;
using System.Xml.Linq;
using System.Xml.XPath;

namespace MassDefect.ConsoleClient
{
    class Program
    {
        static void Main()
        {
            //var context = new MassDefectContext();
            //context.Database.Initialize(true);

            //InportSolarSystems();
            //ImportStars();
            //ImportPlanets();
            //ImportPersons();
            //ImportAnomalies();
            //ImportAnomalyVictims();

            //ImportXml();
        }

        private static void ImportXml()
        {
            try
            {
                const string NEW_ANOMALIES_PATH = "../../../datasets/new-anomalies.xml";
                var xml = XDocument.Load(NEW_ANOMALIES_PATH);
                var anomalies = xml.XPathSelectElements("anomalies/anomaly");
                var context = new MassDefectContext();

                foreach (var anomaly in anomalies)
                {
                    InportAnomalyAndVictims(anomaly, context);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: Invalid data.");
                //throw new Exception(ex.Message);
            }
        }


        private static void InportAnomalyAndVictims(XElement anomalyNode, MassDefectContext context)
        {
            var originPlanetName = anomalyNode.Attribute("origin-planet");
            var teleportPlanetName = anomalyNode.Attribute("teleport-planet");

            if (originPlanetName != null && teleportPlanetName != null)
            {
                var anomalyEntity = new Anomalie()
                {
                    OriginPlanet = GetPlanetByName(originPlanetName.Value, context),
                    TeleportPlanet = GetPlanetByName(teleportPlanetName.Value, context)
                };

                var victims = anomalyNode.XPathSelectElements("victims/victim");
                foreach (var victim in victims)
                {
                    ImportVictim(victim, context, anomalyEntity);
                }

                context.Anomalies.Add(anomalyEntity);
                context.SaveChanges();
                Console.WriteLine("Successfully imported anomaly.");
                
            }
            else
            {
                Console.WriteLine("Error: Invalid data.");
            }
        }

        private static void ImportVictim(XElement victim, MassDefectContext context, Anomalie anomalyEntity)
        {
            var victimName = victim.Attribute("name").Value;
            var anomalyEntityVictim = context.Persons.FirstOrDefault(p => p.Name == victimName);
            
            anomalyEntity.Victims.Add(anomalyEntityVictim);
        }

        private static Planet GetPlanetByName(string name, MassDefectContext context)
        {
            var planet = context.Planets.FirstOrDefault(p => p.Name == name);
            return planet != null ? planet : new Planet() { Name = name };        
        }

        private static void InportSolarSystems()
        {
            try
            {
                const string SOLAR_SYSTEMS_ROUTE = "../../../datasets/solar-systems.json";
                var context = new MassDefectContext();
                var json = File.ReadAllText(SOLAR_SYSTEMS_ROUTE);
                var solarSystems = JsonConvert.DeserializeObject<IEnumerable<SolarSystemDTO>>(json);
                foreach (var solarSystem in solarSystems)
                {
                    if (solarSystem.Name != null)
                    {
                        context.SolarSystems.AddOrUpdate(ss => ss.Name,
                        new SolarSystem()
                        {
                            Name = solarSystem.Name
                        });
                        context.SaveChanges();
                        Console.WriteLine($"Successfully imported Solar System {solarSystem.Name}.");
                    }
                    else
                    {
                        continue;
                    }

                }               
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: Invalid data.");
                //throw new Exception(ex.Message);
            }           
        }

        private static void ImportStars()
        {
            try
            {
                const string STARS_ROUTE = "../../../datasets/stars.json";
                var context = new MassDefectContext();
                var json = File.ReadAllText(STARS_ROUTE);
                var stars = JsonConvert.DeserializeObject<IEnumerable<StarDTO>>(json);

                foreach (var star in stars)
                {
                    if (star.Name != null && star.SolarSystem != null)
                    {
                        var solarSistem = context.SolarSystems.FirstOrDefault(ss => ss.Name == star.SolarSystem);
                        if (solarSistem != null)
                        {
                            context.Stars.AddOrUpdate(s => s.Name, new Star()
                            {
                                Name = star.Name,
                                SolarSystem = solarSistem
                            });
                        }
                        else
                        {
                            context.Stars.AddOrUpdate(s => s.Name, new Star()
                            {
                                Name = star.Name,
                                SolarSystem = new SolarSystem()
                                {
                                    Name = star.SolarSystem
                                }
                            });
                        }   

                        context.SaveChanges();
                        Console.WriteLine($"Successfully imported Star {star.Name}.");
                    }
                    else
                    {
                        Console.WriteLine("Error: Invalid data.");
                        continue;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: Invalid data.");
                //throw new Exception(ex.Message);
            }
        }

        private static void ImportPlanets()
        {
            try
            {
                const string PLANETS_ROUTE = "../../../datasets/planets.json";
                var context = new MassDefectContext();
                var json = File.ReadAllText(PLANETS_ROUTE);
                var planets = JsonConvert.DeserializeObject<IEnumerable<PlanetDTO>>(json);

                foreach (var planet in planets)
                {
                   
                    if (planet.Name != null && planet.Sun != null && planet.SolarSystem != null)
                    {
                        var sun = context.Stars.FirstOrDefault(s => s.Name == planet.Sun);
                        var solarSystem = context.SolarSystems.FirstOrDefault(ss => ss.Name == planet.SolarSystem);
        
                        context.Planets.AddOrUpdate(p => p.Name, new Planet()
                        {
                            Name = planet.Name,
                            Sun = sun != null ? sun : new Star() {Name = planet.Name },
                            SolarSystem = solarSystem != null ? solarSystem : new SolarSystem() { Name = planet.SolarSystem}
                        });

                        context.SaveChanges();
                        Console.WriteLine($"Successfully imported Planet {planet.Name}.");
                    }
                    else
                    {
                        Console.WriteLine("Error: Invalid data.");
                        continue;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: Invalid data.");
                //throw new Exception(ex.Message);
            }
        }

        private static void ImportPersons()
        {
            try
            {
                const string PERSONS_ROUTE = "../../../datasets/persons.json";
                var context = new MassDefectContext();
                var json = File.ReadAllText(PERSONS_ROUTE);
                var persons = JsonConvert.DeserializeObject<IEnumerable<PersonDTO>>(json);

                foreach (var person in persons)
                {
                   
                    if (person.Name != null && person.HomePlanet != null)
                    {
                        var homePlanet = context.Planets.FirstOrDefault(p => p.Name == person.HomePlanet);

                        context.Persons.AddOrUpdate(p => p.Name, new Person()
                        {
                            Name = person.Name,
                            HomePlanet = homePlanet != null ? homePlanet : new Planet() { Name = person.HomePlanet}
                        });

                        context.SaveChanges();
                        Console.WriteLine($"Successfully imported Person {person.Name}.");
                    }
                    else
                    {
                        Console.WriteLine("Error: Invalid data.");
                        continue;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: Invalid data.");
                //throw new Exception(ex.Message);
            }
        }

        private static void ImportAnomalies()
        {
            try
            {
                const string ANOMALIES_ROUTE = "../../../datasets/anomalies.json";
                var context = new MassDefectContext();
                var json = File.ReadAllText(ANOMALIES_ROUTE);
                var anomalies = JsonConvert.DeserializeObject<IEnumerable<AnomalieDTO>>(json);

                foreach (var anomalie in anomalies)
                {
                    
                    if (anomalie.OriginPlanet != null && anomalie.TeleportPlanet != null)
                    {
                        var originPlanet = context.Planets.FirstOrDefault(p => p.Name == anomalie.OriginPlanet);
                        var teleportPlanet = context.Planets.FirstOrDefault(p => p.Name == anomalie.TeleportPlanet);

                        context.Anomalies.AddOrUpdate(new Anomalie()
                        {
                            OriginPlanet = originPlanet != null ? originPlanet : new Planet() { Name = anomalie.OriginPlanet },
                            TeleportPlanet = teleportPlanet != null ? teleportPlanet : new Planet() { Name = anomalie.TeleportPlanet}
                        });

                        context.SaveChanges();
                        Console.WriteLine($"Successfully imported anomaly.");
                    }
                    else
                    {
                        Console.WriteLine("Error: Invalid data.");
                        continue;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: Invalid data.");
                //throw new Exception(ex.Message);
            }
        }

        private static void ImportAnomalyVictims()
        {
            try
            {
                const string ANOMALY_VICTIMS_ROUTE = "../../../datasets/anomaly-victims.json";
                var context = new MassDefectContext();
                var json = File.ReadAllText(ANOMALY_VICTIMS_ROUTE);
                var anomalyVictims = JsonConvert.DeserializeObject<IEnumerable<AnomalyVictimsDTO>>(json);

                foreach (var item in anomalyVictims)
                {
                    if (item.Id.ToString() != null  && item.Person != null)
                    {
                        var anomaly = context.Anomalies.Find(item.Id);
                        var victim = context.Persons.FirstOrDefault(p => p.Name == item.Person);

                        if (anomaly != null)
                        {
                            anomaly.Victims.Add(
                                victim != null ? victim : new Person() { Name = item.Person }
                                );
                        }

                        context.SaveChanges();
                    }
                    else
                    {
                        Console.WriteLine("Error: Invalid data.");
                        continue;
                    }
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Error: Invalid data.");
                //throw new Exception(ex.Message);
            }
        }
    }
}
