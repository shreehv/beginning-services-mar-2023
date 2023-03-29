using LocationsApi.Models;
using Marten;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using static LocationsApi.Models.LocationsResponse;

namespace LocationsApi.Controllers;

public class LocationsController : ControllerBase
{
    private readonly IDocumentSession _context;



    public LocationsController(IDocumentSession context)
    {
        _context = context;
    }



    [HttpGet("/locations")]
    public async Task<ActionResult> GetLocations()
    {
        var data = await _context.Query<LocationItemResponse>().ToListAsync();
        var response = new LocationsResponse { _embedded = data };
        return Ok(response);
    }

    [HttpPost("/locations")]
    public async Task<ActionResult> AddLocation([FromBody] LocationCreate request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var locationToAdd = new LocationItemResponse
        {
            Name = request.Name,
            Description = request.Description,
            Id = Guid.NewGuid().ToString(),
            AddedBy = "bob",
            AddedOn = DateTime.Now,
        };

        _context.Store<LocationItemResponse>(locationToAdd);
        //add all the things
        await _context.SaveChangesAsync();
        return Ok(locationToAdd);

    }

    public record LocationCreate
    {
        [Required, MaxLength(75)]
        public string Name { get; init; } = string.Empty;
        [Required, MaxLength(1000)]
        public string Description { get; init; } = string.Empty;



    }

}
