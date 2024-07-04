using com.productManagementSystem.DBEntity.ViewModels;

namespace com.productManagementSystem.services.Interface
{
    public interface IProductService
    {
        List<ProductModel> GetAll();
        Task<bool> CreateProduct(CreateProductModelDto product);
        ProductModel GetById(int Id);
        Task<bool> UpdateProduct(int Id, UpdateProductModelDto supplier);
        Task<bool> DeleteProduct(int Id);
    }
}
