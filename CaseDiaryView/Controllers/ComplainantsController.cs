using CaseDiaryView.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text.Json;

namespace CaseDiaryView.Controllers
{
    public class ComplainantsController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseApiUrl = "https://localhost:7175/api/Complainants";

        public ComplainantsController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IActionResult> Index()
        {
            List<Complainant> complainant = new List<Complainant>();
            using (var client = new HttpClient())
            {
                var res = await client.GetAsync(_baseApiUrl);
                if (res.IsSuccessStatusCode)
                {
                    complainant = res.Content.ReadAsAsync<List<Complainant>>().Result;
                    return View(complainant);
                }
            }
            return View(Enumerable.Empty<Badi>());
        }
        public IActionResult Create()
        {
            return View(new Complainant());
        }
        [HttpPost]
        public async Task<IActionResult> Create(Complainant entity)
        {
            var content = new MultipartFormDataContent();

            content.Add(new StringContent(entity.ComplainantName), "ComplainantName");
            content.Add(new StringContent(entity.Location), "Location");
            content.Add(new StringContent(entity.DOB.ToString("yyyy-MM-dd")), "DOB");
            content.Add(new StringContent(entity.phoneNumber), "phoneNumber");
            content.Add(new StringContent(entity.EmailAddress), "EmailAddress");
            content.Add(new StringContent(entity.Nationality), "Nationality");
            // Convert DateTime fields safely
            


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

            ModelState.AddModelError("", "Failed to create Complainant.");
            return View(entity);
        }


        public async Task<IActionResult> Edit(int id)
        {
            var res = await _httpClient.GetAsync($"{_baseApiUrl}/{id}");

            if (!res.IsSuccessStatusCode) return NotFound();

            var content = await res.Content.ReadAsStringAsync();

            try
            {
                var complainant = JsonSerializer.Deserialize<Complainant>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                return View(complainant);
            }
            catch (JsonException)
            {
                return BadRequest("Invalid JSON response from API.");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Complainant entity)
        {
            var content = new MultipartFormDataContent
    {
        { new StringContent(entity.ComplainantName), "ComplainantName" },
        { new StringContent(entity.Location ?? ""), "Location" },
        { new StringContent(entity.DOB.ToString("yyyy-MM-dd")), "DOB" },
        { new StringContent(entity.phoneNumber ?? ""), "phoneNumber" },
        { new StringContent(entity.EmailAddress ?? ""), "EmailAddress" },
        { new StringContent(entity.Nationality ?? ""), "Nationality" },
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
