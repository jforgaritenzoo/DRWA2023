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
public class GuruController : ControllerBase
{
    private readonly GuruService _guruService;

    public GuruController (GuruService guruService) => _guruService = guruService;

    /// <summary>
    /// Get all Guru Item.
    /// </summary>
    /// <returns>All Guru Item</returns>
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
    public async Task<List<Guru>> Get() => await _guruService.GetAsync();

    /// <summary>
    /// Get a specific Guru Item.
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Get a specific Guru Item</returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     GET /Guru
    ///     {
    ///         "Id": "112233"
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
    public async Task<ActionResult<Guru>> Get(string id)
    {
        var guru = await _guruService.GetAsync(id);

        if (guru is null)
        {
            return NotFound();
        }

        return guru;
    }

    /// <summary>
    /// Creates a Guru Item.
    /// </summary>
    /// <param name="newGuru"></param>
    /// <returns>A newly created Guru Item</returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     POST /Guru
    ///     {
    ///         "Name": "John Doe",
    ///         "Kelas": "D.1.10",
    ///         "NIP": "112233"
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
    public async Task<IActionResult> Post(Guru newGuru)
    {
        await _guruService.CreateAsync(newGuru);

        return CreatedAtAction(nameof(Get), new { id = newGuru.Id }, newGuru);
    }

    /// <summary>
    /// Update an existing Guru Item.
    /// </summary>
    /// <param name="updatedGuru"></param>
    /// <returns>An updated Guru Item</returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     PUT /Guru
    ///     {
    ///         "Id": "ididid",
    ///         "Name": "John Doe",
    ///         "Kelas": "D.1.10",
    ///         "NIP": "112233"
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
    public async Task<IActionResult> Update(string id, Guru updatedGuru)
    {
        var guru = await _guruService.GetAsync(id);

        if (guru is null)
        {
            return NotFound();
        }

        updatedGuru.Id = guru.Id;

        await _guruService.UpdateAsync(id, updatedGuru);

        return NoContent();
    }

    /// <summary>
    /// Deletes a specific Guru Item.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <returns>A deleted Guru Item</returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     DELETE /Guru
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
        var guru = await _guruService.GetAsync(id);

        if (guru is null)
        {
            return NotFound();
        }

        await _guruService.RemoveAsync(id);

        return NoContent();
    }
}

