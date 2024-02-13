using System.Data;
using System.Data.Common;
using Dapper;
using HelloWorld.Data;
using HelloWorld.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace HelloWorld{
    internal class Program{
        static void Main(string[] args){
            // for accessing the database string from the config file
            IConfiguration config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            
           /////////////////////////////////////////// USING DAPPER ////////////////////////////////////
           DataContextDapper dapper = new DataContextDapper(config);

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
                Console.WriteLine("ID: " + singleComputer.ComputerId + ',' + 
                    "Motherboard: " + singleComputer.MotherBoard + ',' + 
                    "Wifi: " + singleComputer.HasWifi + ',' +
                    "LTE: " + singleComputer.HasLTE + ',' +
                    "ReleaseDate: " + singleComputer.ReleaseDate + ',' +
                    "Price: " + singleComputer.Price + ',' +
                    "VideoCard: " + singleComputer.VideoCard);
            }


             ////////////////////////////////////////////////USING ENTITY FRAMEWORK///////////////////////////////////////////
            
            DataContextEntityFramework entityFramework = new DataContextEntityFramework(config);

            Computer Computer2 = new Computer(){
                MotherBoard = "1234",
                HasWifi = true,
                HasLTE = false,
                ReleaseDate = DateTime.Now,
                Price = 943.43m,
                VideoCard = "RTX 2860"
            };

            // adds the new computer to the table 
            entityFramework.Add(Computer2);
            entityFramework.SaveChanges();

            IEnumerable<Computer>? computersEntityFramework = entityFramework.Computer?.ToList<Computer>();
            
            if(computersEntityFramework != null){
                foreach(Computer singleComputer in computersEntityFramework){
                    Console.WriteLine("ID: " + singleComputer.ComputerId + ',' + 
                        "Motherboard: " + singleComputer.MotherBoard + ',' + 
                        "Wifi: " + singleComputer.HasWifi + ',' +
                        "LTE: " + singleComputer.HasLTE + ',' +
                        "ReleaseDate: " + singleComputer.ReleaseDate + ',' +
                        "Price: " + singleComputer.Price + ',' +
                        "VideoCard: " + singleComputer.VideoCard);

                }
            }
        }

    }
}