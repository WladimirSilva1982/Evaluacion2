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
    public class ViajeroController : Controller
    {

        string BaseUrl = "https://localhost:44301/";

        // GET: ViajeroController
        public async Task<ActionResult> Index()
        {
            List<Viajero> viajerosList = new List<Viajero>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //busco los viajeros en la api usando el HttpClient
                HttpResponseMessage Res = await client.GetAsync("api/Viajero");
                if (Res.IsSuccessStatusCode)
                {
                    var ViajeroResponse = Res.Content.ReadAsStringAsync().Result;

                    viajerosList = JsonConvert.DeserializeObject<List<Viajero>>(ViajeroResponse);
                }
            }

            return View(viajerosList);
        }

        // GET: ViajeroController/Details/5
        public async Task<ActionResult> Details(string id)
        {
            Viajero viajero = new Viajero();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //busco los viajeros en la api usando el HttpClient
                HttpResponseMessage Res = await client.GetAsync("api/Viajero/" + id);
                if (Res.IsSuccessStatusCode)
                {
                    var ViajeroResponse = Res.Content.ReadAsStringAsync().Result;

                    viajero = JsonConvert.DeserializeObject<Viajero>(ViajeroResponse);
                }
            }

            return View(viajero);
        }

        // GET: ViajeroController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ViajeroController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Viajero viajero)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUrl);

                var response = await client.PostAsJsonAsync("api/Viajero/", viajero);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            return View();
        }

        // GET: ViajeroController/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            Viajero viajero = new Viajero();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //busco los viajeros en la api usando el HttpClient
                HttpResponseMessage Res = await client.GetAsync("api/Viajero/" + id);
                if (Res.IsSuccessStatusCode)
                {
                    var ViajeroResponse = Res.Content.ReadAsStringAsync().Result;

                    viajero = JsonConvert.DeserializeObject<Viajero>(ViajeroResponse);
                }
            }

            return View(viajero);
        }

        // POST: ViajeroController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Viajero viajero)
        {

            ViewBag.Message = "Se ha actualizado el cliente satisfactoriamente!!!";

            if (ModelState.IsValid)
            {

                using (var client = new HttpClient())
                using (var request = new HttpRequestMessage(HttpMethod.Put, BaseUrl + "api/Viajero/" + viajero.Cedula))
                {
                    var json = JsonConvert.SerializeObject(viajero);
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

        // GET: ViajeroController/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            Viajero viajero = new Viajero();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //busco los viajeros en la api usando el HttpClient
                HttpResponseMessage Res = await client.GetAsync("api/Viajero/" + id);
                if (Res.IsSuccessStatusCode)
                {
                    var ViajeroResponse = Res.Content.ReadAsStringAsync().Result;

                    viajero = JsonConvert.DeserializeObject<Viajero>(ViajeroResponse);
                }
            }

            return View(viajero);
        }

        // POST: ViajeroController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(Viajero viajero)
        {

            ViewBag.Message = "Se ha actualizado el cliente satisfactoriamente!!!";

            if (ModelState.IsValid)
            {

                using (var client = new HttpClient())
                using (var request = new HttpRequestMessage(HttpMethod.Delete, BaseUrl + "api/Viajero/" + viajero.Cedula))
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
