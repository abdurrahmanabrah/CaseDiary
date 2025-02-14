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

            content.Add(new StringContent(entity.BadiName), "BadiName");
            content.Add(new StringContent(entity.Location ), "Location");
            content.Add(new StringContent(entity.DOB.ToString("yyyy-MM-dd")), "DOB");
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
                content.Add(logoContent, "ImageFile", entity.ImageFile.FileName);
            }

            
            var response = await _httpClient.PostAsync(_baseApiUrl, content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Failed to create BAdi.");
            return View(entity);
        }


        public async Task<IActionResult> Edit(int id)
        {
            var res = await _httpClient.GetAsync($"{_baseApiUrl}/{id}");

            if (!res.IsSuccessStatusCode) return NotFound();

            var content = await res.Content.ReadAsStringAsync();

            try
            {
                var badi = JsonSerializer.Deserialize<Badi>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                return View(badi);
            }
            catch (JsonException)
            {
                return BadRequest("Invalid JSON response from API.");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Badi entity)
        {
            var content = new MultipartFormDataContent
    {
        { new StringContent(entity.BadiName ?? ""), "BadiName" },
        { new StringContent(entity.Location ?? ""), "Location" },
        { new StringContent(entity.DOB.ToString("yyyy-MM-dd")), "DOB" },
        { new StringContent(entity.phoneNumber ?? ""), "phoneNumber" },
        { new StringContent(entity.EmailAddress ?? ""), "EmailAddress" },
        { new StringContent(entity.CrimeDate.ToString("yyyy-MM-dd")), "CrimeDate" },
        { new StringContent(entity.Nationality ?? ""), "Nationality" },
        { new StringContent(entity.ConvictionDate.ToString("yyyy-MM-dd")), "ConvictionDate" },
        { new StringContent(entity.Description ?? ""), "Description" },
        { new StringContent(entity.Status ?? ""), "Status" }
    };

            // Image File Handling (Fixed)
            if (entity.ImageFile != null && entity.ImageFile.Length > 0)
            {
                var stream = new MemoryStream();
                await entity.ImageFile.CopyToAsync(stream);
                stream.Seek(0, SeekOrigin.Begin);

                var imageContent = new StreamContent(stream);
                imageContent.Headers.ContentType = new MediaTypeHeaderValue(entity.ImageFile.ContentType);
                content.Add(imageContent, "PhotoFile", entity.ImageFile.FileName);
            }

            var response = await _httpClient.PutAsync($"{_baseApiUrl}/{id}", content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Failed to update Badi.");
            return View(entity);
        }

    }
}
