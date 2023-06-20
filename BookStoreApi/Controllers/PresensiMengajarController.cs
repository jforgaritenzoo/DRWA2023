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
public class PresensiMengajarController : ControllerBase
{
    private readonly PresensiMengajarService _presensiMengajarService;

    public PresensiMengajarController (PresensiMengajarService presensiMengajarService) => _presensiMengajarService = presensiMengajarService;

    /// <summary>
    /// Get all PresensiMengajar Item.
    /// </summary>
    /// <returns>All PresensiMengajar Item</returns>
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
    public async Task<List<PresensiMengajar>> Get() => await _presensiMengajarService.GetAsync();

    /// <summary>
    /// Get a specific PresensiMengajar Item.
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Get a specific PresensiMengajar Item</returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     GET /PresensiMengajar
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
    public async Task<ActionResult<PresensiMengajar>> Get(string id)
    {
        var presensiMengajar = await _presensiMengajarService.GetAsync(id);

        if (presensiMengajar is null)
        {
            return NotFound();
        }

        return presensiMengajar;
    }

    /// <summary>
    /// Creates a PresensiMengajar Item.
    /// </summary>
    /// <param name="newPresensiMengajar"></param>
    /// <returns>A newly created PresensiMengajar Item</returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     POST /PresensiMengajar
    ///     {
    ///         "NIP": "112233",
    ///         "tgl": "1-1-1970",
    ///         "Kehadiran": false,
    ///         "Kelas": "C.3.2",
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
    public async Task<IActionResult> Post(PresensiMengajar newPresensiMengajar)
    {
        await _presensiMengajarService.CreateAsync(newPresensiMengajar);

        return CreatedAtAction(nameof(Get), new { id = newPresensiMengajar.Id }, newPresensiMengajar);
    }

    /// <summary>
    /// Update an existing PresensiMengajar Item.
    /// </summary>
    /// <param name="updatedPresensiMengajar"></param>
    /// <returns>An updated PresensiMengajar Item</returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     PUT /PresensiMengajar
    ///     {
    ///         "Id": "ididid",
    ///         "NIP": "112233",
    ///         "tgl": "1-1-1970",
    ///         "Kehadiran": false,
    ///         "Kelas": "C.3.2",
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
    public async Task<IActionResult> Update(string id, PresensiMengajar updatedPresensiMengajar)
    {
        var presensiMengajar = await _presensiMengajarService.GetAsync(id);

        if (presensiMengajar is null)
        {
            return NotFound();
        }

        updatedPresensiMengajar.Id = presensiMengajar.Id;

        await _presensiMengajarService.UpdateAsync(id, updatedPresensiMengajar);

        return NoContent();
    }

    /// <summary>
    /// Deletes a specific PresensiMengajar Item.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <returns>A deleted PresensiMengajar Item</returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     DELETE /PresensiMengajar
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
        var presensiMengajar = await _presensiMengajarService.GetAsync(id);

        if (presensiMengajar is null)
        {
            return NotFound();
        }

        await _presensiMengajarService.RemoveAsync(id);

        return NoContent();
    }
}

