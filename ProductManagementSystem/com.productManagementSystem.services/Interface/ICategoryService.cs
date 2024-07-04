using com.productManagementSystem.DBEntity.ViewModels;

namespace com.productManagementSystem.services.Interface
{
    public interface ICategoryService
    {
        List<CategoryModel> GetAll();
        Task<bool> CreateCategory(CreateCategoryModelDto category);
        CategoryModel GetById(int Id);
        Task<bool> UpdateCategory(int Id, UpdateCategoryModelDto category);
        Task<bool> DeleteCategory(int Id);
    }
}
