using CaseDiary.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace CaseDiary.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdalotsController : ControllerBase
    {
        public readonly CaseDiaryContext _context;
        private object _hostEnvironment;

        public object GetHostEnvironment { get; private set; }

        public AdalotsController(CaseDiaryContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IEnumerable<Adalot> Get()
        {
            return _context.Adalot.ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<Adalot> Get(int id)
        {
            var adalot = _context.Adalot.Find(id);
            if (adalot == null)
            {
                return NotFound();
            }
            return Ok(adalot);
        }
        [HttpPost]
        public IActionResult Post(Adalot adalot)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            _context.Adalot.Add(adalot);
            _context.SaveChanges();
            return CreatedAtRoute(new { id = adalot.Id }, adalot);
        }



        [HttpPut]
        public IActionResult Put(int id, Adalot adalot)
        {
            var existingAdalot = _context.Adalot.Find(id);
            if (existingAdalot == null)
            {
                return NotFound();
            }
            existingAdalot.AdalotName = adalot.AdalotName;
            existingAdalot.Description = adalot.Description;
            _context.SaveChanges();
            return Ok(existingAdalot);
        }
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var adalot = _context.Adalot.Find(id);
            if (adalot == null)
            {
                return NotFound();
            }

            _context.Adalot.Remove(adalot);
            _context.SaveChanges();
            return Ok();
        }



    }
}
