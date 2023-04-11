using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mapel.Models;

namespace UTS_DRWA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MapelController : ControllerBase
    {
        private readonly MapelContext _context;

        public MapelController(MapelContext context)
        {
            _context = context;
        }

        // GET: api/Mapel
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MapelItem>>> GetMapelItem()
        {
          if (_context.MapelItem == null)
          {
              return NotFound();
          }
            return await _context.MapelItem.ToListAsync();
        }

        // GET: api/Mapel/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MapelItem>> GetMapelItem(long id)
        {
          if (_context.MapelItem == null)
          {
              return NotFound();
          }
            var mapelItem = await _context.MapelItem.FindAsync(id);

            if (mapelItem == null)
            {
                return NotFound();
            }

            return mapelItem;
        }

        // PUT: api/Mapel/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMapelItem(long id, MapelItem mapelItem)
        {
            if (id != mapelItem.Id)
            {
                return BadRequest();
            }

            _context.Entry(mapelItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MapelItemExists(id))
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

        // POST: api/Mapel
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<MapelItem>> PostMapelItem(MapelItem mapelItem)
        {
          if (_context.MapelItem == null)
          {
              return Problem("Entity set 'MapelContext.MapelItem'  is null.");
          }
            _context.MapelItem.Add(mapelItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetMapelItem), new { id = mapelItem.Id }, mapelItem);
        }

        // DELETE: api/Mapel/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMapelItem(long id)
        {
            if (_context.MapelItem == null)
            {
                return NotFound();
            }
            var mapelItem = await _context.MapelItem.FindAsync(id);
            if (mapelItem == null)
            {
                return NotFound();
            }

            _context.MapelItem.Remove(mapelItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MapelItemExists(long id)
        {
            return (_context.MapelItem?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
