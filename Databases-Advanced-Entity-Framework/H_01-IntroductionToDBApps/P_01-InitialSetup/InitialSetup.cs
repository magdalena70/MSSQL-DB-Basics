using System;
using System.Data.SqlClient;

namespace P_01_InitialSetup
{
    class InitialSetup
    {
        static void Main()
        {
            string connectionStr = "Server=.\\SQLEXPRESS; Database=master; Integrated Security=True;";
            SqlConnection connection = new SqlConnection(connectionStr);
            connection.Open();

            string createDataBase = "CREATE DATABASE Minions ";
            string useDataBase = "USE Minions ";

            string createTableTowns = "CREATE TABLE Towns( " +
                                            "TownID INT PRIMARY KEY IDENTITY(1, 1) NOT NULL, " +
                                             "TownName VARCHAR(50) NOT NULL, " +
                                             "Country VARCHAR(50) NOT NULL) ";

            string createTableMinions = "CREATE TABLE Minions( " +
                                            "MinionID INT PRIMARY KEY IDENTITY(1, 1) NOT NULL, " +
                                            "MinionName VARCHAR(50) NOT NULL, " +
                                            "Age INT NOT NULL, " +
                                            "TownID INT, " +
                                            "CONSTRAINT FK_Minions_Towns FOREIGN KEY(TownID) " +
                                            "REFERENCES Towns(TownID)) ";

            string createTableEvilnessFactors = "CREATE TABLE EvilnessFactors( " +
                                                    "EvilnessFactorID INT PRIMARY KEY IDENTITY(1, 1) NOT NULL, " +
                                                    "EvilnessFactorName VARCHAR(10) NOT NULL) ";

            string createTableVillains = "CREATE TABLE Villains( " +
                                            "VillainID INT PRIMARY KEY IDENTITY(1, 1) NOT NULL, " +
                                            "VillainName VARCHAR(50) NOT NULL, " +
                                            "EvilnessFactorID INT, " +
                                            "CONSTRAINT FK_Villains_EvilnessFactors FOREIGN KEY(EvilnessFactorID) " +
                                            "REFERENCES EvilnessFactors(EvilnessFactorID)) ";

            string createTableMinionsVillains = "CREATE TABLE MinionsVillains( " + 
                                                    "MinionID INT, " +
                                                    "VillainID INT, " +
                                                    "CONSTRAINT PK_MinionsVillains PRIMARY KEY(MinionID, VillainID), " +
                                                    "CONSTRAINT FK_Minions_Villains FOREIGN KEY(MinionID) " +
                                                    "REFERENCES Minions(MinionID), " +
                                                    "CONSTRAINT FK_Villains_Minions FOREIGN KEY(VillainID) " +
                                                    "REFERENCES Villains(VillainID)) ";
            //insert data
            string insertTowns = "INSERT INTO Towns(TownName, Country) " +
                                 "VALUES('Roma', 'Italy'), ('Berlin', 'Germany'), ('Sofia', 'Bulgaria'), " +
                                   "('Viena', 'Austria'), ('Plovdiv', 'Bulgaria') ";

            string insertMinions = "INSERT INTO Minions(MinionName, Age, TownID) " +
                                    "VALUES('Bob', 13, 1), ('Kevin', 14, 2), ('Stuart', 19, 3), " +
                                      "('Simon', 22, 4), ('Jimmy', 49, 5) ";

            string insertEvilnessFactors = "INSERT INTO EvilnessFactors(EvilnessFactorName) " +
                                            "VALUES('good'), ('bad'), ('evil'), ('super evil') ";

            string insertVillains = "INSERT INTO Villains(VillainName, EvilnessFactorID) " +
                                    "VALUES('FirstGoodVillain', 1), ('BadVillain', 2), ('EvilVillain', 3), " +
                                      "('LastGoodVillain', 1), ('SuperEvilVillain', 4) ";

            string insertMinionsVillains = "INSERT INTO MinionsVillains(MinionID, VillainID) " +
                                            "VALUES(1, 1), (1, 2), (1, 4), (2, 3), (2, 1), " +
                                              "(5, 1), (3, 4), (4, 1), (5, 4) ";

            using (connection)
            {
                SqlCommand command = new SqlCommand(createDataBase, connection);
                if (command.ExecuteNonQuery() == -1) {
                    Console.WriteLine("DataBase Minions created successfully.");
                };

                command.CommandText = useDataBase;
                command.ExecuteNonQuery();

                command.CommandText = createTableTowns;
                if (command.ExecuteNonQuery() == -1) {
                    Console.WriteLine("Table Towns created successfully.");
                };

                command.CommandText = createTableMinions;
                if (command.ExecuteNonQuery() == -1) {
                    Console.WriteLine("Table Minions created successfully.");
                };

                command.CommandText = createTableEvilnessFactors;
                if (command.ExecuteNonQuery() == -1) {
                    Console.WriteLine("Table EvilnessFactors created successfully.");
                };

                command.CommandText = createTableVillains;
                if (command.ExecuteNonQuery() == -1) {
                    Console.WriteLine("Table Villains created successfully.");
                };

                command.CommandText = createTableMinionsVillains;
                if (command.ExecuteNonQuery() == -1) {
                    Console.WriteLine("Table MinionsVillains created successfully.");
                };

                //insert
                command.CommandText = insertTowns;
                Console.WriteLine(command.ExecuteNonQuery() + " rows inserted in Towns successfully.");

                command.CommandText = insertMinions;
                Console.WriteLine(command.ExecuteNonQuery() + " rows inserted in Minions successfully.");

                command.CommandText = insertEvilnessFactors;
                Console.WriteLine(command.ExecuteNonQuery() + " rows inserted in EvilnessFactors successfully.");

                command.CommandText = insertVillains;
                Console.WriteLine(command.ExecuteNonQuery() + " rows inserted in Villians successfully.");

                command.CommandText = insertMinionsVillains;
                Console.WriteLine(command.ExecuteNonQuery() + " rows inserted in MinionsVillians successfully.");
            }
        }
    }
}
