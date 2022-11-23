using BuggyAspneture.API.Contracts;
using BuggyAspneture.DataAccess;
using BuggyAspneture.DataAccess.PostgreSQL;
using BuggyAspneture.DataAccess.PostgreSQL.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR.Protocol;
using System.Net;
using System.Net.Mime;

namespace BuggyAspneture.API.Controllers;

[ApiController]
[Route("v2/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[Consumes(MediaTypeNames.Application.Json)]
public class OpenLoopsController : ControllerBase
{
    private readonly ILogger<OpenLoopsController> _logger;
    private readonly BuggyAspnetureDbContext _context;

    public OpenLoopsController(ILogger<OpenLoopsController> logger, BuggyAspnetureDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    [HttpGet("Get all Notes")]
    [ProducesResponseType(typeof(GetOpenLoopsResponse), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Get()
    {
        var openLoops = _context.OpenLoops.ToArray();

        var response = new GetOpenLoopsResponse
        {
            OpenLoops = openLoops.Select(x => new GetOpenLoopDto
            {
                Id = x.Id,
                Note = x.Note,
                CreatedDate = x.CreatedDate
            }).ToArray()
        };

        return Ok(response);
    }

    [HttpPost("Add Note")]
    [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Create([FromBody] CreateOpenLoopsRequest request)
    {
        var openLoop = new OpenLoopEntity
        {
            Id = Guid.NewGuid(),
            Note = request.Note,
            CreatedDate = DateTimeOffset.UtcNow,
            UserId = 1 //
        };

        _context.OpenLoops.Add(openLoop);
        await _context.SaveChangesAsync();
        return Ok(openLoop.Id);
    }

    //[HttpPut("Change note")]
    //[ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
    //public async Task<IActionResult> Update([FromQuery] UpdateOpenLoopsRequest request)
    //{
    //    if (Guid.TryParse(request.Id, out Guid id))
    //    {
    //        var result = OpenLoopsRepository.Update(id, request.NewText);
    //        return result != null ? Ok($"File is updated. Path: {result}") : NotFound($"File with {id} is not found.");
    //    }
    //    else
    //    {
    //        return BadRequest("The request must contain the file's guid.");
    //    }
    //}

    //[HttpDelete("Close Note")]
    //[ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
    //public async Task<IActionResult> Delete([FromQuery] DeleteOpenLoopsRequest request)
    //{
    //    if (Guid.TryParse(request.Id, out Guid id))
    //    {
    //        var result = OpenLoopsRepository.Delete(id);
    //        return result != null ? Ok($"File is deleted. Path: {result}") : NotFound($"File with {id} is not found.");
    //    }
    //    else
    //    {
    //        _logger.LogError("The user doesn't understand what is he doing.");
    //        return BadRequest("The request must contain the file's guid.");
    //    }
    //}
}

[ApiController]
[Route("[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[Consumes(MediaTypeNames.Application.Json)]
public class OpenLoopsV1Controller : ControllerBase
{
    private readonly ILogger<OpenLoopsV1Controller> _logger;

    public OpenLoopsV1Controller(ILogger<OpenLoopsV1Controller> logger)
    {
        _logger = logger;
    }

    [HttpGet("Get all Notes")]
    [ProducesResponseType(typeof(GetOpenLoopsResponse), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Get()
    {
        var openLoops = OpenLoopsRepository.Get();

        var response = new GetOpenLoopsResponse
        {
            OpenLoops = openLoops.Select(x => new GetOpenLoopDto
            {
                Id = x.Id,
                Note = x.Note,
                CreatedDate = x.CreatedDate
            }).ToArray()
        };

        return Ok(response);
    }

    [HttpPost("Add Note")]
    [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Create([FromBody] CreateOpenLoopsRequest request)
    {
        var openLoop = new OpenLoop
        {
            Note = request.Note,
            CreatedDate = DateTimeOffset.UtcNow
        };
        var openLoopId = OpenLoopsRepository.Add(openLoop);
        return Ok(openLoopId);
    }
    
    [HttpPut("Change note")]
    [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Update([FromQuery] UpdateOpenLoopsRequest request)
    {
        if (Guid.TryParse(request.Id, out Guid id))
        { 
            var result = OpenLoopsRepository.Update(id, request.NewText); 
            return result != null ? Ok($"File is updated. Path: {result}") : NotFound($"File with {id} is not found.");
        }
        else
        {
            return BadRequest("The request must contain the file's guid.");
        }
    }

    [HttpDelete("Close Note")]
    [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Delete([FromQuery] DeleteOpenLoopsRequest request)
    {
        if (Guid.TryParse(request.Id, out Guid id))
        {
            var result = OpenLoopsRepository.Delete(id);
            return result != null ? Ok($"File is deleted. Path: {result}") : NotFound($"File with {id} is not found.");
        }
        else
        {
            _logger.LogError("The user doesn't understand what is he doing.");
            return BadRequest("The request must contain the file's guid.");
        }
    }
}