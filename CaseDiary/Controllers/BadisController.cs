using CaseDiary.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CaseDiary.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BadisController : ControllerBase
    {
        public readonly CaseDiaryContext _context;


        public BadisController(CaseDiaryContext context)
        {
            _context = context;
        }

        // GET: api/badis
        [HttpGet]
        public ActionResult<IEnumerable<Badi>> Get()
        {
            return _context.Badi.ToList();
        }

        // GET: api/badis/{id}
        [HttpGet("{id}")]
        public ActionResult<Badi> Get(int id)
        {
            var badi = _context.Badi.Find(id);
            if (badi == null)
            {
                return NotFound();
            }
            return Ok(badi);
        }

        // POST: api/badis
        [HttpPost]
        public IActionResult Post([FromBody] Badi badi)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Badi.Add(badi);
            _context.SaveChanges();

            return CreatedAtAction(nameof(Get), new { id = badi.Id }, badi);
        }

        // PUT: api/badis/{id}
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Badi badi)
        {
            var existingBadi = _context.Badi.Find(id);
            if (existingBadi == null)
            {
                return NotFound();
            }

            existingBadi.BadiName = badi.BadiName;
            existingBadi.phoneNumber = badi.phoneNumber;

            _context.SaveChanges();
            return Ok(existingBadi);
        }

        // DELETE: api/badis/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var badi = _context.Badi.Find(id);
            if (badi == null)
            {
                return NotFound();
            }

            _context.Badi.Remove(badi);
            _context.SaveChanges();
            return NoContent();
        }

    }
}
