﻿using com.productManagementSystem.DBEntity.ViewModels;
using com.productManagementSystem.services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace com.productManagementSystem.Controllers
{
    [Route("api/OrderController")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        #region Configuration
        private readonly IOrderService _orderService;
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        #endregion Configuration

        #region Order_GetAll
        [HttpGet("getorder")]
        public IActionResult GetAll()
        {
            var order = _orderService.GetAll();
            return Ok(order);
        }
        #endregion Order_GetAll

        #region Order_CreateProduct
        [HttpPost("addorder")]
        public async Task<IActionResult> CreateOrder(CreateOrderModelDto order)
        {
            bool cc = await _orderService.CreateOrder(order);
            return Ok(cc);
        }
        #endregion Order_CreateProduct

        #region Order_GetById
        [HttpGet("getbyid/{Id:int}")]
        public IActionResult GetById(int Id)
        {
            var order = _orderService.GetById(Id);
            return Ok(order);
        }
        #endregion Order_GetById

        #region Order_UpdateOrder
        [HttpPut("editorder/{Id:int}")]
        public IActionResult UpdateOrder(int Id, UpdateOrderModelDto orders)
        {
            var order = _orderService.UpdateOrder(Id, orders);
            return Ok();
        }
        #endregion Order_UpdateOrder

        #region Order_DeleteOrder
        [HttpDelete("deleteorder/{Id:int}")]
        public async Task<IActionResult> DeleteOrder(int Id)
        {
            bool order = await _orderService.DeleteOrder(Id);
            return Ok();
        }
        #endregion Order_DeleteOrder

    }
}
