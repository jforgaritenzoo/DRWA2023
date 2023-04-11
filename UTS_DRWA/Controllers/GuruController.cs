using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Guru.Models;

namespace UTS_DRWA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GuruController : ControllerBase
    {
        private readonly GuruContext _context;

        public GuruController(GuruContext context)
        {
            _context = context;
        }

        // GET: api/Guru
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GuruItem>>> GetGuruItem()
        {
            if (_context.GuruItem == null)
            {
                return NotFound();
            }
            return await _context.GuruItem.ToListAsync();
        }

        // GET: api/Guru/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GuruItem>> GetGuruItem(long id)
        {
            if (_context.GuruItem == null)
            {
                return NotFound();
            }
            var guruItem = await _context.GuruItem.FindAsync(id);

            if (guruItem == null)
            {
                return NotFound();
            }

            return guruItem;
        }

        // PUT: api/Guru/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGuruItem(long id, GuruItem guruItem)
        {
            if (id != guruItem.Id)
            {
                return BadRequest();
            }

            _context.Entry(guruItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GuruItemExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Guru
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<GuruItem>> PostGuruItem(GuruItem guruItem)
        {
            if (_context.GuruItem == null)
            {
                return Problem("Entity set 'GuruContext.GuruItem'  is null.");
            }
            _context.GuruItem.Add(guruItem);
            await _context.SaveChangesAsync();

            // return CreatedAtAction("GetGuruItem", new { id = guruItem.Id }, guruItem);
            return CreatedAtAction(nameof(GetGuruItem), new { id = guruItem.Id }, guruItem);
        }

        // DELETE: api/Guru/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGuruItem(long id)
        {
            if (_context.GuruItem == null)
            {
                return NotFound();
            }
            var guruItem = await _context.GuruItem.FindAsync(id);
            if (guruItem == null)
            {
                return NotFound();
            }

            _context.GuruItem.Remove(guruItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool GuruItemExists(long id)
        {
            return (_context.GuruItem?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
