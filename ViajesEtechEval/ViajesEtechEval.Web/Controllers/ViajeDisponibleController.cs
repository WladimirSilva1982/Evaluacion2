using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using ViajesEtechEval.Web.Models.Entities;

namespace ViajesEtechEval.Web.Controllers
{
    public class ViajeDisponibleController : Controller
    {
        string BaseUrl = "https://localhost:44301/";

        // GET: ViajeDisponibleController
        public async Task<ActionResult> Index()
        {
            List<ViajeDisponible> viajesList = new List<ViajeDisponible>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //busco los viajeros en la api usando el HttpClient
                HttpResponseMessage Res = await client.GetAsync("api/ViajeDisponible");
                if (Res.IsSuccessStatusCode)
                {
                    var ViajeResponse = Res.Content.ReadAsStringAsync().Result;

                    viajesList = JsonConvert.DeserializeObject<List<ViajeDisponible>>(ViajeResponse);
                }
            }

            return View(viajesList);
        }

        // GET: ViajeDisponibleController/Details/5
        public async Task<ActionResult> Details(string id)
        {
            ViajeDisponible viajeDisp = new ViajeDisponible();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //busco los viajeros en la api usando el HttpClient
                HttpResponseMessage Res = await client.GetAsync("api/ViajeDisponible/" + id);
                if (Res.IsSuccessStatusCode)
                {
                    var ViajeResponse = Res.Content.ReadAsStringAsync().Result;

                    viajeDisp = JsonConvert.DeserializeObject<ViajeDisponible>(ViajeResponse);
                }
            }

            return View(viajeDisp);
        }

        // GET: ViajeDisponibleController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ViajeDisponibleController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ViajeDisponible viajeDisponible)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUrl);

                var response = await client.PostAsJsonAsync("api/ViajeDisponible/", viajeDisponible);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            return View();
        }

        // GET: ViajeDisponibleController/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            ViajeDisponible viajeDisp = new ViajeDisponible();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //busco los viajeros en la api usando el HttpClient
                HttpResponseMessage Res = await client.GetAsync("api/ViajeDisponible/" + id);
                if (Res.IsSuccessStatusCode)
                {
                    var ViajeResponse = Res.Content.ReadAsStringAsync().Result;

                    viajeDisp = JsonConvert.DeserializeObject<ViajeDisponible>(ViajeResponse);
                }
            }

            return View(viajeDisp);
        }

        // POST: ViajeDisponibleController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(ViajeDisponible viajeDisponible)
        {
            ViewBag.Message = "Se ha actualizado el cliente satisfactoriamente!!!";

            if (ModelState.IsValid)
            {

                using (var client = new HttpClient())
                using (var request = new HttpRequestMessage(HttpMethod.Put, BaseUrl + "api/ViajeDisponible/" + viajeDisponible.CodViaje))
                {
                    var json = JsonConvert.SerializeObject(viajeDisponible);
                    using (var stringContent = new StringContent(json, Encoding.UTF8, "application/json"))
                    {
                        request.Content = stringContent;

                        using (var response = await client
                            .SendAsync(request, HttpCompletionOption.ResponseHeadersRead)
                            .ConfigureAwait(false))
                        {
                            response.EnsureSuccessStatusCode();
                        }
                    }

                }
            }
            return View();
        }

        // GET: ViajeDisponibleController/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            ViajeDisponible viajeDisp = new ViajeDisponible();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //busco los viajeros en la api usando el HttpClient
                HttpResponseMessage Res = await client.GetAsync("api/ViajeDisponible/" + id);
                if (Res.IsSuccessStatusCode)
                {
                    var ViajeroResponse = Res.Content.ReadAsStringAsync().Result;

                    viajeDisp = JsonConvert.DeserializeObject<ViajeDisponible>(ViajeroResponse);
                }
            }

            return View(viajeDisp);
        }

        // POST: ViajeDisponibleController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(ViajeDisponible viajeDisponible)
        {
            ViewBag.Message = "Se ha actualizado el cliente satisfactoriamente!!!";

            if (ModelState.IsValid)
            {

                using (var client = new HttpClient())
                using (var request = new HttpRequestMessage(HttpMethod.Delete, BaseUrl + "api/ViajeDisponible/" + viajeDisponible.CodViaje))
                {

                    using (var response = await client
                        .SendAsync(request, HttpCompletionOption.ResponseHeadersRead)
                        .ConfigureAwait(false))
                    {
                        response.EnsureSuccessStatusCode();
                    }

                }
            }
            return View();
        }
    }
}
