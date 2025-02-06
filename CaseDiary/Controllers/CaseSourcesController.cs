using CaseDiary.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CaseDiary.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CaseSourcesController : ControllerBase
    {
        public readonly CaseDiaryContext _context;
        public CaseSourcesController(CaseDiaryContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IEnumerable<CaseSource> Get()
        {
            return _context.CaseSource.ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<CaseSource> Get(int id)
        {
            var caseSource = _context.CaseSource.Find(id);
            if (caseSource == null)
            {
                return NotFound();
            }
            return Ok(caseSource);
        }
        [HttpPost]
        public IActionResult Post(CaseSource caseSource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            _context.CaseSource.Add(caseSource);
            _context.SaveChanges();
            return CreatedAtRoute(new { id = caseSource.ID }, caseSource);
        }

        [HttpPut]
        public IActionResult Put(int id, CaseSource caseSource)
        {
            var existingCaseSource = _context.CaseSource.Find(id);
            if (existingCaseSource == null)
            {
                return NotFound();
            }
            existingCaseSource.Name = caseSource.Name;
            //existingCaseSource.Description = CaseSource.Name;
            _context.SaveChanges();
            return Ok(existingCaseSource);
        }
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var caseSource = _context.CaseSource.Find(id);
            if (caseSource == null)
            {
                return NotFound();
            }

            _context.CaseSource.Remove(caseSource);
            _context.SaveChanges();
            return Ok();
        }

    }
}
