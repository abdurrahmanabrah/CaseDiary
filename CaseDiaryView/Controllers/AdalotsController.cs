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
        
        public IActionResult Index()
        {
            return View();
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

            if (entity.ILogoFile != null)
            {
                var imageContent = new StreamContent(entity.ILogoFile.OpenReadStream());
                imageContent.Headers.ContentType = new MediaTypeHeaderValue(entity.ILogoFile.ContentType);
                content.Add(imageContent, "LOGOFile", entity.ILogoFile.FileName);
            }
            if (entity.IBannerFile != null)
            {
                var imageContent = new StreamContent(entity.IBannerFile.OpenReadStream());
                imageContent.Headers.ContentType = new MediaTypeHeaderValue(entity.IBannerFile.ContentType);
                content.Add(imageContent, "Banner", entity.IBannerFile.FileName);
            }
            var response = await _httpClient.PostAsync(_baseApiUrl, content);
            //response.EnsureSuccessStatusCode();
            //var output= await response.Content.ReadFromJsonAsync<Adalot>();
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("index");
            }
            return View();
        }
    }
}
