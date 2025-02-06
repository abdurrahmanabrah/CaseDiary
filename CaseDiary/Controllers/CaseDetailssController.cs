using CaseDiary.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CaseDiary.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CaseDetailssController : ControllerBase
    {
        public readonly CaseDiaryContext _context;
              

        public CaseDetailssController(CaseDiaryContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IEnumerable<CaseDetails> Get()
        {
            return _context.CaseDetails.ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<CaseDetails> Get(int id)
        {
            var caseDetails = _context.CaseDetails.Find(id);
            if (caseDetails == null)
            {
                return NotFound();
            }
            return Ok(caseDetails);
        }
        [HttpPost]
        public IActionResult Post(CaseDetails caseDetails)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            _context.CaseDetails.Add(caseDetails);
            _context.SaveChanges();
            return CreatedAtRoute(new { id = caseDetails.Id }, caseDetails);
        }



        [HttpPut]
        public IActionResult Put(int id, CaseDetails caseDetails)
        {
            var existingCaseDetails = _context.CaseDetails.Find(id);
            if (existingCaseDetails == null)
            {
                return NotFound();
            }
            existingCaseDetails.Id = caseDetails.Id;
            existingCaseDetails.Id = caseDetails.Id;
            _context.SaveChanges();
            return Ok(existingCaseDetails);
        }
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var caseDetails = _context.CaseDetails.Find(id);
            if (caseDetails == null)
            {
                return NotFound();
            }

            _context.CaseDetails.Remove(caseDetails);
            _context.SaveChanges();
            return Ok();
        }



    }
}
