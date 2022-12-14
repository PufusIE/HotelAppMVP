using Microsoft.Extensions.Configuration;
using System.Data;
using Dapper;
using System.Data.SQLite;

namespace HotelAppDataBaseAccessLibrary.Databases
{
    public class SQLiteDataAccess : ISQLiteDataAccess
    {
        private readonly IConfiguration _config;

        public SQLiteDataAccess(IConfiguration config)
        {
            _config = config;
        }

        //Reading from DB
        public List<T> LoadData<T, U>(string sqlStatement,
                                      U parameters,
                                      string connectionStringName)
        {
            string connectionString = _config.GetConnectionString(connectionStringName);

            // create connection to db, using to always cloe the connection
            using (IDbConnection connection = new SQLiteConnection(connectionString))
            {
                List<T> rows = connection.Query<T>(sqlStatement,
                                                   parameters).ToList();
                return rows;
            }
        }

        //Writing to DB
        public void SaveData<T>(string sqlStatement, T parameters, string connectionStringName,
                                bool isStoredProcedure = false)
        {
            string connectionString = _config.GetConnectionString(connectionStringName);

            using (IDbConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Execute(sqlStatement, parameters);
            }
        }
    }
}
