using System.Net;
using BookStoreApi.Filters;
using BookStoreApi.Models;
using BookStoreApi.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BookStoreApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class BooksController : ControllerBase
{
    private readonly BooksService _booksService;

    public BooksController(BooksService booksService) => _booksService = booksService;

    /// <summary>
    /// Get all BookStore Item.
    /// </summary>
    /// <returns>All BookStore Item</returns>
    /// <response code="200">Returns all the item</response>
    /// <response code="400">If there is no item</response>
    /// <response code="401">Client request has not been completed because it lacks valid authentication credentials for the requested resource</response>
    /// <response code="404">If the item could not be found</response>
    /// <response code="500">If the request on the server failed unexpectedly</response>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<List<Book>> Get() => await _booksService.GetAsync();

    /// <summary>
    /// Get a specific BookStore Item.
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Get a specific BookStore Item</returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     GET /BookStore
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
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<Book>> Get(string id)
    {
        var book = await _booksService.GetAsync(id);

        if (book is null)
        {
            return NotFound();
        }

        return book;
    }

    /// <summary>
    /// Creates a BookStore Item.
    /// </summary>
    /// <param name="newBook"></param>
    /// <returns>A newly created BookStore Item</returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     POST /BookStore
    ///     {
    ///         "Name": "BookName",
    ///         "Price": 1.0,
    ///         "Category": "BookCategory",
    ///         "Author": "John Doe"
    ///     }
    ///
    /// </remarks>
    /// <response code="201">Returns the newly created item</response>
    /// <response code="400">If the item is null</response>
    /// <response code="401">Client request has not been completed because it lacks valid authentication credentials for the requested resource</response>
    /// <response code="404">If the item could not be found</response>
    /// <response code="500">If the request on the server failed unexpectedly</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Post(Book newBook)
    {
        // if (!ModelState.IsValid)
        // {
        //     var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
        //     var response = new HttpResponse(400, new { Errors = errors }, "application/json");
        //     return (IActionResult)await Task.FromResult(response);
        // }
        await _booksService.CreateAsync(newBook);

        return CreatedAtAction(nameof(Get), new { id = newBook.Id }, newBook);
    }

    /// <summary>
    /// Update an existing BookStore Item.
    /// </summary>
    /// <param name="updatedBook"></param>
    /// <returns>An updated BookStore Item</returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     PUT /BookStore
    ///     {
    ///         "Id": "ididid",
    ///         "Name": "BookName",
    ///         "Price": 1.0,
    ///         "Category": "BookCategory",
    ///         "Author": "John Doe"
    ///     }
    ///
    /// </remarks>
    /// <response code="204">Returns the updated item</response>
    /// <response code="400">If the item is null</response>
    /// <response code="401">Client request has not been completed because it lacks valid authentication credentials for the requested resource</response>
    /// <response code="404">If the item could not be found</response>
    /// <response code="500">If the request on the server failed unexpectedly</response>
    [HttpPut("{id:length(24)}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Update(string id, Book updatedBook)
    {
        var book = await _booksService.GetAsync(id);

        if (book is null)
        {
            return NotFound();
        }

        updatedBook.Id = book.Id;

        await _booksService.UpdateAsync(id, updatedBook);

        return NoContent();
    }

    /// <summary>
    /// Deletes a specific BookStore Item.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <returns>A deleted BookStore Item</returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     DELETE /BookStore
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
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Delete(string id)
    {
        var book = await _booksService.GetAsync(id);

        if (book is null)
        {
            return NotFound();
        }

        await _booksService.RemoveAsync(id);

        return NoContent();
    }
}

public class HttpResponse
{
    public int StatusCode { get; set; }
    public object Body { get; set; }
    public string ContentType { get; set; }

    public HttpResponse(int statusCode, object body, string contentType)
    {
        StatusCode = statusCode;
        Body = body;
        ContentType = contentType;
    }

    public async Task WriteAsync(HttpContext context)
    {
        context.Response.StatusCode = StatusCode;
        context.Response.ContentType = ContentType;

        if (Body != null)
        {
            var json = JsonConvert.SerializeObject(Body);
            await context.Response.WriteAsync(json);
        }
    }
}
