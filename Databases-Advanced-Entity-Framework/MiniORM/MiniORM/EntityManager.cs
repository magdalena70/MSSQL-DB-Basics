using MiniORM.Attributes;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;

namespace MiniORM
{
    class EntityManager : IDbContext
    {
        private SqlConnection connection;
        private string connectionString;
        private bool isCodeFirst;

        public EntityManager(string connectionString, bool isCodeFirst)
        {
            this.connectionString = connectionString;
            this.isCodeFirst = isCodeFirst;
        }

        public IEnumerable<T> FindAll<T>()
        {
            //Console.WriteLine("This is method FindAll() -> ");
            List<T> selectedObjects = new List<T>();
            selectedObjects = SelectAllObjectsByCondition<T>(null).ToList<T>();

            return selectedObjects;
        }

        public IEnumerable<T> FindAll<T>(string where)
        {
            //Console.WriteLine("This is method FindAll where... -> ");
            List<T> selectedObjects = new List<T>();
            selectedObjects = SelectAllObjectsByCondition<T>(where).ToList<T>();

            return selectedObjects;
        }

        private IEnumerable<T> SelectAllObjectsByCondition<T>(string condition)
        {
            List<T> selectedObjects = new List<T>();
            string selectAllObjects = $"SELECT * FROM {this.GetTableName(typeof(T))} ";
            if (condition != null)
            {
                selectAllObjects += condition;
            }
            //Console.WriteLine(selectAllObjects);

            using (connection = new SqlConnection(this.connectionString))
            {
                this.connection.Open();
                SqlCommand command = new SqlCommand(selectAllObjects, this.connection);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {

                    selectedObjects.Add(CrateEntity<T>(reader));
                }
            }

            return selectedObjects;
        }


        public T FindById<T>(int id)
        {
            //Console.WriteLine("This is method FindById -> ");
            T selectedObject = default(T);
            string selectObjectById = $"SELECT * FROM {this.GetTableName(typeof(T))} " +
                                        "WHERE Id = @id ";
            using (connection = new SqlConnection(this.connectionString))
            {
                this.connection.Open();
                SqlCommand command = new SqlCommand(selectObjectById, this.connection);
                command.Parameters.AddWithValue("@id", id);
                SqlDataReader reader = command.ExecuteReader();
                
                if (!reader.HasRows)
                {
                    throw new InvalidOperationException($"No entity was found with id = {id}");
                }

                reader.Read();
                selectedObject = CrateEntity<T>(reader);
            }

            return selectedObject;
        }

        private T CrateEntity<T>(SqlDataReader reader)
        {
            //Console.WriteLine("This is method CrateEntity -> ");
            object[] columnsFromReader = new object[reader.FieldCount];
            reader.GetValues(columnsFromReader);

            Type[] columnsFromReaderTypes = new Type[columnsFromReader.Length - 1];
            object[] columnsFromReaderValues = new object[columnsFromReader.Length - 1];
            for (int i = 1; i < columnsFromReader.Length; i++)
            {
                columnsFromReaderTypes[i - 1] = columnsFromReader[i].GetType();
                columnsFromReaderValues[i - 1] = columnsFromReader[i];
            }

            T objectFromReader = (T)typeof(T)
                .GetConstructor(columnsFromReaderTypes)
                .Invoke(columnsFromReaderValues);
            FieldInfo idfInfo = objectFromReader.GetType()
                .GetFields(BindingFlags.Instance | BindingFlags.NonPublic)
                .FirstOrDefault(x => x.IsDefined(typeof(IdAttribute)));
            idfInfo.SetValue(objectFromReader, columnsFromReader[0]);

            return objectFromReader;
        }

        public T FindFirst<T>()
        {
            T selectedObject = default(T);

            selectedObject = SelectFirstObjectByCondition<T>(null);
            return selectedObject;
        }

        public T FindFirst<T>(string where)
        {
            T selectedObject = default(T);
            
            selectedObject = SelectFirstObjectByCondition<T>(where);
            return selectedObject;
        }

        private T SelectFirstObjectByCondition<T>(string condition)
        {
            T firstSelectedObject = default(T);
            string selectFirstWhere = $"SELECT TOP 1 * FROM {this.GetTableName(typeof(T))} ";
            if (condition != null)
            {
                selectFirstWhere += condition;
            }

            using (connection = new SqlConnection(this.connectionString))
            {
                this.connection.Open();
                SqlCommand command = new SqlCommand(selectFirstWhere, this.connection);
                SqlDataReader reader = command.ExecuteReader();

                if (!reader.HasRows)
                {
                    throw new InvalidOperationException("No entity was found in database.");
                }

                reader.Read();
                firstSelectedObject = CrateEntity<T>(reader);
            }

            return firstSelectedObject;
        }


        public void Delete<T>(object entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("Cannot delete null entity!");
            }

            FieldInfo idInfo = this.GetId(entity);
            int id = (int)idInfo.GetValue(entity);
            if (id <= 0)
            {
                throw new InvalidOperationException($"Cannot delete entity with id {id}!");
            }

            this.DeleteById<T>(id);
        }

        public void DeleteById<T>(int id)
        {
            string deleteObjectById = $"DELETE FROM {this.GetTableName(typeof(T))} WHERE [Id] = @id";

            using (this.connection = new SqlConnection(this.connectionString))
            {
                this.connection.Open();
                SqlCommand deleteCommand = new SqlCommand(deleteObjectById, this.connection);
                deleteCommand.Parameters.AddWithValue("@id", id);
                int count = deleteCommand.ExecuteNonQuery();
                //Console.Write($"{count}");
            }
        }

        public bool Persist(object entity)
        {
            //Console.WriteLine($"This is method Persist ->");
            if (entity == null)
            {
                throw new ArgumentNullException("Cannot persist null entity.");
            }

            if (isCodeFirst && !CheckIfTableExists(entity.GetType()))
            {
                Console.WriteLine($"Persist create table from type: {entity.GetType()}");
                this.CreateTable(entity.GetType());
            }

            FieldInfo idInfo = GetId(entity);
            int id = (int)idInfo.GetValue(entity);
            if (id <= 0)
            {
                //Console.WriteLine($"Persist insert( {entity.GetType().Name}, {id})");
                return this.Insert(entity, idInfo);
            }

            //Console.WriteLine($"Persist update({entity.GetType().Name}, {id})");
            return this.Update(entity, idInfo);
        }


        private bool CheckIfTableExists(Type type)
        {
            //Console.Write($"This is metod Check If Table: {this.GetTableName(type)} Exists -> ");
            string query = $"SELECT COUNT(name) " +
                            $"FROM sys.sysobjects " +
                            $"WHERE [Name] = '{this.GetTableName(type)}' AND [xtype] = 'U'";
            int numberOfTable = 0;
            using (connection = new SqlConnection(this.connectionString))
            {
                this.connection.Open();
                SqlCommand command = new SqlCommand(query, this.connection);
                numberOfTable = (int)command.ExecuteScalar();
            }
            //Console.Write($" result is: {numberOfTable > 0} \n");
            return numberOfTable > 0;
        }

        private void CreateTable(Type entity)
        {
            //Console.WriteLine(" This is mrthod Create Table -> ");
            string creationString = PrepareTableCreationString(entity);
            using (connection = new SqlConnection(this.connectionString))
            {
                this.connection.Open();
                SqlCommand command = new SqlCommand(creationString, this.connection);
                int result = command.ExecuteNonQuery();
                Console.WriteLine($"result: {result}");
            }
        }

        private string PrepareTableCreationString(Type entity)
        {
            //Console.WriteLine("This is method PrepareTableCreationString -> ");
            StringBuilder builder = new StringBuilder();
            builder.Append($"CREATE TABLE {GetTableName(entity)}( ");
            builder.Append($"Id INT PRIMARY KEY IDENTITY NOT NULL, ");

            FieldInfo[] columns = entity.GetFields(BindingFlags.Instance | BindingFlags.NonPublic)
                .Where(x => x.IsDefined(typeof(ColumnAttribute)))
                .ToArray();

            foreach (FieldInfo column in columns)
            {
                builder.Append($"{this.GetFieldName(column)} {this.GetTypeToDb(column)}, ");
            }

            builder.Remove(builder.Length - 2, 2);
            builder.Append(")");
            //Console.WriteLine($"string createTable is: {builder.ToString()}");
            return builder.ToString();
        }

        private bool Insert(object entity, FieldInfo idInfo)
        {
            //Console.WriteLine("This is method Insert -> ");
            int numberOfAffectedRows = 0;
            string insertionString = this.PrepareEntityInsertionString(entity);
            using (connection = new SqlConnection(this.connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(insertionString, this.connection);
                numberOfAffectedRows = command.ExecuteNonQuery();

                string selectLastId = $"SELECT MAX(id) FROM {this.GetTableName(entity.GetType())} ";
                command.CommandText = selectLastId;
                int id = (int)command.ExecuteScalar();

                idInfo.SetValue(entity, id);
            }

            return numberOfAffectedRows > 0;
        }

        private string PrepareEntityInsertionString(object entity)
        {
            //Console.WriteLine("This is method PrepareEntityInsertionString -> ");
            StringBuilder insertionString = new StringBuilder();
            StringBuilder columnNamesString = new StringBuilder();
            StringBuilder valuesString = new StringBuilder();

            insertionString.Append($"INSERT INTO {this.GetTableName(entity.GetType())}( ");
            FieldInfo[] columns = entity.GetType().GetFields(BindingFlags.Instance | BindingFlags.NonPublic)
                .Where(x => x.IsDefined(typeof(ColumnAttribute)))
                .ToArray();

            foreach (FieldInfo column in columns)
            {   
                columnNamesString.Append($"{this.GetFieldName(column)}, ");
                
                string[] columnType = column.ToString().Split(' ');
                if (columnType[0] == "System.DateTime")
                {
                    DateTime date = (DateTime)column.GetValue(entity);
                    string myDate = date.ToUniversalTime().ToString("s");
                    valuesString.Append($"'{myDate}', ");
                }
                else if (columnType[0] == "System.Decimal")
                {
                    string validDecimalValue = column.GetValue(entity).ToString().Replace(',', '.');
                    valuesString.Append($"'{validDecimalValue}', ");
                }
                else
                {
                    valuesString.Append($"'{column.GetValue(entity)}', ");
                }   
            }

            columnNamesString = columnNamesString.Remove(columnNamesString.Length - 2, 2);
            valuesString = valuesString.Remove(valuesString.Length - 2, 2);
            insertionString.Append($"{columnNamesString}) ");
            insertionString.Append($"VALUES({valuesString}) ");

            //Console.WriteLine(insertionString.ToString());
            return insertionString.ToString();
        }

        private bool Update(object entity, FieldInfo idInfo)
        {
            //Console.WriteLine("This is method Update -> ");
            int numberOfAffectedRows = 0;
            string updateString = this.PrepareEntityUpdatedString(entity, idInfo);
            using (connection = new SqlConnection(this.connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(updateString, this.connection);
                numberOfAffectedRows = command.ExecuteNonQuery();
            }

            return numberOfAffectedRows > 0;
        }

        private string PrepareEntityUpdatedString(object entity, FieldInfo idInfo)
        {
            //Console.WriteLine("This is method PrepareEntityUpdateString -> ");
            StringBuilder updateString = new StringBuilder();
            StringBuilder setColumnsToUpdate = new StringBuilder();

            updateString.Append($"UPDATE {this.GetTableName(entity.GetType())} SET ");
            FieldInfo[] columns = entity.GetType().GetFields(BindingFlags.Instance | BindingFlags.NonPublic)
                .Where(x => x.IsDefined(typeof(ColumnAttribute)))
                .ToArray();

            foreach (FieldInfo column in columns)
            {
                string[] columnType = column.ToString().Split(' ');
                if (columnType[0] == "System.DateTime")
                {
                    DateTime date = (DateTime)column.GetValue(entity);
                    string myDate = date.ToUniversalTime().ToString("s");
                    setColumnsToUpdate.Append($"{this.GetFieldName(column)} = '{myDate}', ");
                }
                else if (columnType[0] == "System.Decimal")
                {
                    string validDecimalValue = column.GetValue(entity).ToString().Replace(',', '.');
                    setColumnsToUpdate.Append($"{this.GetFieldName(column)} = '{validDecimalValue}', ");
                }
                else
                {
                    setColumnsToUpdate.Append($"{this.GetFieldName(column)} = '{column.GetValue(entity)}', ");
                }
            }

            setColumnsToUpdate = setColumnsToUpdate.Remove(setColumnsToUpdate.Length - 2, 2);
            updateString.Append($"{setColumnsToUpdate} ");
            updateString.Append($"WHERE Id = {idInfo.GetValue(entity)} ");
            //Console.WriteLine(updateString.ToString());
            return updateString.ToString();
        }

        private string GetTypeToDb(FieldInfo field)
        {
            switch (field.FieldType.Name)
            {
                case "Int32": return "INT";
                case "String": return "VARCHAR(max)"; 
                case "DateTime": return "DATETIME"; 
                case "Boolean": return "BIT"; 
                case "Single":
                case "Double":
                case "Decimal":
                    return "DECIMAL(10, 4)"; 
                default:
                    //Console.WriteLine(field.FieldType.Name);
                    throw new ArgumentException("Get type exception."); 
            }
        }

        private FieldInfo GetId(object entity)
        {
            //Console.Write("This is method GetId -> ");
            if (entity.GetType() == null)
            {
                throw new ArgumentNullException("Cannot get Id for null type.");
            }

            //gets all private fields -> first with IdAttribute
            FieldInfo id = entity.GetType().GetFields(BindingFlags.Instance | BindingFlags.NonPublic)
                .FirstOrDefault(x => x.IsDefined(typeof(IdAttribute)));

            if (id == null)
            {
                throw new ArgumentNullException("No Id field was found in the current class.");
            }

            //Console.WriteLine($" id is: {id.GetValue(entity)}");
            return id;
        }

        private string GetTableName(Type entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("Cannot get Table name for null type.");
            }

            if (!entity.IsDefined(typeof(EntityAttribute)))
            {
                throw new ArgumentException("Cannot get Table name of entity.");
            }

            string tableName = entity.GetCustomAttribute<EntityAttribute>().TableName;

            if (tableName == null)
            {
                throw new ArgumentNullException("Table name cannot be null.");
            }

            //Console.WriteLine($"Table name is -> {tableName}");
            return tableName;
        }

        private string GetFieldName(FieldInfo field)
        {
            //Console.WriteLine("This is method GetFieldName -> ");
            if(field == null)
            {
                throw new ArgumentNullException("field cannot be null.");
            }

            if (!field.IsDefined(typeof(ColumnAttribute)))
            {
                return field.Name;
            }

            string columnName = field.GetCustomAttribute<ColumnAttribute>().ColumnName;

            if (columnName == null)
            {
                throw new ArgumentNullException("Column name cannot be null.");
            }

            //Console.WriteLine($"Column name is: {columnName}");
            return columnName;
        }
    }
}
