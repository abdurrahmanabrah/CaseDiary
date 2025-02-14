using CaseDiaryView.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CaseDiaryView.Controllers
{
    public class CourtsController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiBaseUrl = "https://localhost:7175/api/Courts"; 

        public async Task<IActionResult> Index()
        {
            List<Court> courts = new List<Court>();
            using (var client = new HttpClient())
            {
                var res = await client.GetAsync(_apiBaseUrl);
                if (res.IsSuccessStatusCode)
                {
                    courts = res.Content.ReadAsAsync<List<Court>>().Result;
                    return View(courts);
                }
            }
            return View(Enumerable.Empty<Court>());

        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Court court)
        {
            if (!ModelState.IsValid)
            {
                return View(court);
            }

            try
            {
                var response = await _httpClient.PostAsJsonAsync(_apiBaseUrl, court);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ViewBag.ErrorMessage = "Failed to create a new Court.";
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = $"An error occurred: {ex.Message}";
            }

            return View(court);
        }
    }
}
