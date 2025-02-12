using CaseDiaryView.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Net.Http.Headers;

namespace CaseDiaryView.Controllers
{
    public class AdalotsController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseApiUrl = "https://localhost:7175/api/Adalots";

        public AdalotsController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        
        public async Task<IActionResult> Index()
        {
            List<Adalot> adalot = new List<Adalot>();
            using (var client = new HttpClient())
            {
                var res = await client.GetAsync(_baseApiUrl);
                if (res.IsSuccessStatusCode)
                {
                    adalot = res.Content.ReadAsAsync<List<Adalot>>().Result;
                    return View(adalot);
                }
            }
            return View(Enumerable.Empty<Adalot>());
        }
        public IActionResult Create()
        {
            return View(new Adalot());
        }
        [HttpPost]
        public async Task<IActionResult> Create(Adalot entity)
        {
            var content = new MultipartFormDataContent();

            content.Add(new StringContent(entity.AdalotName), "AdalotName");
            content.Add(new StringContent(entity.Description ?? ""), "Description");
            content.Add(new StringContent(entity.Location ?? ""), "Location");

            if (entity.ILogoFile != null)
            {
                var logoContent = new StreamContent(entity.ILogoFile.OpenReadStream());
                logoContent.Headers.ContentType = new MediaTypeHeaderValue(entity.ILogoFile.ContentType);
                content.Add(logoContent, "LOGOFile", entity.ILogoFile.FileName);
            }

            if (entity.IBannerFile != null)
            {
                var bannerContent = new StreamContent(entity.IBannerFile.OpenReadStream());
                bannerContent.Headers.ContentType = new MediaTypeHeaderValue(entity.IBannerFile.ContentType);
                content.Add(bannerContent, "Banner", entity.IBannerFile.FileName);
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
