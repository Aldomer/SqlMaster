using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;

namespace SqlMaster
{
    public class Helper
    {
        public enum ConnectionType
        {
            Collections,
            CollectionsRemote
        }

        #region List<SqlParameter>
        public static DataRow GetFirstRowOfTable(string sql, ConnectionType connectionType, List<SqlParameter> parameters)
        {
            DataTable dataTable = GetDataFromDatabase(sql, connectionType, parameters);

            if (dataTable.Rows.Count > 0)
                return dataTable.Rows[0];

            return null;
        }

        public static SqlParameter CreateSqlParameter(string parameterName, SqlDbType sqlDbType, int size, object value)
        {
            return new SqlParameter() { ParameterName = parameterName, SqlDbType = sqlDbType, Size = size, Value = value };
        }

        public static SqlParameter CreateSqlParameter(string parameterName, SqlDbType sqlDbType, object value)
        {
            return new SqlParameter() { ParameterName = parameterName, SqlDbType = sqlDbType, Value = value };
        }

        public static DataTable GetDataFromDatabase(string sql, ConnectionType connectionType, List<SqlParameter> parameters)
        {
            DataTable dataTable = new DataTable();

            GetDataFromDatabaseAppend(sql, connectionType, parameters, dataTable);

            return dataTable;
        }

        public static void GetDataFromDatabaseAppend(string sql, ConnectionType connectionType, List<SqlParameter> parameters, DataTable dataTable)
        {
            using (SqlDataAdapter dataAdapter = new SqlDataAdapter(sql, GetConnectionString(connectionType)))
            {
                AddParameters(dataAdapter.SelectCommand, parameters);

                SetCommandTypeIfStoredProcedure(dataAdapter.SelectCommand, true);

                SqlFillWithTimer(dataAdapter, dataTable);

                ClearParameters(dataAdapter.SelectCommand);
            }
        }

        private static void AddParameters(SqlCommand cmd, List<SqlParameter> parameters)
        {
            if (parameters != null)
            {
                foreach (SqlParameter parameter in parameters)
                {
                    cmd.Parameters.Add(parameter);
                }
            }
        }

        private static void ClearParameters(SqlCommand cmd)
        {
            cmd.Parameters.Clear();
        }

        public static Object ExecuteScalar(string sql, ConnectionType connectionType, List<SqlParameter> parameters, bool isStoredProcedure = false)
        {
            using (SqlConnection connection = new SqlConnection(GetConnectionString(connectionType)))
            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                connection.Open();
                AddParameters(command, parameters);
                SetCommandTypeIfStoredProcedure(command, isStoredProcedure);
                try
                {
                    return command.ExecuteScalar();
                }
                catch (SqlException sqlException)
                {
                    return null;
                }
            }
        }
        #endregion List<SqlParameter>

        #region Dictionary<string, object>
        public static DataRow GetFirstRowOfTable(string sql, ConnectionType connectionType, Dictionary<string, object> parameters, bool isStoredProcedure = false)
        {
            DataTable dataTable = GetDataFromDatabase(sql, connectionType, parameters, isStoredProcedure);

            if (dataTable.Rows.Count > 0)
                return dataTable.Rows[0];

            return null;
        }

        public static DataTable GetDataFromDatabase(string sql, ConnectionType connectionType, Dictionary<string, object> parameters, bool isStoredProcedure = false)
        {
            DataTable dataTable = new DataTable();

            GetDataFromDatabaseAppend(sql, connectionType, parameters, dataTable, isStoredProcedure);

            return dataTable;
        }

        public static void GetDataFromDatabaseAppend(string sql, ConnectionType connectionType, Dictionary<string, object> parameters, DataTable dataTable, bool isStoredProcedure = false)
        {
            using (SqlDataAdapter dataAdapter = new SqlDataAdapter(sql, GetConnectionString(connectionType)))
            {
                AddParameters(dataAdapter.SelectCommand, parameters);

                SetCommandTypeIfStoredProcedure(dataAdapter.SelectCommand, isStoredProcedure);

                SqlFillWithTimer(dataAdapter, dataTable);
            }
        }

        private static void AddParameters(SqlCommand cmd, Dictionary<string, object> parameters)
        {
            if (parameters != null)
            {
                foreach (string key in parameters.Keys)
                {
                    cmd.Parameters.AddWithValue(key, parameters[key]);
                }
            }
        }

        public static Object ExecuteScalar(string sql, ConnectionType connectionType, Dictionary<string, object> parameters, bool isStoredProcedure = false)
        {
            using (SqlConnection connection = new SqlConnection(GetConnectionString(connectionType)))
            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                connection.Open();
                AddParameters(command, parameters);
                SetCommandTypeIfStoredProcedure(command, isStoredProcedure);
                return command.ExecuteScalar();
            }
        }
        #endregion Dictionary<string, object>

        #region no parameters
        public static DataRow GetFirstRowOfTable(string sql, ConnectionType connectionType, bool isStoredProcedure = false)
        {
            DataTable dataTable = GetDataFromDatabase(sql, connectionType, isStoredProcedure);

            if (dataTable.Rows.Count > 0)
                return dataTable.Rows[0];

            return null;
        }

        public static DataTable GetDataFromDatabase(string sql, ConnectionType connectionType, bool isStoredProcedure = false)
        {
            DataTable dataTable = new DataTable();

            GetDataFromDatabaseAppend(sql, connectionType, dataTable, isStoredProcedure);

            return dataTable;
        }

        public static void GetDataFromDatabaseAppend(string sql, ConnectionType connectionType, DataTable dataTable, bool isStoredProcedure = false)
        {
            using (SqlDataAdapter dataAdapter = new SqlDataAdapter(sql, GetConnectionString(connectionType)))
            {
                SetCommandTypeIfStoredProcedure(dataAdapter.SelectCommand, isStoredProcedure);

                SqlFillWithTimer(dataAdapter, dataTable);
            }
        }

        public static Object ExecuteScalar(string sql, ConnectionType connectionType, bool isStoredProcedure = false)
        {
            using (SqlConnection connection = new SqlConnection(GetConnectionString(connectionType)))
            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                connection.Open();
                SetCommandTypeIfStoredProcedure(command, isStoredProcedure);
                return command.ExecuteScalar();
            }
        }
        #endregion no parameters

        public static void SqlFillWithTimer(SqlDataAdapter adapter, DataTable table)
        {
            //this has a timer

            var sw = new Stopwatch();
            sw.Start();
            adapter.Fill(table);
            sw.Stop();
        }

        public static int TableSizeInBytes(DataTable table)
        {
            int size = 0;

            foreach (DataRow row in table.Rows)
            {
                foreach (var item in row.ItemArray)
                {
                    if (item is int)
                        size += 4;
                    else if (item is string)
                        size += ((string)item).Length;
                    else if (item is decimal)
                        size += 16;
                    else if (item is double)
                        size += 8;
                    else if (item is bool)
                        size += 1;
                    else if (item is DBNull)
                        size += 1;
                    else if (item is DateTime)
                        size += 8;
                    else if (item is Guid)
                        size += 16;
                    else if (item is Byte)
                        size += 1;
                    else if (item.GetType() == typeof(byte[]))
                        size += ((byte[])item).Length;
                    else
                        throw new Exception("Unable to compute result size in UtilER.TableSizeInBytes().  Missing type: " + item.GetType());
                }
            }

            return size;
        }

        private static void SetCommandTypeIfStoredProcedure(SqlCommand sqlCommand, bool isStoredProcedure)
        {
            if (isStoredProcedure)
                sqlCommand.CommandType = CommandType.StoredProcedure;
        }

        public static SqlResponse UpdateDatabase(string sql, ConnectionType connectionType, List<SqlParameter> parameters)
        {
            SqlResponse sqlResponse = new SqlResponse();

            try
            {
                using (SqlConnection connection = new SqlConnection(GetConnectionString(connectionType)))
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    connection.Open();
                    AddParameters(command, parameters);
                    SetCommandTypeIfStoredProcedure(command, true);
                    command.ExecuteNonQuery();

                    sqlResponse.Success = true;
                }
            }
            catch (Exception exception)
            {
                sqlResponse.Success = false;
                sqlResponse.Exception = exception;
            }

            return sqlResponse;
        }

        public static SqlResponse InsertIntoDatabaseNoReturnId(string sql, ConnectionType connectionType, List<SqlParameter> parameters)
        {
            return UpdateDatabase(sql, connectionType, parameters);
        }

        public static SqlResponse InsertIntoDatabase(string sql, ConnectionType connectionType, List<SqlParameter> parameters)
        {
            SqlResponse sqlResponse = new SqlResponse();

            try
            {
                using (SqlConnection connection = new SqlConnection(GetConnectionString(connectionType)))
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    connection.Open();
                    AddParameters(command, parameters);
                    SetCommandTypeIfStoredProcedure(command, true);

                    int newId = -1;

                    try
                    {
                        newId = Convert.ToInt32(command.ExecuteScalar());
                    }
                    catch (Exception exception)
                    {
                        throw new Exception("Unable to convert returned Id");
                    }

                    sqlResponse.Success = true;
                    sqlResponse.NewId = newId;
                }
            }
            catch (Exception exception)
            {
                sqlResponse.Success = false;
                sqlResponse.Exception = exception;
            }

            return sqlResponse;
        }

        public static int UpdateDatabase(string sql, ConnectionType connectionType, Dictionary<string, object> parameters, bool isStoredProcedure = false)
        {
            using (SqlConnection connection = new SqlConnection(GetConnectionString(connectionType)))
            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                connection.Open();
                AddParameters(command, parameters);
                SetCommandTypeIfStoredProcedure(command, isStoredProcedure);
                return command.ExecuteNonQuery();
            }
        }

        public static string GetConnectionString(ConnectionType connectionType)
        {
            switch (connectionType)
            {
                case ConnectionType.Collections:
                    return GetCollectionsConnectionString();
                case ConnectionType.CollectionsRemote:
                    return GetCollectionsLaptopConnectionString();
                default:
                    return String.Empty;
            }
        }

        public static string GetCollectionsConnectionString()
        {
            return new SqlConnectionStringBuilder()
            {
                DataSource = "WEAPONX\\MYCOLLECTIONS",
                InitialCatalog = "Collections",
                UserID = "CollectionsUser",
                Password = "Otsuka@1"
            }.ConnectionString;
        }

        public static string GetCollectionsLaptopConnectionString()
        {
            return new SqlConnectionStringBuilder()
            {
                DataSource = "tcp:WeaponX,49172",
                InitialCatalog = "Collections",
                UserID = "CollectionsUser",
                Password = "Otsuka@1"
            }.ConnectionString;
        }
    }
}
