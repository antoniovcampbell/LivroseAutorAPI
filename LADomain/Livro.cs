using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace LADomain
{
    public class Livro
    {
        public Livro() { }

        public Livro(int id, string titulo, int isbn, DateTime ano,List<Autor> autores)
        {
            Id = id;
            Titulo = titulo;
            ISBN = isbn;
            Ano = ano;
            Autores = autores;
           
        }
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("Titulo")]
        public string Titulo { get; set; }
        [JsonPropertyName("ISBN")]
        public int ISBN { get; set; }
        [JsonPropertyName("Ano")]
        public DateTime Ano { get; set; }
        [JsonPropertyName("Autores")]
        public List<Autor> Autores { get; set; }
        
        

        
    }
}
