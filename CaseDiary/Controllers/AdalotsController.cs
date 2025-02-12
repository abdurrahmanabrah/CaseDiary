using CaseDiary.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System;

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
            
            Adalot adalot = new Adalot();

            var r = HttpContext.Request.Form.Files[0];
            if (r != null)
            {
                string ext = Path.GetExtension(r.FileName).ToLower();
                if (ext == ".jpg" || ext == ".jpeg" || ext == ".png")
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
                    if (bext == ".jpg" || bext == ".jpeg" || bext == ".png")
                    {
                        string filrtoFOlder = Path.Combine(_environment.WebRootPath, "Pictures");
                        //string filrtoFOlder = Path.Combine(hostEnvironment.WebRootPath, "Pictures");
                        string filetoSave = Path.Combine(filrtoFOlder, name + "banar" + bext);
                        using (FileStream fs = new FileStream(filetoSave, FileMode.Create))
                        {
                            b.CopyTo(fs);
                        }
                        adalot.Banner = "Pictures/" + name + "banner" + bext;

                        try
                        {
                            adalot.AdalotName = name;
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
