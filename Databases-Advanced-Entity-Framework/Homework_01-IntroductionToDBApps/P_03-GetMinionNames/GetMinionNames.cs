using System;
using System.Data.SqlClient;

namespace P_03_GetMinionNames
{
    class GetMinionNames
    {
        static void Main()
        {
            Console.WriteLine("Enter VillainID");
            string villainID = Console.ReadLine();
            int villainIDParsed;
            if (Int32.TryParse(villainID, out villainIDParsed))
            {
                string connectionStr = "Server=.\\SQLEXPRESS; Database=Minions; Integrated Security=True;";
                SqlConnection connection = new SqlConnection(connectionStr);
                connection.Open();

                string selectMinionNames = "SELECT v.VillainName, m.MinionName, m.Age " +
                                            "FROM Minions AS m " +
                                            "RIGHT JOIN MinionsVillains AS mv " +
                                            "ON m.MinionID = mv.MinionID " +
                                            "RIGHT JOIN Villains AS v " +
                                            "ON v.VillainID = mv.VillainID " +
                                            "WHERE v.VillainID =  " + villainIDParsed;

                using (connection)
                {
                    SqlCommand command = new SqlCommand(selectMinionNames, connection);
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        Console.WriteLine($"Villain: {reader[0]}");

                        if (reader[1].ToString().Equals("") && reader[2].ToString().Equals(""))
                        {
                            Console.WriteLine("<no minions>");
                        }
                        else
                        {
                            int countMinion = 1;
                            Console.WriteLine($"{countMinion}. {reader[1]} {reader[2]}");
                            countMinion++;
                            while (reader.Read())
                            {
                                Console.Write(countMinion.ToString() + ".");

                                for (int i = 1; i < reader.FieldCount; i++)
                                {
                                    Console.Write($" {reader[i]} ");
                                }
                                Console.WriteLine();
                                countMinion++;
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine($"No villain with ID {villainID} exists in the database.");
                    }
                }
            }
            else
            {
                Console.WriteLine("Invalid input! String could not be parsed.");
            }
        }
    }
}
