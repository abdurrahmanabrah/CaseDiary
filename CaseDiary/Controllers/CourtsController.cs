using CaseDiary.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CaseDiary.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourtsController : ControllerBase
    {

        public readonly CaseDiaryContext _context;


        public CourtsController(CaseDiaryContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IEnumerable<Court> Get()
        {
            return _context.Court.ToList();
        }


        [HttpGet("{id}")]
        public ActionResult<Court> Get(int id)
        {
            var court = _context.Court.Find(id);
            if (court == null)
            {
                return NotFound();
            }
            return Ok(court);
        }
        [HttpPost]
        public IActionResult Post(Court court)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            _context.Court.Add(court);
            _context.SaveChanges();
            return CreatedAtRoute(new { id = court.CourtId }, court);
        }



        [HttpPut]
        public IActionResult Put(int id, Court court)
        {
            var existingCourt = _context.Court.Find(id);
            if (existingCourt == null)
            {
                return NotFound();
            }
            existingCourt.CourtName = court.CourtName;
            
            _context.SaveChanges();
            return Ok(existingCourt);
        }
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var court = _context.Court.Find(id);
            if (court == null)
            {
                return NotFound();
            }

            _context.Court.Remove(court);
            _context.SaveChanges();
            return Ok();
        }


    }
}
