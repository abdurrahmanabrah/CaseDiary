using CaseDiaryView.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CaseDiaryView.Controllers
{
    public class CaseDetailssController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseApiUrl = "https://localhost:7175/api/CaseDetailss";

        public CaseDetailssController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IActionResult> Index()
        {
            List<CaseDetails> caseDetails = new List<CaseDetails>();
            using (var client = new HttpClient())
            {
                var res = await client.GetAsync(_baseApiUrl);
                if (res.IsSuccessStatusCode)
                {
                    caseDetails = res.Content.ReadAsAsync<List<CaseDetails>>().Result;
                    return View(caseDetails);
                }
            }
            return View(Enumerable.Empty<CaseDetails>());
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CaseDetails caseDetails)
        {
            if (!ModelState.IsValid)
            {
                return View(caseDetails);
            }

            try
            {
                var response = await _httpClient.PostAsJsonAsync(_baseApiUrl, caseDetails);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ViewBag.ErrorMessage = "Failed to create a new CaseDetails.";
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = $"An error occurred: {ex.Message}";
            }

            return View(caseDetails);
        }
    }
}
