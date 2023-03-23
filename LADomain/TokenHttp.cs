using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;


namespace LADomain
{
    public class TokenHttp
        {
        public string GetToken()
            {
            var usuario = new
                {
                email = "antonio@gmail.com",
                nome = "antonio"

                };

            var body = new StringContent(JsonSerializer.Serialize(usuario), new MediaTypeHeaderValue("application/json"));

            HttpClient httpClient = new HttpClient();

            var response = httpClient.PostAsync("https://localhost:7000/api/token", body).Result;

            if (response.IsSuccessStatusCode == false)
                throw new Exception("Erro ao tentar chamar a api");

            var json = response.Content.ReadAsStringAsync().Result;

            var token = JsonSerializer.Deserialize<Token>(json);

            return token.AccessToken;
            }
        public HttpClient PrepareRequest()
            {

            var token = GetToken();

            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", $"Bearer {token}");

            return httpClient;
            }
        
        

       
    }
}
