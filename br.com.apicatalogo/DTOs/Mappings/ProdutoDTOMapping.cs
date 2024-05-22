using System;
using AutoMapper;
using br.com.apicatalogo.Models;

namespace br.com.apicatalogo.DTOs.Mappings
{
    public class ProdutoDTOMapping : Profile
    {
        public ProdutoDTOMapping()
        {
            CreateMap<ProdutoDTO, Produto>().ReverseMap();
            CreateMap<CategoriaDTO, Categoria>().ReverseMap();
            CreateMap<Produto, ProdutoDtoUpdateRequest>().ReverseMap();
            CreateMap<Produto, ProdutoDtoUpdateResponse>().ReverseMap();
        }
    }
}

