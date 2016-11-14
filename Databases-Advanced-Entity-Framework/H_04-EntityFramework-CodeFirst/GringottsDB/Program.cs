using GringottsDB.Models;
using System;

namespace GringottsDB
{
    class Program
    {
        static void Main()
        {
            GringottsContext context = new GringottsContext();
            WizardDeposit dumbledore = new WizardDeposit()
            {
                FirstName = "Albus",
                LastName = "Dumbledore",
                Age = 150,
                MagicWand = new MagicWand()
                {
                    Creator = "Antioch Peverell",
                    Size = 15
                },
                Deposit = new Deposit()
                {
                    StartDate = new DateTime(2016, 10, 20),
                    ExpirationDate = new DateTime(2020, 10, 20),
                    Amount = 20000.24m,
                    Charge = 0.2m,
                    IsExpired = false
                }
            };

            context.WizardDeposits.Add(dumbledore);
            context.SaveChanges();
        }
    }
}
