using CaseDiaryView.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text.Json;

namespace CaseDiaryView.Controllers
{
    public class CaseSourcesController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseApiUrl = "https://localhost:7175/api/CaseSources";

        public CaseSourcesController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IActionResult> Index()
        {
            List<CaseSource> caseSource = new List<CaseSource>();
            using (var client = new HttpClient())
            {
                var res = await client.GetAsync(_baseApiUrl);
                if (res.IsSuccessStatusCode)
                {
                    caseSource = res.Content.ReadAsAsync<List<CaseSource>>().Result;
                    return View(caseSource);
                }
            }
            return View(Enumerable.Empty<CaseSource>());
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CaseSource caseSource)
        {
            if (!ModelState.IsValid)
            {
                return View(caseSource);
            }

            try
            {
                var response = await _httpClient.PostAsJsonAsync(_baseApiUrl, caseSource);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ViewBag.ErrorMessage = "Failed to create a new consultation.";
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = $"An error occurred: {ex.Message}";
            }

            return View(caseSource);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int ID)
        {
            try
            {
                var res = await _httpClient.GetAsync($"{_baseApiUrl}/{ID}");

                if (!res.IsSuccessStatusCode)
                {
                    ViewBag.ErrorMessage = "Failed to fetch the case source details.";
                    return View();
                }

                var content = await res.Content.ReadAsStringAsync();
                var caseSource = JsonSerializer.Deserialize<CaseSource>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                return View(caseSource);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = $"An error occurred: {ex.Message}";
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CaseSource caseSource)
        {
            if (!ModelState.IsValid)
            {
                return View(caseSource);
            }

            try
            {
                var response = await _httpClient.PutAsJsonAsync($"{_baseApiUrl}/{id}", caseSource);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }

                ViewBag.ErrorMessage = "Failed to update the case source.";
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = $"An error occurred: {ex.Message}";
            }

            return View(caseSource);
        }

    }
}
