using System;
using ProductsShopDB.Data;
using System.IO;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using ProductsShopDB.Models;
using System.Linq;
using System.Xml.Linq;

namespace ProductsShopDB.ConsoleClient
{
    class Program
    {
        static void Main()
        {
            var context = new ProductsShopContext();

            #region//Import data
            //ImportUsers(context);
            //ImportCategories(context);
            //ImportProducts(context);
            #endregion

            #region//Export data
            //SelectProductsInRange(context);
            //SelectSuccessfullySoldProducts(context); 
            //SelectCategoriesByProductsCount(context);
            //SelectUsersAndProducts(context);
            #endregion
        }

        #region//Import data
        private static void ImportUsers(ProductsShopContext context)
        {
            var json = File.ReadAllText("../../../datasets/users.json");
            var users = JsonConvert.DeserializeObject<IEnumerable<User>>(json);

            foreach (var user in users)
            {
                if (user.LastName != null)
                {
                    context.Users.AddOrUpdate(u => u.LastName,
                        new User() {
                            FirstName = user.FirstName,
                            LastName = user.LastName,
                            Age = user.Age
                        });
                    context.SaveChanges();
                    //Console.WriteLine($"User {user.FirstName} {user.LastName}.");
                }
            }

            Console.WriteLine("Successfully imported Users.");
        }

        private static void ImportCategories(ProductsShopContext context)
        {
            var json = File.ReadAllText("../../../datasets/categories.json");
            var categories = JsonConvert.DeserializeObject<IEnumerable<Categorie>>(json);

            foreach (var categorie in categories)
            {
                if(categorie.Name != null)
                {
                    context.Categories.AddOrUpdate(c => c.Name,
                        new Categorie()
                        {
                            Name = categorie.Name
                        });
                    context.SaveChanges();
                    //Console.WriteLine($"Category {categorie.Name}.");
                }
            }

            Console.WriteLine("Successfully imported Categories.");
        }

        private static void ImportProducts(ProductsShopContext context)
        {        
            var json = File.ReadAllText("../../../datasets/products.json");
            var products = JsonConvert.DeserializeObject<IEnumerable<Product>>(json);

            foreach (var product in products)
            {
                if (product.Name != null && product.Price > 0)
                {
                    Random rnd = new Random();

                    IList<Categorie> productCategories = new List<Categorie>();
                    int categoriesCount = context.Categories.Count();
                    for (int i = 0; i < categoriesCount / 4; i++)
                    {
                        Categorie categorie = context.Categories.Find(rnd.Next(1, categoriesCount));
                        productCategories.Add(categorie);
                    }

                    int userCount = context.Users.Count();
                    context.Products.AddOrUpdate(p => p.Name,
                        new Product()
                        {
                            Name = product.Name,
                            Price = product.Price,
                            Categories = productCategories,
                            Seller = context.Users.Find(rnd.Next(1, userCount)),
                            Buyer = product.Price > 400 && product.Price < 550 ? null : context.Users.Find(rnd.Next(1, userCount))
                        });

                    context.SaveChanges();
                    //Console.WriteLine($"Product {product.Name}.");
                }
            }

            Console.WriteLine("Successfully imported Products.");
        }
        #endregion

        #region//Export data
        private static void SelectProductsInRange(ProductsShopContext context)
        {
            var products = context.Products
                .Where(p => p.Price >= 500 && p.Price <= 1000 && p.Buyer == null)
                .Select(p => new
                {
                    name = p.Name,
                    price = p.Price,
                    seller = (p.Seller.FirstName + " " + p.Seller.LastName).Trim()
                })
                .OrderBy(p => p.price);

            //Json
            var productsAsJson = JsonConvert.SerializeObject(products, Formatting.Indented);
            File.WriteAllText("../../exportedJson/productsInRange.json", productsAsJson);

            //Xml
            XElement productsXelem = new XElement("products");
            foreach (var product in products)
            {
                XElement productXelem = new XElement("product");
                productXelem.SetAttributeValue(nameof(product.name), product.name);
                productXelem.SetAttributeValue(nameof(product.price), product.price);
                productXelem.SetAttributeValue(nameof(product.seller), product.seller);
                productsXelem.Add(productXelem);
            }
            
            productsXelem.Save("../../exportedXml/productsInRange.xml");
        }

        private static void SelectSuccessfullySoldProducts(ProductsShopContext context)
        {
            var users = context.Users
                .Where(u => u.SoldProducts.Count(p => p.Buyer != null) >= 1)
                .Select(u => new
                {
                    firstName = u.FirstName,
                    lastName = u.LastName,
                    soldProducts = u.SoldProducts
                        .Where(p => p.Buyer != null)
                        .Select(p => new
                        {
                            name = p.Name,
                            price = p.Price,
                            buyerFirstName = p.Buyer.FirstName,
                            buyerLastName = p.Buyer.LastName
                        })
                })
                .OrderBy(u => u.lastName)
                .ThenBy(u => u.firstName);

            //Json
            var usersAsJson = JsonConvert.SerializeObject(users, Formatting.Indented);
            File.WriteAllText("../../exportedJson/successfullySoldProducts.json", usersAsJson);

            //Xml
            XElement usersXelem = new XElement("users");
            foreach (var user in users)
            {
                XElement userXelem = new XElement("user");
                userXelem.SetAttributeValue("first-name", user.firstName);
                userXelem.SetAttributeValue("last-name", user.lastName);
                XElement soldProductsXelem = new XElement("sold-products");
                userXelem.Add(soldProductsXelem);
                foreach (var product in user.soldProducts)
                {
                    XElement productXelem = new XElement("product");
                    productXelem.Add(new XElement(nameof(product.name), product.name));
                    productXelem.Add(new XElement(nameof(product.price), product.price));
                    productXelem.Add(new XElement("buyer-first-name", product.buyerFirstName));
                    productXelem.Add(new XElement("buyer-last-name", product.buyerLastName));
                    soldProductsXelem.Add(productXelem);
                }

                usersXelem.Add(userXelem);
            }

            usersXelem.Save("../../exportedXml/successfullySoldProducts.xml");
        }

        private static void SelectCategoriesByProductsCount(ProductsShopContext context)
        {
            var categories = context.Categories
                .Select(c => new
                {
                    category = c.Name,
                    productsCount = c.Products.Count(),
                    averagePrice = c.Products.Count() > 0 ? c.Products.Average(p => p.Price) : 0,
                    totalRevenue = c.Products.Count() > 0 ? c.Products.Sum(p => p.Price) : 0
                })
                .OrderBy(c => c.productsCount);

            //Json
            var categoriesAsJson = JsonConvert.SerializeObject(categories, Formatting.Indented);
            File.WriteAllText("../../exportedJson/categoriesByProductsCount.json", categoriesAsJson);

            //Xml
            XElement categoriesXelem = new XElement("categories");
            foreach (var category in categories)
            {
                XElement categoryXelem = new XElement("category");
                categoryXelem.SetAttributeValue("name", category.category);
                categoryXelem.Add(new XElement("products-count", category.productsCount));
                categoryXelem.Add(new XElement("average-price", category.averagePrice));
                categoryXelem.Add(new XElement("total-revenue", category.totalRevenue));

                categoriesXelem.Add(categoryXelem);
            }

            categoriesXelem.Save("../../exportedXml/categoriesByProductsCount.xml");
        }

        private static void SelectUsersAndProducts(ProductsShopContext context)
        {
            var usersAndProducts = context.Users
                .Include("SoldProducts")
                .Where(u => u.SoldProducts.Count() > 0)
                .Select(u => new
                {
                    firstName = u.FirstName,
                    lastName = u.LastName,
                    age = u.Age,
                    soldProducts = u.SoldProducts.Select(p => new
                    {
                        count = u.SoldProducts.Count,
                        products = u.SoldProducts.Select(sp => new
                        {
                            name = sp.Name,
                            price = sp.Price
                        })
                    })
                })
                .OrderByDescending(u => u.soldProducts.Count());

            var jsonToSerialize = new
            {
                usersCount = usersAndProducts.Count(),
                users = usersAndProducts
            };

            //Json
            var usersAndProductsAsJson = JsonConvert.SerializeObject(jsonToSerialize, Formatting.Indented);
            File.WriteAllText("../../exportedJson/usersAndProducts.json", usersAndProductsAsJson);

            //Xml
            XElement usersXelem = new XElement("users");
            usersXelem.SetAttributeValue("count", usersAndProducts.Count());
            foreach (var user in usersAndProducts)
            {
                XElement userXelem = new XElement("user");
                userXelem.SetAttributeValue("first-name", user.firstName);
                userXelem.SetAttributeValue("last-name", user.lastName);
                userXelem.SetAttributeValue("age", user.age);
                
                foreach (var products in user.soldProducts)
                {
                    XElement soldProductsXelem = new XElement("sold-products");
                    soldProductsXelem.SetAttributeValue("count", products.count);
                    foreach (var product in products.products)
                    {
                        XElement productXelem = new XElement("product");
                        productXelem.SetAttributeValue("name", product.name);
                        productXelem.SetAttributeValue("price", product.price);

                        soldProductsXelem.Add(productXelem);
                    }

                    userXelem.Add(soldProductsXelem);
                }

                usersXelem.Add(userXelem);
            }

            usersXelem.Save("../../exportedXml/usersAndProducts.xml");
        }
        #endregion
    }
}
