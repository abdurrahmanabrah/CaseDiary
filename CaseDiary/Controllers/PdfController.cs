using CaseDiary.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CaseDiary.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PdfController : ControllerBase
    {
        private readonly PdfService _pdfService;

        public PdfController(PdfService pdfService)
        {
            _pdfService = pdfService;
        }

        [HttpPost]
        [Route("generate")]
        public IActionResult GeneratePdf([FromBody] PdfRequest request)
        {
            if (request == null || string.IsNullOrWhiteSpace(request.HtmlContent))
            {
                return BadRequest("Invalid request.");
            }

            var pdfBytes = _pdfService.GeneratePdf(request.HtmlContent);

            return File(pdfBytes, "application/pdf", "generated.pdf");
        }
    }

    public class PdfRequest
    {
        public string HtmlContent { get; set; }
    }
}

