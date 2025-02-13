using CaseDiaryView.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;

namespace CaseDiaryView.Controllers
{
    public class BadisController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseApiUrl = "https://localhost:7175/api/Badis";

        public BadisController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IActionResult> Index()
        {
            List<Badi> badi = new List<Badi>();
            using (var client = new HttpClient())
            {
                var res = await client.GetAsync(_baseApiUrl);
                if (res.IsSuccessStatusCode)
                {
                    badi = res.Content.ReadAsAsync<List<Badi>>().Result;
                    return View(badi);
                }
            }
            return View(Enumerable.Empty<Badi>());
        }

        public IActionResult Create()
        {
            return View(new Badi());
        }
        [HttpPost]
        public async Task<IActionResult> Create(Badi entity)
        {
            var content = new MultipartFormDataContent();

            content.Add(new StringContent(entity.BadiName), "AdalotName");
            content.Add(new StringContent(entity.Location ), "Location");
            content.Add(new StringContent(entity.DOB.ToString("yyyy-MM-dd")), "Location");
            content.Add(new StringContent(entity.phoneNumber), "phoneNumber");
            content.Add(new StringContent(entity.EmailAddress), "EmailAddress");
            content.Add(new StringContent(entity.Nationality), "Nationality");
            // Convert DateTime fields safely
            content.Add(new StringContent(entity.CrimeDate.ToString("yyyy-MM-dd")), "CrimeDate");
            content.Add(new StringContent(entity.ConvictionDate.ToString("yyyy-MM-dd")), "ConvictionDate");
            content.Add(new StringContent(entity.Description), "Description");
            //content.Add(new StringContent(entity.ConvictionDate), "ConvictionDate");
            content.Add(new StringContent(entity.Status), "Status");


            if (entity.ImageFile != null)
            {
                var logoContent = new StreamContent(entity.ImageFile.OpenReadStream());
                logoContent.Headers.ContentType = new MediaTypeHeaderValue(entity.ImageFile.ContentType);
                content.Add(logoContent, "LOGOFile", entity.ImageFile.FileName);
            }

            
            var response = await _httpClient.PostAsync(_baseApiUrl, content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Failed to create Adalot.");
            return View(entity);
        }
    }
}
