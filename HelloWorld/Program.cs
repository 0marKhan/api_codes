using System.Data;
using System.Data.Common;
using Dapper;
using HelloWorld.Data;
using HelloWorld.Models;
using Microsoft.Data.SqlClient;

namespace HelloWorld{
    internal class Program{
        static void Main(string[] args){
            
           DataContextDapper dapper = new DataContextDapper();

           DateTime rightNow = dapper.LoadDataSingle<DateTime>("SELECT GETDATE()");

            // querying the database for date
            // string sqlCommand = "SELECT GETDATE()";
            // DateTime rightNow = dbConnection.QuerySingle<DateTime>(sqlCommand);
            // Console.WriteLine(rightNow);

            Computer myComputer = new Computer(){
                MotherBoard = "1234",
                HasWifi = true,
                HasLTE = false,
                ReleaseDate = DateTime.Now,
                Price = 943.43m,
                VideoCard = "RTX 2860"
            };

            // query to add something
            string sql = @"INSERT INTO TutorialAppSchema.Computer (
                MotherBoard,
                HasWifi,
                HasLTE,
                ReleaseDate,
                Price,
                VideoCard
            ) VALUES ('1234', 'true', 'false','1/16/2024 5:17pm', '943.43', 'RTX 2060')";

            // if i wanna get the row count
            // int result = dapper.ExecuteSqlWithRowCount(sql);
            bool result = dapper.ExecuteSql(sql);

            string sqlSelect = @"SELECT 
            Computer.MotherBoard,
            Computer.HasWifi,
            Computer.HasLTE,
            Computer.ReleaseDate,
            Computer.Price,
            Computer.VideoCard
            FROM TutorialAppSchema.Computer";

            // executing query to get the data
           IEnumerable<Computer> computers = dapper.LoadData<Computer>(sqlSelect);
          
            foreach(Computer singleComputer in computers){
                Console.WriteLine(singleComputer.MotherBoard);
                Console.WriteLine(singleComputer.HasWifi);
                Console.WriteLine(singleComputer.HasLTE);
                Console.WriteLine(singleComputer.ReleaseDate);
                Console.WriteLine(singleComputer.Price);
                Console.WriteLine(singleComputer.VideoCard);


            }
        }
    }
}