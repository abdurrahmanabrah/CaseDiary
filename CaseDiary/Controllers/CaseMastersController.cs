﻿using CaseDiary.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CaseDiary.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CaseMastersController : ControllerBase
    {
        public readonly CaseDiaryContext _context;
        public CaseMastersController(CaseDiaryContext context)
        {
            _context = context;
        }
        // GET: api/CaseMasters
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CaseMaster>>> GetCaseMaster()
        {
            return await _context.CaseMaster
                .Include(d => d.Adalot)
                .Include(d => d.Section)
                .Include(d=>d.Badi)
                .Include(d=>d.Complainant)
                .Include(d => d.CaseSource)
                .Include(d => d.Court)
                .Include(d=>d.CaseDetails)
                .ToListAsync();
        }

        // GET: api/CaseMasters/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CaseMaster>> GetCaseMaster(int id)
        {
            var caseMaster = await _context.CaseMaster
                .Include(d => d.Adalot)
                .Include(d => d.Section)
                .Include(d => d.Badi)
                .Include(d => d.Complainant)
                .Include(d => d.CaseSource)
                .Include(d => d.Court)
                .Include(d => d.CaseDetails)
                .FirstOrDefaultAsync(d => d.Id == id);

            if (caseMaster == null)
            {
                return NotFound();
            }

            return caseMaster;
        }

        // PUT: api/CaseMasters/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCaseMaster(int id, CaseMaster caseMaster)
        {
            if (id != caseMaster.Id)
            {
                return BadRequest();
            }

            _context.Entry(caseMaster).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DoctorExists(id))
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

        // POST: api/CaseMasters
        [HttpPost]
        public async Task<ActionResult<CaseMaster>> CreateCaseMaster(CaseMaster caseMaster)
        {
            _context.CaseMaster.Add(caseMaster);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCaseMaster", new { id = caseMaster.Id }, caseMaster);
        }

        // DELETE: api/CaseMasters/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCaseMaster(int id)
        {
            var caseMaster = await _context.CaseMaster.FindAsync(id);
            if (caseMaster == null)
            {
                return NotFound();
            }

            _context.CaseMaster.Remove(caseMaster);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DoctorExists(int id)
        {
            return _context.CaseMaster.Any(e => e.Id == id);
        }
    }
}
