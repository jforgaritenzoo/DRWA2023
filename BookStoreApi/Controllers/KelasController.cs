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
public class KelasController : ControllerBase
{
    private readonly KelasService _kelasService;

    public KelasController (KelasService kelasService) => _kelasService = kelasService;

    /// <summary>
    /// Get all Kelas Item.
    /// </summary>
    /// <returns>All Kelas Item</returns>
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
    public async Task<List<Kelas>> Get() => await _kelasService.GetAsync();

    /// <summary>
    /// Get a specific Kelas Item.
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Get a specific Kelas Item</returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     GET /Kelas
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
    public async Task<ActionResult<Kelas>> Get(string id)
    {
        var kelas = await _kelasService.GetAsync(id);

        if (kelas is null)
        {
            return NotFound();
        }

        return kelas;
    }

    /// <summary>
    /// Creates a Kelas Item.
    /// </summary>
    /// <param name="newKelas"></param>
    /// <returns>A newly created Kelas Item</returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     POST /Kelas
    ///     {
    ///         "Name": "C.3.2",
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
    public async Task<IActionResult> Post(Kelas newKelas)
    {
        await _kelasService.CreateAsync(newKelas);

        return CreatedAtAction(nameof(Get), new { id = newKelas.Id }, newKelas);
    }

    /// <summary>
    /// Update an existing Kelas Item.
    /// </summary>
    /// <param name="updatedKelas"></param>
    /// <returns>An updated Kelas Item</returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     PUT /Kelas
    ///     {
    ///         "Id": "ididid",
    ///         "Name": "C.3.2",
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
    public async Task<IActionResult> Update(string id, Kelas updatedKelas)
    {
        var kelas = await _kelasService.GetAsync(id);

        if (kelas is null)
        {
            return NotFound();
        }

        updatedKelas.Id = kelas.Id;

        await _kelasService.UpdateAsync(id, updatedKelas);

        return NoContent();
    }

    /// <summary>
    /// Deletes a specific Kelas Item.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <returns>A deleted Kelas Item</returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     DELETE /Kelas
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
        var kelas = await _kelasService.GetAsync(id);

        if (kelas is null)
        {
            return NotFound();
        }

        await _kelasService.RemoveAsync(id);

        return NoContent();
    }
}

