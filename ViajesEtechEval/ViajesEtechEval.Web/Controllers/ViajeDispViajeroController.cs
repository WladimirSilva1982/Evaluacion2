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
    public class ViajeDispViajeroController : Controller
    {
        string BaseUrl = "https://localhost:44301/";

        // GET: ViajeDispViajeroController
        public async Task<ActionResult> Index()
        {
            List<ViajeDispoViajero> viajesList = new List<ViajeDispoViajero>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //busco los viajeros en la api usando el HttpClient
                HttpResponseMessage Res = await client.GetAsync("api/ViajeDispoViajero");
                if (Res.IsSuccessStatusCode)
                {
                    var ViajeResponse = Res.Content.ReadAsStringAsync().Result;

                    viajesList = JsonConvert.DeserializeObject<List<ViajeDispoViajero>>(ViajeResponse);
                }
            }

            return View(viajesList);
        }

        // GET: ViajeDispViajeroController/Details/5
        public async Task<ActionResult> Details(string id)
        {
            ViajeDispoViajero viajeDisp = new ViajeDispoViajero();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //busco los viajeros en la api usando el HttpClient
                HttpResponseMessage Res = await client.GetAsync("api/ViajeDispoViajero/" + id);
                if (Res.IsSuccessStatusCode)
                {
                    var ViajeResponse = Res.Content.ReadAsStringAsync().Result;

                    viajeDisp = JsonConvert.DeserializeObject<ViajeDispoViajero>(ViajeResponse);
                }
            }

            return View(viajeDisp);
        }

        // GET: ViajeDispViajeroController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ViajeDispViajeroController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ViajeDispoViajero viajeDispoViajero)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUrl);

                var response = await client.PostAsJsonAsync("api/ViajeDispoViajero/", viajeDispoViajero);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            return View();
        }

        // GET: ViajeDispViajeroController/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            ViajeDispoViajero viajeDisp = new ViajeDispoViajero();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //busco los viajeros en la api usando el HttpClient
                HttpResponseMessage Res = await client.GetAsync("api/ViajeDispoViajero/" + id);
                if (Res.IsSuccessStatusCode)
                {
                    var ViajeResponse = Res.Content.ReadAsStringAsync().Result;

                    viajeDisp = JsonConvert.DeserializeObject<ViajeDispoViajero>(ViajeResponse);
                }
            }

            return View(viajeDisp);
        }

        // POST: ViajeDispViajeroController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(ViajeDispoViajero viajeDispoViajero)
        {
            ViewBag.Message = "Se ha actualizado el cliente satisfactoriamente!!!";

            if (ModelState.IsValid)
            {

                using (var client = new HttpClient())
                using (var request = new HttpRequestMessage(HttpMethod.Put, BaseUrl + "api/viajeDispoViajero/" + viajeDispoViajero.Cedula))
                {
                    var json = JsonConvert.SerializeObject(viajeDispoViajero);
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

        // GET: ViajeDispViajeroController/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            ViajeDispoViajero viajeDisp = new ViajeDispoViajero();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //busco los viajeros en la api usando el HttpClient
                HttpResponseMessage Res = await client.GetAsync("api/ViajeDispoViajero/" + id);
                if (Res.IsSuccessStatusCode)
                {
                    var ViajeroResponse = Res.Content.ReadAsStringAsync().Result;

                    viajeDisp = JsonConvert.DeserializeObject<ViajeDispoViajero>(ViajeroResponse);
                }
            }

            return View(viajeDisp);
        }

        // POST: ViajeDispViajeroController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(ViajeDispoViajero viajeDispoViajero)
        {
            ViewBag.Message = "Se ha actualizado el cliente satisfactoriamente!!!";

            if (ModelState.IsValid)
            {

                using (var client = new HttpClient())
                using (var request = new HttpRequestMessage(HttpMethod.Delete, BaseUrl + "api/viajeDispoViajero/" + viajeDispoViajero.Cedula))
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
