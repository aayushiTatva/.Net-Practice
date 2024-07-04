﻿using com.productManagementSystem.DBEntity.ViewModels;
using com.productManagementSystem.services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace com.productManagementSystem.Controllers
{
    [Route("api/ProductController")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        #region Configuration
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        #endregion Configuration

        #region Product_GetAll
        [HttpGet("getproducts")]
        public IActionResult GetAll()
        {
            var product = _productService.GetAll();
            return Ok(product);
        }
        #endregion Product_GetAll

        #region Product_CreateProduct
        [HttpPost("addproduct")]
        public async Task<IActionResult> CreateProduct(CreateProductModelDto cmd)
        {
            if (ModelState.IsValid)
            {
                bool cc = await _productService.CreateProduct(cmd);
                return Ok(cc);
            }
            else { return BadRequest(ModelState); }
        }
        #endregion Product_CreateProduct

        #region Product_GetById
        [HttpGet("getbyid/{Id:int}")]
        public IActionResult GetById(int Id)
        {
            var product = _productService.GetById(Id);
            return Ok(product);
        }
        #endregion Product_GetById

        #region Product_UpdateProduct
        [HttpPut("editproduct/{Id:int}")]
        public IActionResult UpdateProduct(int Id, UpdateProductModelDto cmd)
        {
            var product = _productService.UpdateProduct(Id, cmd);
            return Ok();
        }
        #endregion Product_UpdateProduct

        #region Product_DeleteProduct
        [HttpDelete("deleteproduct/{Id:int}")]
        public async Task<IActionResult> DeleteProduct(int Id)
        {
            bool cd = await _productService.DeleteProduct(Id);
            return Ok();
        }
        #endregion Product_DeleteProduct
    }
}
