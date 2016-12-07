using System;
using CarDealerDB.Data;
using System.IO;
using Newtonsoft.Json;
using System.Collections.Generic;
using CarDealerDB.Models;
using System.Data.Entity.Migrations;
using System.Linq;

namespace CarDealerDB.ConsoleClient
{
    class Program
    {
        static void Main()
        {
            var context = new CarDealerContext();

            #region//Import Data
            //ImportSuppliers(context);
            //ImportParts(context);
            //ImportCars(context);
            //ImportCustomers(context);
            //ImportSales(context);
            #endregion

            #region//Query and Export Data
            //OrderedCustomers(context);
            //CarsFromMakeToyota(context);
            //LocalSuppliers(context);
            //CarsWithTheirListOfParts(context);
            //TotalSalesByCustomer(context);
            //SalesWithAppliedDiscount(context);
            #endregion

        }

        #region//Import Data
        private static void ImportSuppliers(CarDealerContext context)
        {
            var json = File.ReadAllText("../../../datasets/suppliers.json");
            var suppliers = JsonConvert.DeserializeObject<IEnumerable<Supplier>>(json);

            foreach (var supplier in suppliers)
            {
                if (supplier.Name != null)
                {
                    context.Suppliers.AddOrUpdate(s => s.Name,
                        new Supplier()
                        {
                            Name = supplier.Name,
                            IsImporter = supplier.IsImporter
                        });

                    context.SaveChanges();
                    //Console.WriteLine($"Supplier {supplier.Name} - {supplier.IsImporter}.");
                }
            }

            Console.WriteLine("Successfully imported Suppliers.");
        }

        private static void ImportParts(CarDealerContext context)
        {
            var json = File.ReadAllText("../../../datasets/parts.json");
            var parts = JsonConvert.DeserializeObject<IEnumerable<Part>>(json);

            foreach (var part in parts)
            {
                if (part.Name != null && part.Price > 0)
                {
                    Random rnd = new Random();

                    int suppliersCount = context.Suppliers.Count();
                    context.Parts.AddOrUpdate(p => p.Name,
                        new Part()
                        {
                            Name = part.Name,
                            Price = part.Price,
                            Quantity = part.Quantity,
                            Supplier = context.Suppliers.Find(rnd.Next(1, suppliersCount))
                        });

                    context.SaveChanges();
                    //Console.WriteLine($"Part {part.Name}.");
                }
            }

            Console.WriteLine("Successfully imported Parts.");
        }

        private static void ImportCars(CarDealerContext context)
        {
            var json = File.ReadAllText("../../../datasets/cars.json");
            var cars = JsonConvert.DeserializeObject<IEnumerable<Car>>(json);

            foreach (var car in cars)
            {
                if (car.Make != null && car.Model != null)
                {
                    Random rnd = new Random();

                    IList<Part> carParts = new List<Part>();
                    int partsCount = context.Parts.Count();
                    for (int i = 0; i < 15; i++)
                    {
                        Part part = context.Parts.Find(rnd.Next(1, partsCount));
                        carParts.Add(part);
                    }

                    context.Cars.AddOrUpdate(c => new {c.Make, c.Model, c.TravelledDistance},
                        new Car()
                        {
                            Make = car.Make,
                            Model = car.Model,
                            TravelledDistance = car.TravelledDistance,
                            Parts = carParts
                        });

                    context.SaveChanges();
                    //Console.WriteLine($"car {car.Make} - {car.Model}.");
                }
            }

            Console.WriteLine("Successfully imported Cars.");
        }

        private static void ImportCustomers(CarDealerContext context)
        {
            var json = File.ReadAllText("../../../datasets/customers.json");
            var customers = JsonConvert.DeserializeObject<IEnumerable<Customer>>(json);

            foreach (var customer in customers)
            {
                if (customer.Name != null)
                {
                    context.Customers.AddOrUpdate(c => new { c.Name, c.BirthDate, c.IsYoungDriver},
                        new Customer()
                        {
                            Name = customer.Name,
                            BirthDate = customer.BirthDate,
                            IsYoungDriver = customer.IsYoungDriver
                        });

                    context.SaveChanges();
                    //Console.WriteLine($"Customer {customer.Name}.");
                }
            }

            Console.WriteLine("Successfully imported Customers.");
        }

        private static void ImportSales(CarDealerContext context)
        {
            Random rnd = new Random();

            var cars = context.Cars.ToList();
            var customers = context.Customers.ToList();
            decimal[] discounts = new decimal[] { 0, 0.05m, 0.10m, 0.20m, 0.30m, 0.40m, 0.50m };
            for (int i = 0; i < 20; i++)
            {
                var customer = customers[rnd.Next(customers.Count)];
                var discount = discounts[rnd.Next(discounts.Length)];
                if (customer.IsYoungDriver)
                {
                    discount += 0.05m;
                }

                context.Sales.Add(
                new Sale()
                {
                    Car = cars[rnd.Next(cars.Count)],
                    Customer = customer,
                    Discount = discount    
                });
            }

            context.SaveChanges();
            Console.WriteLine("Successfully imported Sales.");
        }
        #endregion

        #region//Query and Export Data
        private static void OrderedCustomers(CarDealerContext context)
        {
            var customers = context.Customers
                .OrderBy(c => c.BirthDate)
                .ThenByDescending(c => c.IsYoungDriver);

            var customersAsJson = JsonConvert.SerializeObject(customers, Formatting.Indented);
            //Console.WriteLine(customersAsJson);
            File.WriteAllText("../../exportedJson/orderedCustomers.json", customersAsJson);
        }

        private static void CarsFromMakeToyota(CarDealerContext context)
        {
            var cars = context.Cars
                .Where(c => c.Make == "Toyota")
                .Select(c => new
                {
                    c.Id,
                    c.Make,
                    c.Model,
                    c.TravelledDistance
                })
                .OrderBy(c => c.Model)
                .ThenByDescending(c => c.TravelledDistance);

            var carsAsJson = JsonConvert.SerializeObject(cars, Formatting.Indented);
            //Console.WriteLine(carsAsJson);
            File.WriteAllText("../../exportedJson/carsFromMakeToyota.json", carsAsJson);
        }

        private static void LocalSuppliers(CarDealerContext context)
        {
            var suppliers = context.Suppliers
                .Where(s => s.IsImporter == false)
                .Select(s => new
                {
                    s.Id,
                    s.Name,
                    partsCount = s.Parts.Count
                });

            var suppliersAsJson = JsonConvert.SerializeObject(suppliers, Formatting.Indented);
            //Console.WriteLine(suppliersAsJson);
            File.WriteAllText("../../exportedJson/localSuppliers.json", suppliersAsJson);
        }

        private static void CarsWithTheirListOfParts(CarDealerContext context)
        {
            var carsWithParts = context.Cars
                .Include("Parts")
                .Select(c => new
                {
                    car = new { 
                        c.Make,
                        c.Model,
                        c.TravelledDistance
                    },
                    parts = c.Parts.Select(p => new
                    {
                        p.Name,
                        p.Price
                    })
                });

            var carsWithPartsAsJson = JsonConvert.SerializeObject(carsWithParts, Formatting.Indented);
            //Console.WriteLine(carsWithPartsAsJson);
            File.AppendAllText("../../exportedJson/carsWithParts.json", carsWithPartsAsJson);
        }

        private static void TotalSalesByCustomer(CarDealerContext context)
        {
            var customers = context.Customers
                .Where(c => c.Sales.Count() > 0)
                .Select(c => new
                {
                    fullName = c.Name,
                    boughtCars = c.Sales.Count,
                    spentMoney = c.Sales.Sum(s => s.Car.Parts.Sum(p => p.Price))
                })
                .OrderByDescending(c => c.spentMoney)
                .ThenByDescending(c => c.boughtCars);

            var customersAsJson = JsonConvert.SerializeObject(customers, Formatting.Indented);
            //Console.WriteLine(customersAsJson);
            File.AppendAllText("../../exportedJson/totalSalesByCustomer.json", customersAsJson);
        }

        private static void SalesWithAppliedDiscount(CarDealerContext context)
        {
            var sales = context.Sales
                .Include("Car")
                .Include("Customer")
                .Select(s => new
                {
                    car = new
                    {
                        s.Car.Make,
                        s.Car.Model,
                        s.Car.TravelledDistance
                    },
                    customerName = s.Customer.Name,
                    s.Discount,
                    price = s.Car.Parts.Sum(p => p.Price),
                    priceWithDiscount = (s.Car.Parts.Sum(p => p.Price)) - (s.Car.Parts.Sum(p => p.Price) * s.Discount)
                });

            var salesAsJson = JsonConvert.SerializeObject(sales, Formatting.Indented);
            //Console.WriteLine(salesAsJson);
            File.AppendAllText("../../exportedJson/salesWithAppliedDiscount.json", salesAsJson);
        }
        #endregion
    }
}
