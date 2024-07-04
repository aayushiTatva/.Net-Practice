﻿using com.productManagementSystem.DBEntity.DataModels;
using com.productManagementSystem.DBEntity.ViewModels;
using com.productManagementSystem.generic.repositories.Interface;
using com.productManagementSystem.services.Interface;

namespace com.productManagementSystem.services
{
    public class OrderService: IOrderService
    {
        #region Configuration
        private readonly IGenericRepositories<Order> _orderRepository;
        private readonly IGenericRepositories<Category> _categoryRepository;
        private readonly IGenericRepositories<Product> _productRepository;
        private readonly IGenericRepositories<OrderProduct> _orderproductrepository;
        private readonly IGenericRepositories<Supplier> _supplierRepository;
        public OrderService(IGenericRepositories<Order> orderRepository, IGenericRepositories<Category> categoryRepository, IGenericRepositories<Product> productRepository, 
            IGenericRepositories<OrderProduct> orderproductrepository, IGenericRepositories<Supplier> supplierRepository)
        {
            _orderRepository = orderRepository;
            _categoryRepository = categoryRepository;
            _productRepository = productRepository;
            _orderproductrepository = orderproductrepository;
            _supplierRepository = supplierRepository;
        }
        #endregion Configuration

        #region Order_GetAll
        public List<OrderModel> GetAll()
        {
            var om = _orderRepository.GetAll().Where(e => e.Isdeleted == false).Select(e => new OrderModel
            {
                Id = e.Id,
                OrderDate = e.OrderDate.Value.ToString("MMM dd, yyyy"),
            }).ToList();
            return om;
        }
        #endregion Order_GetAll

        #region Order_CreateOrder
        public async Task<bool> CreateOrder(CreateOrderModelDto addorder)
        {
            Order order = new()
            {
                OrderDate = DateTime.Now,
            };
            _orderRepository.Add(order);

            OrderProduct op = new()
            {
                OrderId = order.Id,
                ProductId = addorder.ProductId,
                Price = addorder.Price,
                Quantity = addorder.Quantity,
                DeliveryDate = DateTime.Now.AddDays(4),
            };
            _orderproductrepository.Add(op);

            return true;
        }
        #endregion Order_CreateOrder

        #region Order_GetById 
        public List<OrderModel> GetById(int Id)
        {
            var om = (from op in _orderproductrepository.GetAll()
                      join order in _orderRepository.GetAll()
                      on op.OrderId equals order.Id into orderGroup
                      from og in orderGroup.DefaultIfEmpty()
                      join product in _productRepository.GetAll()
                      on op.ProductId equals product.Id into productGroup
                      from pg in productGroup.DefaultIfEmpty()
                      where og.Isdeleted == false && pg.Isdeleted == false && op.OrderId == Id
                      select new OrderModel
                      {
                          Id = og.Id,
                          ProductName = pg.Name,
                          Price = (int)op.Price,
                          Quantity = (int)op.Quantity,
                          SupplierName = _supplierRepository.GetAll().FirstOrDefault(e => e.Id == pg.SupplierId).FirstName + " " + _supplierRepository.GetAll().FirstOrDefault(e => e.Id == pg.SupplierId).LastName,
                          CategoryName = _categoryRepository.GetAll().FirstOrDefault(e => e.Id == pg.CategoryId).Name,
                          OrderDate = og.OrderDate.Value.ToString("MMM dd, yyyy"),
                          DeliveryDate = op.DeliveryDate.Value.ToString("MMM dd, yyyy"),
                      }).ToList();
            return om;
        }
        #endregion Order_GetById

        #region Order_UpdateOrder
        public async Task<bool> UpdateOrder(int Id, UpdateOrderModelDto omd)
        {
            var order = _orderRepository.GetAll().FirstOrDefault(e => e.Id == omd.OrderId);
            var ordermap = _orderproductrepository.GetAll().FirstOrDefault(e => e.Id == Id);
            if(ordermap == null)
            {
                return false;
            }
            else
            {
                order.ModifiedDate = DateTime.Now;
                _orderRepository.Update(order);

                ordermap.ProductId = omd.ProductId;
                ordermap.Price = omd.Price;
                ordermap.Quantity = omd.Quantity;
                ordermap.DeliveryDate = DateTime.Now.AddDays(4);
                _orderproductrepository.Update(ordermap);
            }
            return true;
        }
        #endregion Order_UpdateOrder

        #region Order_DeleteOrder
        public async Task<bool> DeleteOrder(int Id)
        {
            var ordermap = _orderproductrepository.GetAll().FirstOrDefault(e => e.OrderId == Id);
            var order = _orderRepository.GetAll().FirstOrDefault(e => e.Id == Id);
            if(ordermap == null || order == null)
            {
                return false;
            }
            else
            {
                _orderproductrepository.Remove(ordermap);
                _orderRepository.Remove(order);
            }
            return true;
        }
        #endregion Order_DeleteOrder

       /* public List<Product> GetProductsForOrder(int orderId, UpdateOrderModelDto model)
        {
            // Retrieve the order by its ID using the repository
            var order = _orderproductrepository.GetOrderById(orderId);

            // Check if the order exists
            if (order == null)
            {
                return null; // Return null if order is not found
            }

            // Return the list of products associated with the order
            return order.Product;
        }*/
    }
}
