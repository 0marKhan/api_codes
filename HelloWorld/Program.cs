using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text.Json;
using AutoMapper;
using Dapper;
using HelloWorld.Data;
using HelloWorld.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace HelloWorld{
    internal class Program{
        static void Main(string[] args){
            // for accessing the database string from the config file
            IConfiguration config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            
           /////////////////////////////////////////// USING DAPPER ////////////////////////////////////
           DataContextDapper dapper = new DataContextDapper(config);

            // READING FROM JSON FILE AND ADDING TO DATABASE
            string computerJson = File.ReadAllText("ComputersSnake.json");

            // mapping from ComputerSnake to Computer
            // adds it automatically to ComputerId
            Mapper mapper = new Mapper(new MapperConfiguration((cfg) =>{
                cfg.CreateMap<ComputerSnake, Computer>()
                    // making it so ComputerId maps to computer_id
                    .ForMember(destination => destination.ComputerId, options =>
                        options.MapFrom(source => source.computer_id))
                    .ForMember(destination => destination.CPUcores, options =>
                        options.MapFrom(source => source.cpu_cores))
                    .ForMember(destination => destination.HasLTE, options =>
                        options.MapFrom(source => source.has_lte))
                    .ForMember(destination => destination.MotherBoard, options =>
                        options.MapFrom(source => source.motherboard))
                    .ForMember(destination => destination.VideoCard, options =>
                        options.MapFrom(source => source.video_card))
                    .ForMember(destination => destination.ReleaseDate, options =>
                        options.MapFrom(source => source.release_date))
                    .ForMember(destination => destination.Price, options =>
                        options.MapFrom(source => source.price));
            }));

            // Deserialize JSON into a collection of Computer objects
            IEnumerable<Computer>? computerSystem = System.Text.Json.JsonSerializer.Deserialize<IEnumerable<Computer>>(computerJson);

 
            
            if(computerSystem != null){
                foreach (Computer computer in computerSystem){
                    Console.WriteLine(computer.MotherBoard);
                }           
            }


        //     if (jsonComputers != null)
        //     {
        //         string sql = @"INSERT INTO TutorialAppSchema.Computer (
        //             MotherBoard,
        //             HasWifi,
        //             HasLTE,
        //             ReleaseDate,
        //             Price,
        //             VideoCard
        //         ) VALUES (
        //             @MotherBoard,
        //             @HasWifi,
        //             @HasLTE,
        //             @ReleaseDate,
        //             @Price,
        //             @VideoCard
        //         )";

        //         foreach (Computer computer in jsonComputers)
        //         {
        //             dapper.ExecuteSql(sql, new {
        //                 MotherBoard = computer.MotherBoard,
        //                 HasWifi = computer.HasWifi,
        //                 HasLTE = computer.HasLTE,
        //                 ReleaseDate = computer.ReleaseDate,
        //                 Price = computer.Price,
        //                 VideoCard = computer.VideoCard
        //             });
        //         }
        //     }

        //     // ADDING JSON TO TEXT FILE
        //     // serializing, converting to camel case for json
        //     JsonSerializerSettings settings = new(){
        //         ContractResolver = new CamelCasePropertyNamesContractResolver()
        //     };

        //     string computersCopyNewtonssoft = JsonConvert.SerializeObject(jsonComputers, settings);
        //     File.WriteAllText("computersCopyNewtonssoft.txt", computersCopyNewtonssoft);

           

      
        // }
        }
        static string EscapeSingleQuote(string input){
            string output = input.Replace("'", "''");
            return output;
        }

    }
    
}