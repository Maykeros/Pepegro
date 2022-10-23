namespace Pepegro.Api.Controllers;

using Bll.Services.MainServices;
using Domain.DTO_s.MainEntities;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[Controller]")]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }
    [HttpGet("GetPage")]
  public async Task<IActionResult> GetPageOfProducts([FromQuery] int PageNumber,[FromQuery] int PageCount)
  {
      if (!ModelState.IsValid)
      {
          return BadRequest("Parameters is not valid");
      }
      return Ok(await _productService.GetPageOfProducts(PageNumber, PageCount));
  }
  [HttpGet("GetById")]
  public async Task<IActionResult> GetProductById([FromQuery] int id)
  {
      if (!ModelState.IsValid)
      {
          return BadRequest("Parameters is not valid");
      }

      return Ok(await _productService.GetProductById(id));
  }
  [HttpPost]
  public async Task<IActionResult> CreateProduct([FromBody] CreateProductDTO productDto)
  {
      if (!ModelState.IsValid)
      {
          return BadRequest("Parameters is not valid");
      }

      return CreatedAtAction("GetProductById",await _productService.CreateProduct(productDto));
  }
  [HttpPut]
  public async Task<IActionResult> UpdateProuct([FromBody] UpdateProductDTO productDto, [FromQuery] int productId)
  {
      if (!ModelState.IsValid)
      {
          return BadRequest("Parameters is not valid");
      }

      return CreatedAtAction("GetProductById",await _productService.UpdateProduct(productId ,productDto));
  }
  [HttpDelete]
  public async Task<IActionResult> DeleteProduct([FromQuery] int productId)
  {
      if (!ModelState.IsValid)
      {
          return BadRequest("Parameters is not valid");
      }

      await _productService.DeleteProduct(productId);
      
      return Ok();
  }
}