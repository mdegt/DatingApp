using API.Controllers;
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SQLitePCL;

namespace API;

public class BuggyController : BaseApiController
{
    private DataContext _context;

    public BuggyController(DataContext context) 
    {
        _context = context;
    }

    [Authorize]
    [HttpGet("auth")]
    public ActionResult<string> GetSecret() 
    {
        return "secret text";
    }
    [HttpGet("not-found")]
    public ActionResult<AppUser> GetNotFound() 
    {
        var thing = _context.Users.Find(-1);
        if (thing == null) return new NotFoundResult();
        return thing;
    }

    [HttpGet("server-error")]
    public ActionResult<string> GetServerError() 
    {
        var thing = _context.Users.Find(-1);
        var thingToReturn = thing.ToString();
        return thingToReturn;
    }
 [HttpGet("bad-request")]
    public ActionResult<string> GetBadRequest() 
    {
        return new BadRequestObjectResult("This was not a good request");
    }

}
