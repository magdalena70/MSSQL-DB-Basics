using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace P_07_PrintAllMinionNames
{
    class PrintAllMinionNames
    {
        static void Main()
        {
            string connectionStr = "Server=.\\SQLEXPRESS; Database=Minions; Integrated Security=True;";
            SqlConnection connection = new SqlConnection(connectionStr);
            connection.Open();
            using (connection)
            {
                string selectMinionNamesAsc = "SELECT MinionName FROM Minions " +
                                            "ORDER BY MinionID ASC";

                SqlCommand command = new SqlCommand(selectMinionNamesAsc, connection);
                SqlDataReader reader = command.ExecuteReader();
                //Original Order
                List<string> minions = new List<string>();
                while (reader.Read())
                {
                    minions.Add(reader[0].ToString());
                }

                Console.WriteLine("Original Order:");
                Console.WriteLine(string.Join("\n", minions));
                Console.WriteLine();

                //Output
                Console.WriteLine("Output:");
                int first = 0;
                int last = minions.Count - 1;
                for (int y = 0; y < minions.Count; y++)
                {
                    int index;
                    if (y % 2 == 0)
                    {
                        index = first;
                        first++;
                        
                    }else
                    {
                        index = last;
                        last--;
                     
                    }

                    Console.WriteLine(minions[index]);
                }
            }
        }
    }
}
