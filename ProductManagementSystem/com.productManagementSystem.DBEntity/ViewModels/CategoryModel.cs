using System.ComponentModel.DataAnnotations;

namespace com.productManagementSystem.DBEntity.ViewModels
{
    public class CategoryModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string SearchInput { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }

    public class CreateCategoryModelDto
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }
    }

    public class UpdateCategoryModelDto
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
