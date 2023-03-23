using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace LADomain
{
    public class Autor

    {
        public Autor() { }

        public Autor(int id, string nome, string sobrenome, string email,string telefone , DateTime data , List<Livro> livros)
        {
            Id = id;
            Nome = nome;
            Sobrenome = sobrenome;
            Email = email;
            Telefone = telefone;
            Data = data;
            Livros = livros;
        }
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("Nome")]
        public string Nome { get; set; }
        [JsonPropertyName("Sobrenome")]
        public string Sobrenome { get; set; }
        [JsonPropertyName("Email")]
        public string Email { get; set; }
        [JsonPropertyName("Telefone")]
        public string Telefone { get; set; }
        [JsonPropertyName("Data")]
        public DateTime Data { get; set; }
        [JsonPropertyName("Livros")]
        public List<Livro> Livros { get; set; }

        
    }
}
