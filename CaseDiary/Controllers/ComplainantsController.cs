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


        IWebHostEnvironment _environment;


        ////private object _hostEnvironment;

        //public object GetHostEnvironment { get; private set; }

        public ComplainantsController(CaseDiaryContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
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
        public IActionResult Post()
        {
            var name = HttpContext.Request.Form["ComplainantName"];
            var location = HttpContext.Request.Form["Location"];
            var dob = HttpContext.Request.Form["DOB"];
            var number = HttpContext.Request.Form["phoneNumber"];
            var email = HttpContext.Request.Form["EmailAddress"];
            var nationality = HttpContext.Request.Form["Nationality"];
            Complainant complainant = new Complainant();

            var r = HttpContext.Request.Form.Files[0];
            if (r != null)
            {
                string ext = Path.GetExtension(r.FileName).ToLower();
                if (ext == ".jpg" || ext == ".jpeg" || ext == ".png" || ext == ".gif")
                {
                    string filrtoFOlder = Path.Combine(_environment.WebRootPath, "Pictures");
                    //string filrtoFOlder = Path.Combine(hostEnvironment.WebRootPath, "Pictures");
                    string filetoSave = Path.Combine(filrtoFOlder, name + ext);
                    using (FileStream fs = new FileStream(filetoSave, FileMode.Create))
                    {
                        r.CopyTo(fs);
                    }
                    complainant.ImageUrl = "Pictures/" + name + ext;


                    try
                    {
                        complainant.ComplainantName = name;
                        complainant.Location = location;
                        complainant.DOB = Convert.ToDateTime(dob);
                        complainant.phoneNumber = number;
                        complainant.EmailAddress = email;
                        complainant.Nationality = nationality;
                        _context.Complainant.Add(complainant);
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


        [HttpPut("{id}")]
        public IActionResult Put(int id)
        {
            var existingComplainant = _context.Complainant.Find(id);
            if (existingComplainant == null)
            {
                return NotFound();
            }

            var name = HttpContext.Request.Form["ComplainantName"];
            var location = HttpContext.Request.Form["Location"];
            var dob = HttpContext.Request.Form["DOB"];
            var number = HttpContext.Request.Form["phoneNumber"];
            var email = HttpContext.Request.Form["EmailAddress"];
            var nationality = HttpContext.Request.Form["Nationality"];

            existingComplainant.ComplainantName = name;
            existingComplainant.Location = location;
            existingComplainant.DOB = Convert.ToDateTime(dob);
            existingComplainant.phoneNumber = number;
            existingComplainant.EmailAddress = email;
            existingComplainant.Nationality = nationality;

            // Update Image if a new file is uploaded
            if (HttpContext.Request.Form.Files.Count > 0)
            {
                var imageFile = HttpContext.Request.Form.Files.FirstOrDefault();
                if (imageFile != null)
                {
                    string ext = Path.GetExtension(imageFile.FileName).ToLower();
                    if (ext == ".jpg" || ext == ".jpeg" || ext == ".png")
                    {
                        string filePath = Path.Combine(_environment.WebRootPath, "Pictures", name + ext);
                        using (FileStream fs = new FileStream(filePath, FileMode.Create))
                        {
                            imageFile.CopyTo(fs);
                        }
                        existingComplainant.ImageUrl = "Pictures/" + name + ext;
                    }
                    else
                    {
                        return BadRequest("Invalid logo file format.");
                    }
                }

            }

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
