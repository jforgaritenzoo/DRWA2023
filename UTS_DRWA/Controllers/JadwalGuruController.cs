using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JadwalGuru.Models;

namespace UTS_DRWA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JadwalGuruController : ControllerBase
    {
        private readonly JadwalGuruContext _context;

        public JadwalGuruController(JadwalGuruContext context)
        {
            _context = context;
        }

        // GET: api/JadwalGuru
        [HttpGet]
        public async Task<ActionResult<IEnumerable<JadwalGuruItem>>> GetJadwalGuruItem()
        {
            if (_context.JadwalGuruItem == null)
            {
                return NotFound();
            }
            return await _context.JadwalGuruItem.ToListAsync();
        }

        // GET: api/JadwalGuru/5
        [HttpGet("{id}")]
        public async Task<ActionResult<JadwalGuruItem>> GetJadwalGuruItem(long id)
        {
            if (_context.JadwalGuruItem == null)
            {
                return NotFound();
            }
            var jadwalGuruItem = await _context.JadwalGuruItem.FindAsync(id);

            if (jadwalGuruItem == null)
            {
                return NotFound();
            }

            return jadwalGuruItem;
        }

        [HttpGet("{nip}")]
        public async Task<ActionResult<JadwalGuruItem>> GetJadwalGuruItembyNip(string nip)
        {
            if (_context.JadwalGuruItem == null)
            {
                return NotFound();
            }
            //var jadwalGuruItem = await _context.JadwalGuruItem.FindAsync(nip);
            var jadwalGuruItem = _context.JadwalGuruItem.Where(j => j.Nip == nip).FirstOrDefault();

            if (jadwalGuruItem == null)
            {
                return NotFound();
            }

            return jadwalGuruItem;
        }

        [HttpGet("{id_mapel}")]
        public async Task<ActionResult<JadwalGuruItem>> GetJadwalGuruItembyIdMapel(string id_mapel)
        {
            if (_context.JadwalGuruItem == null)
            {
                return NotFound();
            }
            // var jadwalGuruItem = await _context.JadwalGuruItem.FindAsync(id_mapel);
            var jadwalGuruItem = _context.JadwalGuruItem.Where(j => j.Id_mapel == id_mapel).FirstOrDefault();


            if (jadwalGuruItem == null)
            {
                return NotFound();
            }

            return jadwalGuruItem;
        }

        // PUT: api/JadwalGuru/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutJadwalGuruItem(long id, JadwalGuruItem jadwalGuruItem)
        {
            if (id != jadwalGuruItem.Id)
            {
                return BadRequest();
            }

            _context.Entry(jadwalGuruItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!JadwalGuruItemExists(id))
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

        // POST: api/JadwalGuru
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<JadwalGuruItem>> PostJadwalGuruItem(
            JadwalGuruItem jadwalGuruItem
        )
        {
            if (_context.JadwalGuruItem == null)
            {
                return Problem("Entity set 'JadwalGuruContext.JadwalGuruItem'  is null.");
            }
            _context.JadwalGuruItem.Add(jadwalGuruItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetJadwalGuruItem),
                new { id = jadwalGuruItem.Id },
                jadwalGuruItem
            );
        }

        // DELETE: api/JadwalGuru/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJadwalGuruItem(long id)
        {
            if (_context.JadwalGuruItem == null)
            {
                return NotFound();
            }
            var jadwalGuruItem = await _context.JadwalGuruItem.FindAsync(id);
            if (jadwalGuruItem == null)
            {
                return NotFound();
            }

            _context.JadwalGuruItem.Remove(jadwalGuruItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool JadwalGuruItemExists(long id)
        {
            return (_context.JadwalGuruItem?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
