using System;
using System.Data.SqlClient;

namespace P_04_AddMinion
{
    class AddMinion
    {
        static void Main()
        {
            //minion data
            Console.WriteLine("Enter minion's Name, Age and Town separated by a single space.");
            string minionDataStr = Console.ReadLine();
            string[] minionDataArr = minionDataStr.Split(' ');
            if (minionDataArr.Length != 3) {
                Console.WriteLine("Invalid input! You must enter 3 values separated by a single space.");
                return;
            }

            string minionName = minionDataArr[0];
            string minionAge = minionDataArr[1];
            int minionAgeParsed;
            if (!Int32.TryParse(minionAge, out minionAgeParsed))
            {
                Console.WriteLine("Invalid input! Age must be a number.");
                return;
            }
            string minionTown = minionDataArr[2];

            //villain data
            Console.WriteLine("Enter villain's Name");
            string villainName = Console.ReadLine().Trim();
            if (villainName.Equals(""))
            {
                Console.WriteLine("Invalid input! Villain's name can not be empty.");
                return;
            }

            bool isVillainExists;
            bool isMinionExists;
            bool isTownExists;
            bool isMinionsVillainsExists;

            string connectionStr = "Server=.\\SQLEXPRESS; Database=Minions; Integrated Security=True;";
            SqlConnection connection = new SqlConnection(connectionStr);

            connection.Open();
            string useDataBase = "USE Minions ";
            SqlCommand command = new SqlCommand(useDataBase, connection);
            using (connection)
            {
                //check if Town Exists
                string selectExistingTown = "SELECT TownName " +
                                       "FROM Towns " +
                                       "WHERE TownName = '" + minionTown + "' ";
                command.CommandText = selectExistingTown;
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    isTownExists = true;
                }
                else
                {
                    isTownExists = false;
                }
                //Console.WriteLine($"Town Exists: {isTownExists}");
                connection.Close();

                //insert Town
                if (!isTownExists)
                {
                    connection.Open();
                    string insertTown = "INSERT INTO Towns(TownName, Country) " +
                                "VALUES('" + minionTown + "', 'NoCountryName') ";
                    command.CommandText = insertTown;
                    if (command.ExecuteNonQuery() == 1)
                    {
                        Console.WriteLine($"Town {minionTown} was added to the database.");
                    }
                    connection.Close();
                }

                //find  TownID
                connection.Open();
                string selectMinionTownID = "SELECT TownID " +
                                        "FROM Towns " +
                                        "WHERE TownName = '" + minionTown + "' ";
                int existingTownID;
                command.CommandText = selectMinionTownID;
                existingTownID = (int)command.ExecuteScalar();
                //Console.WriteLine($"TownID: {existingTownID}");
                connection.Close();

                //check if Minion Exists
                connection.Open();
                string selectExistingMinion = "SELECT MinionName " +
                        "FROM Minions " +
                        "WHERE MinionName = '" + minionName + "' " +
                        "AND Age = " + minionAge + "AND TownID = ";
                command.CommandText = selectExistingMinion + existingTownID;
                reader = command.ExecuteReader();
                if (reader.Read())
                {
                    isMinionExists = true;
                    Console.WriteLine("Minion already exists");
                }
                else
                {
                    isMinionExists = false;
                }
                connection.Close();

                //insert Minion
                if (!isMinionExists)
                {
                    connection.Open();
                    string insertMinion = "INSERT INTO Minions(MinionName, Age, TownID) " +
                                    "VALUES('" + minionName + "', " + minionAge + ", " + existingTownID + ") ";
                    command.CommandText = insertMinion;
                    if (command.ExecuteNonQuery() == 1)
                    {
                        Console.WriteLine($"Minion {minionName} was added to the database.");
                    }
                    connection.Close();
                }

                //find MinionID
                connection.Open();
                string selectMinionID = "SELECT MinionID " +
                                "FROM Minions " +
                                "WHERE MinionName = '" + minionName + "' " +
                                "AND Age = " + minionAge + " AND TownID = " + existingTownID;
                int existingMinionID;
                command.CommandText = selectMinionID;
                existingMinionID = (int)command.ExecuteScalar();
                //Console.WriteLine($"MinionID: {existingMinionID}");
                connection.Close();

                //check if Villain Exists
                connection.Open();
                string selectExistingVillain = "SELECT VillainName " +
                        "FROM Villains " +
                        "WHERE VillainName = '" + villainName + "' ";
                command.CommandText = selectExistingVillain;
                reader = command.ExecuteReader();
                if (reader.Read())
                {
                    isVillainExists = true;
                }
                else
                {
                    isVillainExists = false;
                }
                //Console.WriteLine($"Villain Exists: {isVillainExists}");
                connection.Close();

                //insert Villain
                if (!isVillainExists)
                {
                    connection.Open();
                    string insertVillain = "INSERT INTO Villains(VillainName, EvilnessFactorID) " +
                                    "VALUES('" + villainName + "',  3) ";
                    command.CommandText = insertVillain;
                    if (command.ExecuteNonQuery() == 1)
                    {
                        Console.WriteLine($"Villain {villainName} was added to the database.");
                    }
                    connection.Close();
                }

                //find VillainID
                connection.Open();
                string selectVillainID = "SELECT VillainID " +
                                "FROM Villains " +
                                "WHERE VillainName = '" + villainName + "' ";
                int existingVillainID;
                command.CommandText = selectVillainID;
                existingVillainID = (int)command.ExecuteScalar();
                //Console.WriteLine($"VillainID: {existingVillainID}");
                connection.Close();

                //check if MinionsVillains Exists
                connection.Open();
                string selectMinionsVillains = "SELECT * FROM MinionsVillains " +
                                            "WHERE MinionID = " + existingMinionID +
                                            "AND VillainID = " + existingVillainID;
                command.CommandText = selectMinionsVillains;
                reader = command.ExecuteReader();
                if (reader.Read())
                {
                    isMinionsVillainsExists = true;
                    Console.WriteLine($"{minionName} already is minion of {villainName}.");
                    return;
                }
                else
                {
                    isMinionsVillainsExists = false;
                }
                //Console.WriteLine($"isMinionsVillainsExists: {isMinionsVillainsExists}");
                connection.Close();

                //insert MinionsVillains
                if (!isMinionsVillainsExists)
                {
                    connection.Open();
                    string insertMinionsVillains = "INSERT INTO MinionsVillains(MinionID, VillainID) " +
                                                    "VALUES(" + existingMinionID + ", " + existingVillainID + ") ";
                    command.CommandText = insertMinionsVillains;
                    if (command.ExecuteNonQuery() == 1)
                    {
                        Console.WriteLine($"Successfully added {minionName} to be minion of {villainName}.");
                    }
                    connection.Close();
                }
            }
        }
    }
}
