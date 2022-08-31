using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelAppLibrary.Databases;

public class SQLDataAccess : ISQLDataAccess
{
    private readonly IConfiguration _config;

    public SQLDataAccess(IConfiguration config)
    {
        _config = config;
    }

    //Reading from DB
    public List<T> LoadData<T, U>(string sqlStatement,
                                  U parameters,
                                  string connectionStringName,
                                  bool isStoredProcedure = false)
    {
        CommandType commandType = CommandType.Text;
        if (isStoredProcedure == true)
        {
            commandType = CommandType.StoredProcedure;
        }
        
        string connectionString = _config.GetConnectionString(connectionStringName);

        // create connection to db, using to always cloe the connection
        using (IDbConnection connection = new SqlConnection(connectionString))
        {
            List<T> rows = connection.Query<T>(sqlStatement,
                                               parameters,
                                               commandType: commandType).ToList();
            return rows;
        }
    }

    //Writing to DB
    public void SaveData<T>(string sqlStatement, T parameters, string connectionStringName,
                            bool isStoredProcedure = false)
    {
        CommandType commandType = CommandType.Text;
        if (isStoredProcedure == true)
        {
            commandType = CommandType.StoredProcedure;
        }
        string connectionString = _config.GetConnectionString(connectionStringName);

        using (IDbConnection connection = new SqlConnection(connectionString))
        {
            connection.Execute(sqlStatement, parameters, commandType: commandType);
        }
    }
}
