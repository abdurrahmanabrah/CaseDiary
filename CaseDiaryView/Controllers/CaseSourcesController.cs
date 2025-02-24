using CaseDiaryView.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

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
            try
            {
                var caseSources = await _httpClient.GetFromJsonAsync<List<CaseSource>>(_baseApiUrl);
                return View(caseSources ?? new List<CaseSource>());
            }
            catch
            {
                return View(Enumerable.Empty<CaseSource>());
            }
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
                return View(caseSource);

            try
            {
                var response = await _httpClient.PostAsJsonAsync(_baseApiUrl, caseSource);
                if (response.IsSuccessStatusCode)
                    return RedirectToAction(nameof(Index));

                ViewBag.ErrorMessage = "Failed to create case source.";
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = $"An error occurred: {ex.Message}";
            }

            return View(caseSource);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var caseSource = await _httpClient.GetFromJsonAsync<CaseSource>($"{_baseApiUrl}/{id}");
                if (caseSource == null)
                {
                    ViewBag.ErrorMessage = "Case source not found.";
                    return View();
                }
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
        public async Task<IActionResult> Edit(CaseSource caseSource)
        {
            if (!ModelState.IsValid)
                return View(caseSource);

            try
            {
                var response = await _httpClient.PutAsJsonAsync($"{_baseApiUrl}", caseSource);
                if (response.IsSuccessStatusCode)
                    return RedirectToAction(nameof(Index));

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
