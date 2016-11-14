using System;
using System.Collections.Generic;
using System.Linq;

namespace Gringotts
{
    class Program
    {
        static void Main()
        {
            GringottsContext context = new GringottsContext();
            using (context)
            {
                //Problem 19-First Letter

                IEnumerable<string> wizardFirstLetters = context.WizzardDeposits
                    .Where(w => w.DepositGroup == "Troll Chest")
                    .Select(w => w.FirstName.Substring(0, 1))
                    .Distinct()
                    .OrderBy(w => w);
                foreach (var letter in wizardFirstLetters)
                {
                    Console.WriteLine(letter);
                }
            }
        }
    }
}
