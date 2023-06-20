using System.Net;
using BookStoreApi.Filters;
using BookStoreApi.Models;
using BookStoreApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BookStoreApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class MapelController : ControllerBase
{
    private readonly MapelService _mapelService;

    public MapelController (MapelService mapelService) => _mapelService = mapelService;

    /// <summary>
    /// Get all Mapel Item.
    /// </summary>
    /// <returns>All Mapel Item</returns>
    /// <response code="200">Returns all the item</response>
    /// <response code="400">If there is no item</response>
    /// <response code="401">Client request has not been completed because it lacks valid authentication credentials for the requested resource</response>
    /// <response code="404">If the item could not be found</response>
    /// <response code="500">If the request on the server failed unexpectedly</response>
    [HttpGet]
    // [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<List<Mapel>> Get() => await _mapelService.GetAsync();

    /// <summary>
    /// Get a specific Mapel Item.
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Get a specific Mapel Item</returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     GET /Mapel
    ///     {
    ///         "Id": "IDIDIDI"
    ///     }
    ///
    /// </remarks>
    /// <response code="200">Returns a specific item</response>
    /// <response code="400">If the specific item is null</response>
    /// <response code="401">Client request has not been completed because it lacks valid authentication credentials for the requested resource</response>
    /// <response code="404">If the item could not be found</response>
    /// <response code="500">If the request on the server failed unexpectedly</response>
    [HttpGet("{id:length(24)}")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<Mapel>> Get(string id)
    {
        var mapel = await _mapelService.GetAsync(id);

        if (mapel is null)
        {
            return NotFound();
        }

        return mapel;
    }

    /// <summary>
    /// Creates a Mapel Item.
    /// </summary>
    /// <param name="newMapel"></param>
    /// <returns>A newly created Mapel Item</returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     POST /Mapel
    ///     {
    ///         "Name": "Data Warehouse",
    ///         "Kelas": "C.3.2"
    ///     }
    ///
    /// </remarks>
    /// <response code="201">Returns the newly created item</response>
    /// <response code="400">If the item is null</response>
    /// <response code="401">Client request has not been completed because it lacks valid authentication credentials for the requested resource</response>
    /// <response code="404">If the item could not be found</response>
    /// <response code="500">If the request on the server failed unexpectedly</response>
    [HttpPost]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Post(Mapel newMapel)
    {
        await _mapelService.CreateAsync(newMapel);

        return CreatedAtAction(nameof(Get), new { id = newMapel.Id }, newMapel);
    }

    /// <summary>
    /// Update an existing Mapel Item.
    /// </summary>
    /// <param name="updatedMapel"></param>
    /// <returns>An updated Mapel Item</returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     PUT /Mapel
    ///     {
    ///         "Id": "ididid",
    ///         "Name": "Data Warehouse",
    ///         "Kelas": "C.3.2"
    ///     }
    ///
    /// </remarks>
    /// <response code="204">Returns the updated item</response>
    /// <response code="400">If the item is null</response>
    /// <response code="401">Client request has not been completed because it lacks valid authentication credentials for the requested resource</response>
    /// <response code="404">If the item could not be found</response>
    /// <response code="500">If the request on the server failed unexpectedly</response>
    [HttpPut("{id:length(24)}")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Update(string id, Mapel updatedMapel)
    {
        var mapel = await _mapelService.GetAsync(id);

        if (mapel is null)
        {
            return NotFound();
        }

        updatedMapel.Id = mapel.Id;

        await _mapelService.UpdateAsync(id, updatedMapel);

        return NoContent();
    }

    /// <summary>
    /// Deletes a specific Mapel Item.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <returns>A deleted Mapel Item</returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     DELETE /Mapel
    ///     {
    ///         "Id": "ididid"
    ///     }
    ///
    /// </remarks>
    /// <response code="204">Item successfully deleted</response>
    /// <response code="400">If the item is null</response>
    /// <response code="401">Client request has not been completed because it lacks valid authentication credentials for the requested resource</response>
    /// <response code="404">If the item could not be found</response>
    /// <response code="500">If the request on the server failed unexpectedly</response>
    [HttpDelete("{id:length(24)}")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Delete(string id)
    {
        var mapel = await _mapelService.GetAsync(id);

        if (mapel is null)
        {
            return NotFound();
        }

        await _mapelService.RemoveAsync(id);

        return NoContent();
    }
}

