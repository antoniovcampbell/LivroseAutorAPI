

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

namespace LivrosWeb
{
    [Authorize]
    public class LivrosController : Controller
        {
        private readonly TokenHttp _token;
        public LivrosController(TokenHttp token)
        {
            _token = token;
        }
        public ActionResult Index()
            {
            var httpClient = _token.PrepareRequest();

            var response = httpClient.GetAsync("https://localhost:7000/api/livros").Result;

            if (response.IsSuccessStatusCode == false)
                throw new Exception("Erro ao tentar chamar a api do Livro");

            String jsonString = response.Content.ReadAsStringAsync().Result;

            if (!jsonString.Length.Equals(0))
            {
                var result = JsonSerializer.Deserialize<IEnumerable<Livro>>(jsonString);
                return View(result);
            }
            else
            {
                List<Livro> result = new List<Livro>();


                return View(result);
            }
        }

        

        public ActionResult Details(int id)
            {
            var httpClient = _token.PrepareRequest();

            var response = httpClient.GetAsync($"https://localhost:7000/api/lirvos/{id}").Result;

            if (response.IsSuccessStatusCode == false)
                throw new Exception("Erro ao tentar chamar a api do Livro");

            var jsonString = response.Content.ReadAsStringAsync().Result;

            if (!jsonString.Length.Equals(0))
            {
                var result = JsonSerializer.Deserialize<Livro>(jsonString);
                return View(result);
            }
            else
            {
                Livro result = new Livro();


                return View(result);
            }

        }

        
        public ActionResult Create()
            {
            return View();
            }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Livro model)
            {
            if (ModelState.IsValid == false)
                return View(model);

            try
                {
                var json = JsonSerializer.Serialize(model);
                StringContent content = new(json, new MediaTypeHeaderValue("application/json"));


                var httpClient = _token.PrepareRequest();

                var response = httpClient.PostAsync($"https://localhost:7000/api/livros", content).Result;

                if (response.IsSuccessStatusCode == false)
                    throw new Exception("Erro ao tentar chamar a api do Livro");

                return RedirectToAction(nameof(Index));
                }
            catch
                {
                return View();
                }
            }

        
        public ActionResult Edit(int id)
            {
            var httpClient = _token.PrepareRequest();

            var response = httpClient.GetAsync($"https://localhost:7000/api/livros/{id}").Result;

            if (response.IsSuccessStatusCode == false)
                throw new Exception("Erro ao tentar chamar a api do Livro");

            var jsonString = response.Content.ReadAsStringAsync().Result;
            if (!jsonString.Length.Equals(0))
            {
                var result = JsonSerializer.Deserialize<Livro>(jsonString);
                return View(result);
            }
            else
            {
                Livro result = new Livro();


                return View(result);
            }
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Livro model)
            {
            try
                {
                var json = JsonSerializer.Serialize(model);
                StringContent content = new StringContent(json, new MediaTypeHeaderValue("application/json"));

                var httpClient = _token.PrepareRequest();

                var response = httpClient.PutAsync($"https://localhost:7000/api/livros/{id}", content).Result;

                if (response.IsSuccessStatusCode == false)
                    throw new Exception("Erro ao tentar chamar a api do Livro");


                return RedirectToAction(nameof(Index));
                }
            catch
                {
                return View();
                }
            }

      
        public ActionResult Delete(int id)
            {
            var httpClient = _token.PrepareRequest();

            var response = httpClient.GetAsync($"https://localhost:7000/api/livros/{id}").Result;

            if (response.IsSuccessStatusCode == false)
                throw new Exception("Erro ao tentar chamar a api do Livro");

            var jsonString = response.Content.ReadAsStringAsync().Result;

            if (!jsonString.Length.Equals(0))
            {
                var result = JsonSerializer.Deserialize<Livro>(jsonString);
                return View(result);
            }
            else
            {
                Livro result = new Livro();


                return View(result);
            }
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
            {
            try
                {
                var httpClient = _token.PrepareRequest();

                var response = httpClient.DeleteAsync($"https://localhost:7000/api/livros/{id}").Result;

                if (response.IsSuccessStatusCode == false)
                    throw new Exception("Erro ao tentar chamar a api do Livro");

                return RedirectToAction(nameof(Index));
                }
            catch
                {
                return View();
                }
            }

        }
}
