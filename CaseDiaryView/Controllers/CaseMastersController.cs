using System.Net.Http;
using System.Numerics;
using CaseDiaryView.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Net.Http.Headers;
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


        //[HttpGet]
        //public async Task<IActionResult> Create()
        //{
        //    ViewBag.ProductId = new SelectList(await GetProduct(), "Id", "Name");
        //    var model = new OrderVM()
        //    {
        //        Details = { new DetailsVM{
        //         OrderId=0,
        //         ProductId=0,
        //             Price=0,
        //        } }
        //    };

        //    return View(model);
        //}
        //[HttpPost]
        //public async Task<IActionResult> Create(OrderVM order, string act = "")
        //{

        //    if (act == "add")
        //    {
        //        order.Details.Add(new DetailsVM());
        //    }
        //    if (act.StartsWith("remove"))
        //    {

        //        int index = int.Parse(act.Substring(act.IndexOf("_") + 1));
        //        order.Details.RemoveAt(index);
        //    }
        //    if (act == "Save")
        //    {
        //        var jsonSerializerSettings = new JsonSerializerSettings
        //        {
        //            StringEscapeHandling = StringEscapeHandling.EscapeNonAscii
        //        };
        //        string orderString = JsonConvert.SerializeObject(order, jsonSerializerSettings);
        //        var content = new MultipartFormDataContent();
        //        // content.Add(new StringContent(orderString, "Orderdata"));
        //        content.Add(new StringContent(orderString), "orderdata");
        //        //content.Add(new StringContent(entity.ContactNumber), "ContactNumber");
        //        if (order.Image != null)
        //        {
        //            var imageContent = new StreamContent(order.Image.OpenReadStream());
        //            imageContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(order.Image.ContentType);
        //            content.Add(imageContent, "LOGOFile", order.Image.FileName);

        //        }
        //        HttpClient client = _factory.CreateClient();
        //        client.BaseAddress =
        //        new Uri("https://localhost:7178/api/");
        //        client.DefaultRequestHeaders.Add(
        //        HeaderNames.UserAgent, "ExchangeRateViewer");
        //        var response = await client.PostAsync("sales", content);


        //        if (response.IsSuccessStatusCode)
        //        {
        //            return RedirectToAction("index");
        //        }
        //    }

        //    ViewBag.ProductId = new SelectList(await GetProduct(), "Id", "Name");
        //    return View();

        //}
        //private async Task<IEnumerable<ProductVM>> GetProduct()
        //{
        //    HttpClient client = _factory.CreateClient();
        //    client.BaseAddress =
        //    new Uri("https://localhost:7178/api/");
        //    client.DefaultRequestHeaders.Add(
        //    HeaderNames.UserAgent, "ExchangeRateViewer");
        //    var response = await client.GetAsync("Products");
        //    response.EnsureSuccessStatusCode();
        //    var result = await response.Content.ReadFromJsonAsync<IEnumerable<ProductVM>>();
        //    return result ?? Enumerable.Empty<ProductVM>();
        //}

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


        //[HttpPost]
        //public async Task<IActionResult> Create(CaseMaster caseinfo, string act = "")
        //{

        //    if (act == "add")
        //    {
        //        caseinfo.CaseDetails.Add(new CaseDetails());
        //    }
        //    if (act.StartsWith("remove"))
        //    {

        //        int index = int.Parse(act.Substring(act.IndexOf("_") + 1));
        //        caseinfo.Details.RemoveAt(index);
        //    }
        //    if (act == "Save")
        //    {
        //        var jsonSerializerSettings = new JsonSerializerSettings
        //        {
        //            StringEscapeHandling = StringEscapeHandling.EscapeNonAscii
        //        };
        //        string orderString = JsonConvert.SerializeObject(caseinfo, jsonSerializerSettings);
        //        var content = new MultipartFormDataContent();
        //        // content.Add(new StringContent(orderString, "Orderdata"));
        //        content.Add(new StringContent(orderString), "orderdata");
        //        //content.Add(new StringContent(entity.ContactNumber), "ContactNumber");
        //        if (caseinfo.Image != null)
        //        {
        //            var imageContent = new StreamContent(caseinfo.imge.OpenReadStream());
        //            imageContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(order.Image.ContentType);
        //            content.Add(imageContent, "LOGOFile", order.Image.FileName);

        //        }
        //        HttpClient client = _factory.CreateClient();
        //        client.BaseAddress =
        //        new Uri("https://localhost:7178/api/");
        //        client.DefaultRequestHeaders.Add(
        //        HeaderNames.UserAgent, "ExchangeRateViewer");
        //        var response = await client.PostAsync("sales", content);


        //        if (response.IsSuccessStatusCode)
        //        {
        //            return RedirectToAction("index");
        //        }
        //    }

        //    ViewBag.ProductId = new SelectList(await GetProduct(), "Id", "Name");
        //    return View();

        //}









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
