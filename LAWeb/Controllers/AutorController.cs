

using LADomain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using MediaTypeHeaderValue = System.Net.Http.Headers.MediaTypeHeaderValue;

namespace AutorsWeb
{
    [Authorize]
    public class AutorController : Controller
        {
        private readonly TokenHttp _token;

        public AutorController(TokenHttp token) {
            _token = token;
        }
        // GET: AutorController
        public ActionResult Index()
            {
            var httpClient = _token.PrepareRequest();

            var response = httpClient.GetAsync("https://localhost:7000/api/autores").Result;

            if (response.IsSuccessStatusCode == false)
                throw new Exception("Erro ao tentar chamar a api do Autor");

            String jsonString = response.Content.ReadAsStringAsync().Result;

            if (!jsonString.Length.Equals(0))
            {
                var result = JsonSerializer.Deserialize<IEnumerable<Autor>>(jsonString);
                return View(result);
            }
            else
            {
                List<Autor> result = new List<Autor>();


                return View(result);
            }
        }

        // GET: AutorController/Details/5

        public ActionResult Details(int id)
            {
            var httpClient = _token.PrepareRequest();

            var response = httpClient.GetAsync($"https://localhost:7000/api/autores/{id}").Result;

            if (response.IsSuccessStatusCode == false)
                throw new Exception("Erro ao tentar chamar a api do Autor");

            var jsonString = response.Content.ReadAsStringAsync().Result;

            if (!jsonString.Length.Equals(0))
            {
                var result = JsonSerializer.Deserialize<Autor>(jsonString);
                return View(result);
            }
            else
            {
                Autor result = new Autor();


                return View(result);
            }

        }

        // GET: AutorController/Create
        public ActionResult Create()
            {
            return View();
            }

        // POST: AutorController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Autor model)
            {
            if (ModelState.IsValid == false)
                return View(model);

            try
                {
                var json = JsonSerializer.Serialize(model);
                StringContent content = new(json, new MediaTypeHeaderValue("application/json"));


                var httpClient = _token.PrepareRequest();

                var response = httpClient.PostAsync($"https://localhost:7000/api/autores", content).Result;

                if (response.IsSuccessStatusCode == false)
                    throw new Exception("Erro ao tentar chamar a api do Autor");

                return RedirectToAction(nameof(Index));
                }
            catch
                {
                return View();
                }
            }

        // GET: AutorController/Edit/5
        public ActionResult Edit(int id)
            {
            var httpClient = _token.PrepareRequest();

            var response = httpClient.GetAsync($"https://localhost:7000/api/autores/{id}").Result;

            if (response.IsSuccessStatusCode == false)
                throw new Exception("Erro ao tentar chamar a api do Autor");

            var jsonString = response.Content.ReadAsStringAsync().Result;
            if (!jsonString.Length.Equals(0))
            {
                var result = JsonSerializer.Deserialize<Autor>(jsonString);
                return View(result);
            }
            else
            {
                Autor result = new Autor();


                return View(result);
            }
        }

        // POST: AutorController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Autor model)
            {
            try
                {
                var json = JsonSerializer.Serialize(model);
                StringContent content = new StringContent(json, new MediaTypeHeaderValue("application/json"));

                var httpClient = _token.PrepareRequest();

                var response = httpClient.PutAsync($"https://localhost:7000/api/autores/{id}", content).Result;

                if (response.IsSuccessStatusCode == false)
                    throw new Exception("Erro ao tentar chamar a api do Autor");


                return RedirectToAction(nameof(Index));
                }
            catch
                {
                return View();
                }
            }

        // GET: AutorController/Delete/5
        public ActionResult Delete(int id)
            {
            var httpClient = _token.PrepareRequest();

            var response = httpClient.GetAsync($"https://localhost:7000/api/autores/{id}").Result;

            if (response.IsSuccessStatusCode == false)
                throw new Exception("Erro ao tentar chamar a api do Autor");

            var jsonString = response.Content.ReadAsStringAsync().Result;

            if (!jsonString.Length.Equals(0))
            {
                var result = JsonSerializer.Deserialize<Autor>(jsonString);
                return View(result);
            }
            else
            {
                Autor result = new Autor();


                return View(result);
            }
        }

        // POST: AutorController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
            {
            try
                {
                var httpClient = _token.PrepareRequest();

                var response = httpClient.DeleteAsync($"https://localhost:7000/api/autores/{id}").Result;

                if (response.IsSuccessStatusCode == false)
                    throw new Exception("Erro ao tentar chamar a api do Autor");

                return RedirectToAction(nameof(Index));
                }
            catch
                {
                return View();
                }
            }

        }
}
