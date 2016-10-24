using System;
using System.Data.SqlClient;

namespace P_05_ChangeTownNamesCasing
{
    class ChangeTownNamesCasing
    {
        static void Main()
        {
            string countryName = Console.ReadLine().Trim();
            if (countryName == "")
            {
                Console.WriteLine("Town's Name can not be empty.");
                return;
            }

            string connectionStr = "Server=.\\SQLEXPRESS; Database=Minions; Integrated Security=True;";
            SqlConnection connection = new SqlConnection(connectionStr);
            connection.Open();
            
            using (connection)
            {
                //update towns in current Country
                string updateTableTowns = "UPDATE Towns " +
                                        "SET TownName = UPPER(TownName) " +
                                        "WHERE Country = '" + countryName + "' ";
                SqlCommand command = new SqlCommand(updateTableTowns, connection);

                int countUpdatedTowns = command.ExecuteNonQuery();
                if (countUpdatedTowns > 0)
                {
                    Console.WriteLine($"{countUpdatedTowns} town names were affected.");
                }
                else
                {
                    Console.WriteLine("No town names were affected.");
                    return;
                }
                connection.Close();

                //select updated towns
                connection.Open();
                string selectTownsInCountry = "SELECT TownName FROM Towns " +
                                                "WHERE Country = '" + countryName + "' ";
                command.CommandText = selectTownsInCountry;
                SqlDataReader reader = command.ExecuteReader();

                string[] towns = new string[countUpdatedTowns];
                int countSelectedTowns = 0;
                while (reader.Read())
                {
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        towns[countSelectedTowns] = reader[i].ToString();
                    }

                    countSelectedTowns++;
                }

                Console.Write($"[{string.Join(", ", towns)}]");       
            }
        }
    }
}
