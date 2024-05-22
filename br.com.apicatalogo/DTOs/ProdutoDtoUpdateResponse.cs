using br.com.apicatalogo.Models;

namespace br.com.apicatalogo.DTOs
{
    public class ProdutoDtoUpdateResponse
    {
        public int IdProduto { get; set; }

        public string? Nome { get; set; }

        public string? Descricao { get; set; }

        public decimal Preco { get; set; }

        public string? ImagemUrl { get; set; }

        public float Estoque { get; set; }

        public DateTime DataCadastro { get; set; }

        public int CategoriaId { get; set; }
    }
}

