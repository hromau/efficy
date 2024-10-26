using Efficy.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;

namespace Efficy.Controllers;

[Route("api/")]
[ApiController]
public class TeamsController(IDbContextFactory<EfficyDbContext> contextFactory) : Controller
{
    [HttpPost("counters/{id}")]
    [ExistsValidation]
    [SwaggerOperation("I want to be able to create a new counter",
        "So that steps can be accumulated for a team of one or multiple employees")]
    public async Task<IActionResult> AddCounter([FromRoute] Guid id, [FromBody] string? description)
    {
        var context = await contextFactory.CreateDbContextAsync();
        var team = await context.Teams.FirstAsync(t => t.Id == id);
        var counter = new Counter { Team = team, Description = description };
        context.Counters.Add(counter);
        await context.SaveChangesAsync();
        return Created();
    }

    [HttpPut("counters/{id}")]
    [ExistsValidation]
    [SwaggerOperation("I want to be able to increment the value of a stored counter",
        "So that I can get steps counted towards my team's score")]
    public async Task<IActionResult> IncrementCounter(Guid id, [FromBody] int steps)
    {
        var context = await contextFactory.CreateDbContextAsync();
        var counter = await context.Counters.Include(x => x.Team).FirstAsync(c => c.Id == id);
        counter.Steps += steps;
        await context.SaveChangesAsync();
        return Ok(counter);
    }

    [HttpGet("teams/{id}")]
    [ExistsValidation]
    [SwaggerOperation("I want to get the current total steps taken by a team",
        "So that I can see how much that team have walked in total")]
    public async Task<IActionResult> GetCountersByTeamId(Guid id)
    {
        var context = await contextFactory.CreateDbContextAsync();
        var result = await context.Teams.AsNoTracking().Include(x => x.Counters).FirstAsync(x => x.Id == id);
        return Ok(new
        {
            Team = result.Id, Name = result.Name, Total = result.Counters?.Sum(x => x.Steps),
            Counters = result.Counters.ToArray()
        });
    }

    [HttpGet("teams/list")]
    [SwaggerOperation("I want to list all teams and see their step counts",
        "So that I can compare my team with the others")]
    public async Task<IActionResult> ListTeamsAndCounters()
    {
        var context = await contextFactory.CreateDbContextAsync();
        var result = await context.Teams.AsNoTracking().Include(x => x.Counters).Select(x => new
                { Sum = x.Counters.Sum(c => c.Steps), Name = x.Name, Team = x.Id, Counters = x.Counters.ToArray() })
            .ToListAsync();
        return Ok(result);
    }

    [HttpGet("teams/{id}/list")]
    [ExistsValidation]
    [SwaggerOperation("I want to list all counters in a team",
        "So that I can see how much each team member have walked")]
    public async Task<IActionResult> ListAllCounters(Guid id)
    {
        var context = await contextFactory.CreateDbContextAsync();
        var result = await context.Teams.AsNoTracking().Include(x => x.Counters).FirstAsync(x => x.Id == id);
        return Ok(result.Counters.ToList());
    }

    [HttpPost("teams")]
    [SwaggerOperation("Create team")]
    public async Task<IActionResult> CreateTeam([FromBody] Team entity)
    {
        var context = await contextFactory.CreateDbContextAsync();
        await context.Teams.AddAsync(entity);
        await context.SaveChangesAsync();
        return Created();
    }

    [HttpDelete("teams/{id}")]
    [ExistsValidation]
    [SwaggerOperation("Delete team")]
    public async Task<IActionResult> DeleteTeam(Guid id)
    {
        var context = await contextFactory.CreateDbContextAsync();
        var entity = await context.Teams.FirstAsync(x => x.Id == id);
        context.Teams.Remove(entity);
        await context.SaveChangesAsync();
        return NoContent();
    }

    [HttpPost("counters")]
    [SwaggerOperation("Create counter")]
    public async Task<IActionResult> CreateCounter([FromBody] Counter entity)
    {
        var context = await contextFactory.CreateDbContextAsync();
        await context.Counters.AddAsync(entity);
        await context.SaveChangesAsync();
        return Created();
    }

    [HttpDelete("counters/{id}")]
    [ExistsValidation]
    [SwaggerOperation("Delete counter")]
    public async Task<IActionResult> DeleteCounter(Guid id)
    {
        var context = await contextFactory.CreateDbContextAsync();
        var entity = await context.Counters.FirstAsync(x => x.Id == id);
        context.Counters.Remove(entity);
        await context.SaveChangesAsync();
        return NoContent();
    }
}