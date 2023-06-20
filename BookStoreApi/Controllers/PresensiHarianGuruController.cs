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
public class PresensiHarianGuruController : ControllerBase
{
    private readonly PresensiHarianGuruService _presensiHarianGuruService;

    public PresensiHarianGuruController (PresensiHarianGuruService presensiHarianGuruService) => _presensiHarianGuruService = presensiHarianGuruService;

    /// <summary>
    /// Get all PresensiHarianGuru Item.
    /// </summary>
    /// <returns>All PresensiHarianGuru Item</returns>
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
    public async Task<List<PresensiHarianGuru>> Get() => await _presensiHarianGuruService.GetAsync();

    /// <summary>
    /// Get a specific PresensiHarianGuru Item.
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Get a specific PresensiHarianGuru Item</returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     GET /PresensiHarianGuru
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
    public async Task<ActionResult<PresensiHarianGuru>> Get(string id)
    {
        var presensiharianguru = await _presensiHarianGuruService.GetAsync(id);

        if (presensiharianguru is null)
        {
            return NotFound();
        }

        return presensiharianguru;
    }

    /// <summary>
    /// Creates a PresensiHarianGuru Item.
    /// </summary>
    /// <param name="newPresensiHarianGuru"></param>
    /// <returns>A newly created PresensiHarianGuru Item</returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     POST /PresensiHarianGuru
    ///     {
    ///         "NIP": "112233",
    ///         "tgl": "1-1-1970",
    ///         "Kehadiran": false
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
    public async Task<IActionResult> Post(PresensiHarianGuru newPresensiHarianGuru)
    {
        await _presensiHarianGuruService.CreateAsync(newPresensiHarianGuru);

        return CreatedAtAction(nameof(Get), new { id = newPresensiHarianGuru.Id }, newPresensiHarianGuru);
    }

    /// <summary>
    /// Update an existing PresensiHarianGuru Item.
    /// </summary>
    /// <param name="updatedPresensiHarianGuru"></param>
    /// <returns>An updated PresensiHarianGuru Item</returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     PUT /PresensiHarianGuru
    ///     {
    ///         "Id": "ididid",
    ///         "NIP": "112233",
    ///         "tgl": "1-1-1970",
    ///         "Kehadiran": false
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
    public async Task<IActionResult> Update(string id, PresensiHarianGuru updatedPresensiHarianGuru)
    {
        var presensiharianguru = await _presensiHarianGuruService.GetAsync(id);

        if (presensiharianguru is null)
        {
            return NotFound();
        }

        updatedPresensiHarianGuru.Id = presensiharianguru.Id;

        await _presensiHarianGuruService.UpdateAsync(id, updatedPresensiHarianGuru);

        return NoContent();
    }

    /// <summary>
    /// Deletes a specific PresensiHarianGuru Item.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <returns>A deleted PresensiHarianGuru Item</returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     DELETE /PresensiHarianGuru
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
        var presensiharianguru = await _presensiHarianGuruService.GetAsync(id);

        if (presensiharianguru is null)
        {
            return NotFound();
        }

        await _presensiHarianGuruService.RemoveAsync(id);

        return NoContent();
    }
}

