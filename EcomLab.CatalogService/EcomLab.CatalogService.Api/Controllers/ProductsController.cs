using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using EcomLab.CatalogService.Api.Data.Repositories;
using EcomLab.CatalogService.Api.Data.Entities;
using Microsoft.AspNetCore.JsonPatch;
using EcomLab.CatalogService.Api.Models;
using AutoMapper;

namespace EcomLab.CatalogService.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly ILogger<ProductsController> logger;
        private readonly IMapper mapper;
        private readonly IProductRepository repository;

        public ProductsController(ILogger<ProductsController> logger, IMapper mapper, IProductRepository repository)
        {
            this.logger = logger;
            this.mapper = mapper;
            this.repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var products = await repository.GetAllAsync();
                return Ok(products);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error occured while fetching Products");
                return StatusCode(500, ex);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute]string id)
        {
            try
            {
                var product = await repository.GetByIdAsync(id);
                return Ok(product);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error occured while fetching Product");
                return StatusCode(500, ex);
            }
        }

        [HttpGet("category/{category}")]
        public async Task<IActionResult> GetByCategory([FromRoute]string category)
        {
            try
            {
                var products = await repository.GetByCategoryAsync(category);
                return Ok(products);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error occured while fetching Products");
                return StatusCode(500, ex);
            }
        }

        [HttpGet("name/{name}")]
        public async Task<IActionResult> GetByName([FromRoute] string name)
        {
            try
            {
                var products = await repository.GetByNameAsync(name);
                return Ok(products);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error occured while fetching Products");
                return StatusCode(500, ex);
            }
        }


        [HttpPost]
        public async Task<IActionResult> Add([FromBody]ProductDTO model)
        {
            try
            {
                var product = mapper.Map<Product>(model);
                await repository.InsertAsync(product);
                return Ok();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error occured while inserting Product");
                return StatusCode(500, ex);
            }
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Update([FromRoute]string id, [FromBody]JsonPatchDocument<Product> model)
        {
            try
            {
                var product = await repository.GetByIdAsync(id);

                if (product == null)
                {
                    return NotFound();
                }                
                model.ApplyTo(product);
                
                bool updated = await repository.UpdateAsync(product);
                if (updated)
                {
                    return Ok();
                }
                return BadRequest("No change detected.");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error occured while updating Product");
                return StatusCode(500, ex);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] string id)
        {
            try
            {
                bool deleted = await repository.DeleteAsync(id);
                if (deleted)
                {
                    return Ok();
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error occured while deleting Product");
                return StatusCode(500, ex);
            }
        }
    }// class ends
}
