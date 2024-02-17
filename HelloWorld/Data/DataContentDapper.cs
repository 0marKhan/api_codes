using System.Data;
using Dapper;
using HelloWorld.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace HelloWorld.Data;

public class DataContextDapper{
    // underscore to make it private
    private string? _connectionString;
    public DataContextDapper(IConfiguration config){
        _connectionString = config.GetConnectionString("DefaultConnection");
    }

    public IEnumerable<T> LoadData<T>(string sql){
          // can convert it into a list like this
          // List<Computer> computerList = dbConnection.Query<Computer>(sqlSelect).ToList();
        IDbConnection dbConnection = new SqlConnection(_connectionString);
        return dbConnection.Query<T>(sql);
    }

    public T LoadDataSingle<T>(string sql){
        IDbConnection dbConnection = new SqlConnection(_connectionString);
        return dbConnection.QuerySingle<T>(sql);
    }
    // returns true if the sql code runs
    public bool ExecuteSql(string sql){
        IDbConnection dbConnection = new SqlConnection(_connectionString);
        return (dbConnection.Execute(sql) > 0);
    }

    public bool ExecuteSql(string sql, object param = null){
    IDbConnection dbConnection = new SqlConnection(_connectionString);
    return (dbConnection.Execute(sql, param) > 0);
}

    // returns the number of rows if the sql code runs
    public int ExecuteSqlWithRowCount(string sql){
        IDbConnection dbConnection = new SqlConnection(_connectionString);
        return dbConnection.Execute(sql);
    }

    

}