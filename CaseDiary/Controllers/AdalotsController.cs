using CaseDiary.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System;
using static System.Net.Mime.MediaTypeNames;

namespace CaseDiary.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdalotsController : ControllerBase
    {
        public readonly CaseDiaryContext _context;
        IWebHostEnvironment _environment;
        

        ////private object _hostEnvironment;

        //public object GetHostEnvironment { get; private set; }

        public AdalotsController(CaseDiaryContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
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
        public IActionResult Post()
        {
            var name = HttpContext.Request.Form["AdalotName"];
            var description = HttpContext.Request.Form["Description"];
            var location = HttpContext.Request.Form["Location"];
            Adalot adalot = new Adalot();

            var r = HttpContext.Request.Form.Files[0];
            if (r != null)
            {
                string ext = Path.GetExtension(r.FileName).ToLower();
                if (ext == ".jpg" || ext == ".jpeg" || ext == ".png" || ext == ".gif")
                {
                    string filrtoFOlder = Path.Combine(_environment.WebRootPath, "Pictures");
                    //string filrtoFOlder = Path.Combine(hostEnvironment.WebRootPath, "Pictures");
                    string filetoSave = Path.Combine(filrtoFOlder, name + "logo" + ext);
                    using (FileStream fs = new FileStream(filetoSave, FileMode.Create))
                    {
                        r.CopyTo(fs);
                    }
                    adalot.Logo = "Pictures/" + name + "logo" + ext;
                }
                var b = HttpContext.Request.Form.Files[1];
                if (b != null)
                {
                    string bext = Path.GetExtension(b.FileName).ToLower();
                    if (bext == ".jpg" || bext == ".jpeg" || bext == ".png" || bext == ".gif")
                    {
                        string filrtoFOlder = Path.Combine(_environment.WebRootPath, "Pictures");
                        //string filrtoFOlder = Path.Combine(hostEnvironment.WebRootPath, "Pictures");
                        string filetoSave = Path.Combine(filrtoFOlder, name + "banner" + bext);
                        using (FileStream fs = new FileStream(filetoSave, FileMode.Create))
                        {
                                    b.CopyTo(fs);
                        }
                        adalot.Banner = "Pictures/" + name + "banner" + bext;

                        try
                        {
                            adalot.AdalotName = name;
                            adalot.Description = description;
                            adalot.Location = location;
                            _context.Adalot.Add(adalot);
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
            }
            return Problem("Image not found");
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id)
        {
            var existingAdalot = _context.Adalot.Find(id);
            if (existingAdalot == null)
            {
                return NotFound();
            }

            var name = HttpContext.Request.Form["AdalotName"];
            var description = HttpContext.Request.Form["Description"];
            var location = HttpContext.Request.Form["Location"];

            existingAdalot.AdalotName = name;
            existingAdalot.Description = description;
            existingAdalot.Location = location;

            // Update logo if a new file is uploaded
            if (HttpContext.Request.Form.Files.Count > 0)
            {
                var logoFile = HttpContext.Request.Form.Files.FirstOrDefault();
                if (logoFile != null)
                {
                    string ext = Path.GetExtension(logoFile.FileName).ToLower();
                    if (ext == ".jpg" || ext == ".jpeg" || ext == ".png" || ext == ".gif")
                    {
                        string filePath = Path.Combine(_environment.WebRootPath, "Pictures", name + "logo" + ext);
                        using (FileStream fs = new FileStream(filePath, FileMode.Create))
                        {
                            logoFile.CopyTo(fs);
                        }
                        existingAdalot.Logo = "Pictures/" + name + "logo" + ext;
                    }
                    else
                    {
                        return BadRequest("Invalid logo file format.");
                    }
                }

                // Update banner if a new file is uploaded
                var bannerFile = HttpContext.Request.Form.Files.Skip(1).FirstOrDefault();
                if (bannerFile != null)
                {
                    string bext = Path.GetExtension(bannerFile.FileName).ToLower();
                    if (bext == ".jpg" || bext == ".jpeg" || bext == ".png" || bext == ".gif")
                    {
                        string filePath = Path.Combine(_environment.WebRootPath, "Pictures", name + "banner" + bext);
                        using (FileStream fs = new FileStream(filePath, FileMode.Create))
                        {
                            bannerFile.CopyTo(fs);
                        }
                        existingAdalot.Banner = "Pictures/" + name + "banner" + bext;
                    }
                    else
                    {
                        return BadRequest("Invalid banner file format.");
                    }
                }
            }

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
