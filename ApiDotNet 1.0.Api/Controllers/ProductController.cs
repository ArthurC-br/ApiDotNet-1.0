using ApiDotNet_1._0.Application.DTOs;
using ApiDotNet_1._0.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ApiDotNet_1._0.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        [HttpGet]
        public async Task<ActionResult> GetAsync()
        {
            var result = await _productService.GetAsync();
            if (result.IsSuccess) return Ok(result);
            return BadRequest(result);
        }
        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult> GetByIdAsync(int id)
        {
            var result = await _productService.GetByIdAsync(id);
            if (result.IsSuccess) return Ok(result);

            return BadRequest(result);
        }
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        
        [HttpPost]
        public async Task<ActionResult> PostAsync([FromBody] ProductDTO productDTO)
        {
            var result = await _productService.CreateAsync(productDTO);
            if(result.IsSuccess) return Ok(result);
            
            return BadRequest(result);
        }
        [HttpPut]
        public async Task<ActionResult> UpdateAsync([FromBody] ProductDTO productDTO)
        {
            var result = await _productService.UpdateAsync(productDTO);
            if (result.IsSuccess) return Ok(result);

            return BadRequest(result);
        }
        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            var result = await _productService.DeleteAsync(id);
            if (result.IsSuccess) return Ok(result);

            return BadRequest(result);
        }
        
    }
}
