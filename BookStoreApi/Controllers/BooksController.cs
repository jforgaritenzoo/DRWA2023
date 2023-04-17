using System.Net;
using BookStoreApi.Filters;
using BookStoreApi.Models;
using BookStoreApi.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BookStoreApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BooksController : ControllerBase
{
    private readonly BooksService _booksService;

    public BooksController(BooksService booksService) => _booksService = booksService;

    [HttpGet]
    public async Task<List<Book>> Get() => await _booksService.GetAsync();

    [HttpGet("{id:length(24)}")]
    public async Task<ActionResult<Book>> Get(string id)
    {
        var book = await _booksService.GetAsync(id);

        if (book is null)
        {
            return NotFound();
        }

        return book;
    }

    [HttpPost]
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

    [HttpPut("{id:length(24)}")]
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

    [HttpDelete("{id:length(24)}")]
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
