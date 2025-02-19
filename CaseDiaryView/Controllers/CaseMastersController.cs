using System.Net.Http;
using System.Numerics;
using CaseDiaryView.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace CaseDiaryView.Controllers
{
    public class CaseMastersController : Controller
    {
        private readonly HttpClient _httpClient;
        //private readonly string _baseApiUrl = "https://localhost:7175/api/CaseMasters";

        public CaseMastersController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        // GET: CaseMaster
        public async Task<IActionResult> Index()
        {
            List<CaseMaster> caseMasters = new List<CaseMaster>();
            var response = await _httpClient.GetAsync("https://localhost:7175/api/CaseMasters");
            if (!response.IsSuccessStatusCode)
            {
                ViewBag.ErrorMessage = "Failed to retrieve cases from the API.";
                return View(new List<CaseMaster>());
            }

            caseMasters = await response.Content.ReadAsAsync<List<CaseMaster>>();
            return View(caseMasters);
        }
       

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            //List<CaseMaster> casemaster = new List<CaseMaster>();
            ViewBag.BadiId = new SelectList(await GetBadi(), "Id", "BadiName");
            ViewBag.ComplainantId = new SelectList(await GetComplainant(), "Id", "ComplainantName");
            ViewBag.CaseSourceId = new SelectList(await GetCaseSource(), "Id", "Name");
            ViewBag.CourtId = new SelectList(await GetCourt(), "Id", "CourtName");
            ViewBag.AdalotId = new SelectList(await GetAdalot(), "Id", "AdalotName");
            
            return View(new CaseMaster());
        }
        public async Task<List<Badi>> GetBadi()
        {
            List<Badi> badi = new List<Badi>();
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync("https://localhost:7175/api/Badis");
                if (response.IsSuccessStatusCode)
                {
                    badi = response.Content.ReadAsAsync<List<Badi>>().Result;
                }

            }

            return badi;
        }
        public async Task<List<Complainant>> GetComplainant()
        {
            List<Complainant> complainant = new List<Complainant>();
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync("https://localhost:7175/api/Complainants");
                if (response.IsSuccessStatusCode)
                {
                    complainant = response.Content.ReadAsAsync<List<Complainant>>().Result;
                }

            }

            return complainant;
        }
        public async Task<List<CaseSource>> GetCaseSource()
        {
            List<CaseSource> caseSources = new List<CaseSource>();
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync("https://localhost:7175/api/CaseSources");
                if (response.IsSuccessStatusCode)
                {
                    caseSources = response.Content.ReadAsAsync<List<CaseSource>>().Result;
                }

            }

            return caseSources;
        }

        public async Task<List<Court>> GetCourt()
        {
            List<Court> court = new List<Court>();
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync("https://localhost:7175/api/Courts");
                if (response.IsSuccessStatusCode)
                {
                    court = response.Content.ReadAsAsync<List<Court>>().Result;
                }

            }

            return court;
        }
        public async Task<List<Adalot>> GetAdalot()
        {
            List<Adalot> court = new List<Adalot>();
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync("https://localhost:7175/api/Adalots");
                if (response.IsSuccessStatusCode)
                {
                    court = response.Content.ReadAsAsync<List<Adalot>>().Result;
                }

            }

            return court;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CaseMaster caseMaster)
        {
            if (!ModelState.IsValid)
            {
                return View(caseMaster);
            }

            try
            {
                using (var client = new HttpClient())
                {
                    var response = await client.PostAsJsonAsync("https://localhost:7175/api/CaseMasters", caseMaster);
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

            return View(caseMaster);
        }

    }
}
