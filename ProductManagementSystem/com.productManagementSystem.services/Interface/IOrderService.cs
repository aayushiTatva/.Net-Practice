using com.productManagementSystem.DBEntity.ViewModels;

namespace com.productManagementSystem.services.Interface
{
    public interface IOrderService
    {
        List<OrderModel> GetAll();
        Task<bool> CreateOrder(CreateOrderModelDto addorder);
        List<OrderModel> GetById(int Id);
        Task<bool> UpdateOrder(int Id, UpdateOrderModelDto omd);
        Task<bool> DeleteOrder(int Id);
    }
}
