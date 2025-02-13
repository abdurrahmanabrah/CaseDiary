using CaseDiary.Model;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CaseDiary.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BadisController : ControllerBase
    {
        public readonly CaseDiaryContext _context;

        IWebHostEnvironment _environment;


        ////private object _hostEnvironment;

        //public object GetHostEnvironment { get; private set; }

        public BadisController(CaseDiaryContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
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
        public IActionResult Post()
        {
            var name = HttpContext.Request.Form["BadiName"];
            var location = HttpContext.Request.Form["Location"];
            var dob = HttpContext.Request.Form["DOB"];
            var number = HttpContext.Request.Form["phoneNumber"];
            var email = HttpContext.Request.Form["EmailAddress"];
            var nationality = HttpContext.Request.Form["Nationality"];
            var description = HttpContext.Request.Form["Description"];
            var crimeDate = HttpContext.Request.Form["CrimeDate"];
            var convictionDate = HttpContext.Request.Form["ConvictionDate"];
            var status = HttpContext.Request.Form["Status"];
            Badi badi = new Badi();

            var r = HttpContext.Request.Form.Files[0];
            if (r != null)
            {
                string ext = Path.GetExtension(r.FileName).ToLower();
                if (ext == ".jpg" || ext == ".jpeg" || ext == ".png" || ext == ".gif")
                {
                    string filrtoFOlder = Path.Combine(_environment.WebRootPath, "Pictures");
                    //string filrtoFOlder = Path.Combine(hostEnvironment.WebRootPath, "Pictures");
                    string filetoSave = Path.Combine(filrtoFOlder, name  + ext);
                    using (FileStream fs = new FileStream(filetoSave, FileMode.Create))
                    {
                        r.CopyTo(fs);
                    }
                    badi.ImageUrl = "Pictures/" + name  + ext;
                

                        try
                        {
                        badi.BadiName = name;
                        badi.Location = location;
                        badi.DOB = Convert.ToDateTime(dob);
                        badi.phoneNumber = number;
                        badi.EmailAddress = email;
                        badi.Nationality = nationality;
                        badi.Description = description;
                        badi.CrimeDate = Convert.ToDateTime(crimeDate);
                        badi.ConvictionDate = Convert.ToDateTime(convictionDate);
                        badi.Status = status;
                            _context.Badi.Add(badi);
                            if (_context.SaveChanges() > 0)
                            {
                                return Created();
                            }
                        }
                        catch (Exception ex)
                        {
                            return Problem(ex.Message);
                        }
                    }
                    else
                    {
                        return BadRequest("Please provide valid Logo image");
                    }
                
            }
            return Problem("Image not found");
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
