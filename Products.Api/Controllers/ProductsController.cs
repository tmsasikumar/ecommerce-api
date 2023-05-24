using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Products.Api.Data;
using Products.Api.Dto;
using Products.Api.Models;

namespace Products.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductsController : ControllerBase
{
    private readonly EcommerceContext _context;
    private readonly IMapper _mapper;

    public ProductsController(EcommerceContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] ProductDto productDto)
    {
        var product = _mapper.Map<Product>(productDto);
        _context.Products.Add(product);
        await _context.SaveChangesAsync();
        return StatusCode(201);
    }
}
