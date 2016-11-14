using System;
using System.Data.SqlClient;

namespace P_02_GetVillainsNames
{
    class GetVillainsNames
    {
        static void Main()
        {
            string connectionStr = "Server=.\\SQLEXPRESS; Database=Minions; Integrated Security=True;";
            SqlConnection connection = new SqlConnection(connectionStr);
            connection.Open();

            string selectVillainsNames = "SELECT x.VillainName, x.MinionsCount " +
                                        "FROM( " +
                                            "SELECT v.VillainID, v.VillainName, COUNT(mv.MinionID) AS 'MinionsCount' " +
                                            "FROM Villains AS v " +
                                            "INNER JOIN MinionsVillains AS mv " +
                                            "ON v.VillainID = mv.VillainID " +
                                            "GROUP BY v.VillainID, v.VillainName " +
                                        ") AS x " +
                                        "WHERE x.MinionsCount >= 3 " +
                                        "ORDER BY x.MinionsCount DESC ";

            using (connection)
            {
                SqlCommand command = new SqlCommand(selectVillainsNames, connection);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        Console.Write($"{reader[i]} ");
                    }
                    Console.WriteLine();
                }
            }
        }
    }
}
