using System;
using System.Data.SqlClient;
using System.Linq;

namespace P_08_IncreaseMinionsAge
{
    class IncreaseMinionsAge
    {

        static void Main()
        {
            Console.WriteLine("Enter minion IDs separated by space.");
            string[] input = Console.ReadLine().Trim().Split(' ');
            int[] minionIDs = new int[input.Length];
            try
            {

                minionIDs = input.Select(int.Parse).ToArray();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return;
            }

            string connectionStr = "Server=.\\SQLEXPRESS; Database=Minions; Integrated Security=True;";
            SqlConnection connection = new SqlConnection(connectionStr);
            connection.Open();
            using (connection)
            {
                string updateMinionData = "UPDATE Minions " +
                                            "SET Age = Age + 1, " +
                                            "MinionName = UPPER(LEFT(MinionName, 1)) + LOWER(RIGHT(MinionName, LEN(MinionName)-1)) " +
                                            "WHERE MinionID IN(" + string.Join(",", minionIDs) + ") ";
                SqlCommand command = new SqlCommand(updateMinionData, connection);
                int countUpdatedMinions = command.ExecuteNonQuery();
                if (countUpdatedMinions > 0)
                {
                    Console.WriteLine($"{countUpdatedMinions} minions were updated.");
                    connection.Close();

                    connection.Open();
                    string selectAllMinions = "SELECT MinionName, Age FROM Minions";
                    command.CommandText = selectAllMinions;
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Console.WriteLine($"{reader[0]} {reader[1]}");
                    }
                }
                else
                {
                    Console.WriteLine("No minions were updated.");
                }
            }
        }
    }
}
