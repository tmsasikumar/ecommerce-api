using AutoMapper;
using Products.Api.Dto;
using Products.Api.Models;

namespace Products.Api.Automapper;

public class ProductProfileConfiguration: Profile
{
    public ProductProfileConfiguration()
    {
        CreateMap<ProductDto, Product>();
    }
}