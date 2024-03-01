using Microsoft.AspNetCore.Mvc;

namespace DotnetAPI.Controllers;

[ApiController]
// lets us access the route/controller easily
[Route("[controller]")]
public class UserController : ControllerBase
{
    DataContextDapper _dapper;
    public UserController(IConfiguration config)
    {
        _dapper = new DataContextDapper(config);
    }

    [HttpGet("TestConnection")]
    public DateTime TestConnection()
    {
        return _dapper.LoadDataSingle<DateTime>("SELECT GETDATE()");
    }

    // GETTING ALL USERS
    [HttpGet("GetUsers")]
    public IEnumerable<User> GetUsers()
    {
        string sql = @"SELECT [UserId],
                        [FirstName],
                        [LastName],
                        [Email],
                        [Gender],
                        [Active] 
                    FROM TutorialAppSchema.Users";
        IEnumerable<User> users = _dapper.LoadData<User>(sql);
        return users;


    }

    // GETTING A SINGLE USER
    [HttpGet("GetSingleUser/{userId}")]
    public User GetSingleUser(int userId)
    {
        string sql = @"SELECT [UserId],
                        [FirstName],
                        [LastName],
                        [Email],
                        [Gender],
                        [Active] 
                    FROM TutorialAppSchema.Users 
                    WHERE UserId = " + userId.ToString(); ;
        User users = _dapper.LoadDataSingle<User>(sql);
        return users;
    }

    // EDITING A USER
    [HttpPut("EditUser")]
    public IActionResult EditUser(User user)
    {
        string sql = @"UPDATE TutorialAppSchema.Users
                        SET [FirstName] = '" + user.FirstName + @"',
                            [LastName] = '" + user.LastName + @"',
                            [Email] = '" + user.Email + @"',
                            [Gender] = '" + user.Gender + @"',
                            [Active] = '" + user.Active + @"'
                         WHERE UserId = " + user.UserId;

        // using this to see what sql query looks like when api call made
        Console.WriteLine(sql);


        if (_dapper.ExecuteSql(sql))
        {
            return Ok();
        }
        throw new Exception("Failed to Update User");

    }

    // ADDING A USER
    [HttpPost("AddUser")]
    public IActionResult AddUser()
    {

        return Ok();
    }
}

