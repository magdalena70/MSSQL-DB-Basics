using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using Vehicles.Data;
using Vehicles.Models.MotorVehiclesModels;
using Vehicles.Models.NonMotorVehiclesModels;

namespace Vehicles.ConsoleClient
{
    class Program
    {
        static void Main()
        {
            try
            {
                var context = new VehiclesContext();

                InsertVehicles(context);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private static void InsertVehicles(VehiclesContext context)
        {
            context.Vehicles.AddOrUpdate(v => v.Model,
                    new Car()
                    {
                        Manufacturer = "Manufacturer QWERT",
                        Model = "QWE1234-AA",
                        MaxSpeed = 300,
                        EngineType = "qwe9-GG-555",
                        NumberOfDoors = 2,
                        NumberOfEngines = 1,
                        InformationInsurance = "SOME iNFO....ASDFGasdfghj....",
                        Price = 456.456m,
                        TankCapacity = 234
                    },
                    new Locomotive()
                    {
                        LocomotiveModel = "1234-oiSDF-uy",
                        Manufacturer = "QWERTY",
                        EngineType = "AA-4321-G",
                        MaxSpeed = 550,
                        NumberOfEngines = 4,
                        Model = "zxcvbnm",
                        Power = 600,
                        Price = 4663.999m,
                        TankCapacity = 700,
                        Train = new Train()
                        {
                            EngineType = "ASDF-777",
                            Manufacturer = "QWERTY",
                            Model = "QQ-12120",
                            MaxSpeed = 550,
                            NumberOfEngines = 4,
                            Price = 4663.999m,
                            TankCapacity = 999,
                            numberOfCarriages = 8
                        }
                    },
                    new RestaurantCarriage()
                    {
                        Manufacturer = "QWERTY-99",
                        Model = "POIU",
                        MaxSpeed = 20,
                        PassengersSeatsCapacity = 300,
                        Price = 788.99M,
                        TablesCount = 100,
                        Train = new Train()
                        {
                            EngineType = "poiuyt",
                            Manufacturer = "qwer zxcvbn",
                            MaxSpeed = 400,
                            Model = "ASDFGH",
                            numberOfCarriages = 5,
                            NumberOfEngines = 6,
                            Price = 10023.55m,
                            TankCapacity = 66
                        }
                    });

            context.SaveChanges();
        }
    }
}
