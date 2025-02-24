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
                using (var client = new HttpClient())
                {
                    var response = await client.PostAsJsonAsync("https://localhost:7175/api/Courts", court);
                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        // Handle API error
                        ViewBag.ErrorMessage = "Failed to create a new degree.";
                    }

                }
                //if (response.IsSuccessStatusCode)
                //{
                //    return RedirectToAction(nameof(Index));
                //}

            }
            catch (Exception ex)
            {
                // Log the exception
                ViewBag.ErrorMessage = $"An error occurred: {ex.Message}";
            }

            return View(court);
        }









        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            try
            {
                Court court = new Court();
                //degree.Fi(Id == id);

                using (var client = new HttpClient())
                {

                    var response = await client.GetAsync("https://localhost:7175/api/Courts" + id);
                    if (response.IsSuccessStatusCode)
                    {
                        // return RedirectToAction(nameof(Index));
                        court = response.Content.ReadAsAsync<Court>().Result;
                        return View(court);
                    }
                    else
                    {
                        // Handle API error
                        //ViewBag.ErrorMessage = "Failed to create a new degree.";
                        ViewBag.ErrorMessage = "Failed to fetch the degree details.";
                    }

                }
                //ViewBag.ErrorMessage = "Failed to fetch the degree details.";
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = $"An error occurred: {ex.Message}";
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Court court)
        {
            if (!ModelState.IsValid)
            {
                return View(court);
            }

            try
            {
                using (var client = new HttpClient())
                {
                    var response = await client.PutAsJsonAsync($"https://localhost:7175/api/Courts", court);
                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        // Handle API error
                        //ViewBag.ErrorMessage = "Failed to create a new degree.";
                        ViewBag.ErrorMessage = "Failed to fetch the degree details.";
                    }

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
