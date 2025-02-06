using CaseDiary.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace CaseDiary.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SectionsController : ControllerBase
    {
        public readonly CaseDiaryContext _context;
        public SectionsController(CaseDiaryContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IEnumerable<Section> Get()
        {
            return _context.Section.ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<Section> Get(int id)
        {
            var section = _context.Section.Find(id);
            if (section == null)
            {
                return NotFound();
            }
            return Ok(section);
        }
        [HttpPost]
        public IActionResult Post(Section section)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            _context.Section.Add(section);
            _context.SaveChanges();
            return CreatedAtRoute(new { id = section.Id }, section);
        }

        [HttpPut]
        public IActionResult Put(int id, Section section)
        {
            var existingSection = _context.Section.Find(id);
            if (existingSection == null)
            {
                return NotFound();
            }
            existingSection.SectionName = section.SectionName;
            existingSection.Description = section.Description;
            _context.SaveChanges();
            return Ok(existingSection);
        }
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var section = _context.Section.Find(id);
            if (section == null)
            {
                return NotFound();
            }

            _context.Section.Remove(section);
            _context.SaveChanges();
            return Ok();
        }
    }
}
