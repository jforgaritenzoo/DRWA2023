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
    [Route("[controller]")]
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
        [HttpGet("{nip}")]
        public async Task<ActionResult<GuruItem>> GetGuruItem(string nip)
        {
            if (_context.GuruItem == null)
            {
                return NotFound();
            }
            //var guruItem = await _context.GuruItem.FindAsync(nip);
            var guruItem =  _context.GuruItem.Where(j => j.Nip == nip).FirstOrDefault();

            if (guruItem == null)
            {
                return NotFound();
            }

            return guruItem;
        }

        // PUT: api/Guru/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754

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

        private bool GuruItemExists(long id)
        {
            return (_context.GuruItem?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
