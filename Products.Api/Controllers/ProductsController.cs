using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Products.Api.Data;
using Products.Api.Dto;
using Products.Api.Models;

namespace Products.Api.Controllers;

[ApiController]
[Route("v1/products")]
public class ProductsController : ControllerBase
{
    private readonly EcommerceContext _context;
    private readonly IMapper _mapper;

    public ProductsController(EcommerceContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    [HttpGet]
    public async Task<ActionResult<ProductDto>> Get()
    {
        var products = await _context.Products.ToListAsync();
        var productDtoList = _mapper.Map<List<ProductDto>>(products);

        return Ok(productDtoList);
    }
    
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] ProductDto productDto)
    {
        var product = _mapper.Map<Product>(productDto);
        _context.Products.Add(product);
        await _context.SaveChangesAsync();
        return StatusCode(201);
    }
    
    [HttpPost("update-inventory")]
    public async Task<IActionResult> PlaceOrder([FromBody] Dictionary<Guid, int> orderDetails)
    {
        foreach (var order in orderDetails)
        {
            var product = _context.Products.SingleOrDefault(p => p.Id == order.Key);
            if (product is null)
            {
                return BadRequest();
            }

            product.Inventory -= order.Value;
            if (product.Inventory < 0)
            {
                return BadRequest();
            }
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
        }
        return StatusCode(200);
    }
}
