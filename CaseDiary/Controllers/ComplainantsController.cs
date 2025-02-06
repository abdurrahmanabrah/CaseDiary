using CaseDiary.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CaseDiary.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComplainantsController : ControllerBase
    {
        public readonly CaseDiaryContext _context;


        public ComplainantsController(CaseDiaryContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IEnumerable<Complainant> Get()
        {
            return _context.Complainant.ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<Complainant> Get(int id)
        {
            var complainant = _context.Complainant.Find(id);
            if (complainant == null)
            {
                return NotFound();
            }
            return Ok(complainant);
        }
        [HttpPost]
        public IActionResult Post(Complainant complainant)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            _context.Complainant.Add(complainant);
            _context.SaveChanges();
            return CreatedAtRoute(new { id = complainant.Id }, complainant);
        }



        [HttpPut]
        public IActionResult Put(int id, Complainant complainant)
        {
            var existingComplainant = _context.Complainant.Find(id);
            if (existingComplainant == null)
            {
                return NotFound();
            }
            existingComplainant.ComplainantName = complainant.ComplainantName;
            existingComplainant.phoneNumber = complainant.phoneNumber;
            _context.SaveChanges();
            return Ok(existingComplainant);
        }
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var complainant = _context.Complainant.Find(id);
            if (complainant == null)
            {
                return NotFound();
            }

            _context.Complainant.Remove(complainant);
            _context.SaveChanges();
            return Ok();
        }

    }
}
