using CaseDiaryView.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CaseDiaryView.Controllers
{
    public class SectionsController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _BaseapiUrl = "https://localhost:7175/api/Sections";

        public async Task<IActionResult> Index()
        {
            List<Section> sections = new List<Section>();
            using (var client = new HttpClient())
            {
                var res = await client.GetAsync(_BaseapiUrl);
                if (res.IsSuccessStatusCode)
                {
                    sections = res.Content.ReadAsAsync<List<Section>>().Result;
                    return View(sections);
                }
            }
            return View(Enumerable.Empty<Section>());

        }


        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Section section)
        {
            if (!ModelState.IsValid)
            {
                return View(section);
            }

            try
            {
                using (var client = new HttpClient())
                {
                    var response = await client.PostAsJsonAsync("https://localhost:7175/api/Sections", section);
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

            return View(section);
        }




    }
}
