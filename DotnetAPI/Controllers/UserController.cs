using Microsoft.AspNetCore.Mvc;

namespace DotnetAPI.Controllers;

[ApiController]
// lets us access the route/controller easily
[Route("[controller]")]
public class UserController : ControllerBase
{
    public UserController()
    {

    }


    [HttpGet("test/{testValue}")]
    public string[] Test(string testValue)
    {
        string[] response = new string[]{
        "test1", "test2", testValue
    };
        return response;

    }
}

