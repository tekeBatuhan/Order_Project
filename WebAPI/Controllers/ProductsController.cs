﻿
using Business.Handlers.Products.Commands;
using Business.Handlers.Products.Queries;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Entities.Concrete;
using System.Collections.Generic;
using Core.Entities.Dtos;
using Business.Handlers.WareHouseProductMappings.Commands;
using ServiceStack;
using Entities.Enums;

namespace WebAPI.Controllers
{
    /// <summary>
    /// Products If controller methods will not be Authorize, [AllowAnonymous] is used.
    /// </summary>
    [Microsoft.AspNetCore.Mvc.Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class ProductsController : BaseApiController
    {
        ///<summary>
        ///List Products
        ///</summary>
        ///<remarks>Products</remarks>
        ///<return>List Products</return>
        ///<response code="200"></response>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Product>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpGet("getall")]
        public async Task<IActionResult> GetList()
        {
            var result = await Mediator.Send(new GetProductsQuery());
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        ///<summary>
        ///It brings the details according to its id.
        ///</summary>
        ///<remarks>Products</remarks>
        ///<return>Products List</return>
        ///<response code="200"></response>  
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Product))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpGet("getbyid")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await Mediator.Send(new GetProductQuery { Id = id });
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        /// <summary>
        /// Add Product.
        /// </summary>
        /// <param name="createProduct"></param>
        /// <returns></returns>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] ProductDto createProduct)
        {
            CreateProductCommand create = new CreateProductCommand
            {
                ColorId = createProduct.ColorId,
                Name = createProduct.Name,
                CreatedDate = createProduct.CreatedDate,
                CreatedUserId = createProduct.CreatedUserId,
                LastUpdatedDate = createProduct.LastUpdatedDate,
                LastUpdatedUserId = createProduct.LastUpdatedUserId,
                Size = (Size)createProduct.Size,
                Status = createProduct.Status,
                isDeleted = createProduct.isDeleted
            };

            var result = await Mediator.Send(create);
            if (result.Success)
            {
                CreateWareHouseProductMappingCommand mapping = new CreateWareHouseProductMappingCommand
                {
                    ProductId = result.Id,
                    WareHouseId = createProduct.WareHouseId,
                    CreatedDate = createProduct.CreatedDate,
                    CreatedUserId = createProduct.CreatedUserId,
                    LastUpdatedDate = createProduct.LastUpdatedDate,
                    LastUpdatedUserId = createProduct.LastUpdatedUserId,
                    ReadyForSale = createProduct.ReadyForSale,
                    Count = createProduct.Count,
                };

                var result2 = await Mediator.Send(mapping);

                if (result2.Success)
                {
                    return Ok(result.Message);
                }
            }
            return BadRequest(result.Message);
        }

        /// <summary>
        /// Update Product.
        /// </summary>
        /// <param name="updateProduct"></param>
        /// <returns></returns>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] ProductDto updateProduct)
        {
            UpdateProductCommand update = new UpdateProductCommand
            {
                Id = updateProduct.Id,
                ColorId = updateProduct.ColorId,
                Name = updateProduct.Name,
                LastUpdatedDate = updateProduct.LastUpdatedDate,
                LastUpdatedUserId = updateProduct.LastUpdatedUserId,
                Size = (Size)updateProduct.Size,
                Status = updateProduct.Status,
                isDeleted = updateProduct.isDeleted
            };

            var result = await Mediator.Send(update);
            if (result.Success)
            {
                //product listesini ekranda gosterirken mapping tablosunu ve warehouse tablosunu cekemedegim icin ekrandan duzenleme yaptigimda product verileri geliyor ama mapping tablosuınun verisi (ID) gelmedigi icin burada UpdateWareHouseProductMappingCommand objesini olusturup repoya gonderemedim. BURADA KALDIM
                UpdateWareHouseProductMappingCommand mapping = new UpdateWareHouseProductMappingCommand
                {
                    ProductId = result.Id,
                    WareHouseId = updateProduct.WareHouseId,
                    LastUpdatedDate = updateProduct.LastUpdatedDate,
                    LastUpdatedUserId = updateProduct.LastUpdatedUserId,
                    ReadyForSale = updateProduct.ReadyForSale,
                    Count = updateProduct.Count,
                };

                var result2 = await Mediator.Send(mapping);

                if (result2.Success)
                {
                    return Ok(result2.Message);
                }
            }

            return BadRequest(result.Message);
        }

        /// <summary>
        /// Delete Product.
        /// </summary>
        /// <param name="deleteProduct"></param>
        /// <returns></returns>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] DeleteProductCommand deleteProduct)
        {
            var result = await Mediator.Send(deleteProduct);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }
    }
}
