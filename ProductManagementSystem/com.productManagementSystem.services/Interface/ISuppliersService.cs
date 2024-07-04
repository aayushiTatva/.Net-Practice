using com.productManagementSystem.DBEntity.ViewModels;

namespace com.productManagementSystem.services.Interface
{
    public interface ISuppliersService
    {
        List<SupplierModel> GetAll();
        Task<bool> CreateSupplier(CreateSupplierModelDto supplier);
        SupplierModel GetById(int Id);
        Task<bool> UpdateSupplier(int Id, UpdateSupplierModelDto supplier);
        Task<bool> DeleteSupplier(int Id);
    }
}
