using H_04_EntityFramework_CodeFirst.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;

namespace H_04_EntityFramework_CodeFirst
{
    class Program
    {
        static void Main()
        {
            UserContext context = new UserContext();

            #region//Problm 2 - create UsersDB and table Users; insert valid user in Users
            //User user = new User();
            //user.Username = "David";
            //user.Age = 17;
            //user.Email = "david.jones@proseware.com";
            //user.Password = "daVid_j0nes";
            //user.RegisteredOn = DateTime.Now;
            //user.LastTimeLoggedIn = DateTime.Now;
            //user.ProfilePicture = File.ReadAllBytes(".../.../user-img.jpg");
            //user.IsDeleted = false;

            //context.Users.Add(user);
            //context.SaveChanges();
            #endregion

            #region//check valid email and valid password

            //User david = context.Users.First();
            //Console.WriteLine($"User {david.Username}\n Current Email: {david.Email}\n Current pass: {david.Password}");
            //try
            //{
            //    david.Email = "_david.jones@prose.ware.com";
            //    david.Password = "dav!d<>Jones";
            //    context.SaveChanges();
            //}
            //catch (DbEntityValidationException ex)
            //{
            //    foreach (DbEntityValidationResult result in ex.EntityValidationErrors)
            //    {
            //        foreach (DbValidationError error in result.ValidationErrors)
            //        {
            //            Console.WriteLine(error.ErrorMessage);
            //        }
            //    }
            //}
            #endregion

            #region//Problem 6 - insert Towns
            //context.Towns.AddOrUpdate(
            //      new Town { Name = "Berlin", Country = "Germany" },
            //      new Town { Name = "Sofia", Country = "Bulgaria" },
            //      new Town { Name = "Roma", Country = "Italy" }
            //    );

            //context.SaveChanges();
            #endregion

            #region//add towns to users
            //Town berlin = context.Towns.Find(1);
            //Town sofia = context.Towns.Find(2);
            //User userFromBerlin = context.Users.First();
            //userFromBerlin.bornTown = berlin;
            //userFromBerlin.currentlyLivingTown = sofia;

            //context.SaveChanges();
            #endregion

            #region//Problem 7 - add user FirstName, LastName and FullName

            //User david = context.Users.Find(1);
            //david.FirstName = "David";
            //david.LastName = "Jones";
            //context.SaveChanges();

            //Console.WriteLine(david.FullName);
            #endregion
        }
    }
}
