using AutoMapper;
using EcomLab.CatalogService.Api.Data.Entities;
using EcomLab.CatalogService.Api.Models;

namespace EcomLab.CatalogService.Api.Mappings
{
    public class CatalogProfile : Profile
    {
        public CatalogProfile()
        {
            CreateMap<ProductDTO, Product>();
        }
    }
}
