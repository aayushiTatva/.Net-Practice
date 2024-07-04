using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace com.productManagementSystem.DBEntity.ViewModels
{
    public class ProductModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public string SupplierName { get; set; }
        public int SupplierId { get; set; }
        public int InStock { get; set; }
        public string CategoryName { get; set; }
        public string SearchInput { get; set; }  // For filtering/searching
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string SortBy { get; set; }       // For sorting (e.g., "Name", "Price", "CategoryName", "SupplierName")
        public bool Ascending { get; set; }      // Sorting direction (ascending or descending)
    }


    public class CreateProductModelDto
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Price is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Price must be greater than zero.")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "InStock quantity is required")]
        public int InStock { get; set; }

        [Required(ErrorMessage = "Supplier is required")]
        public int SupplierId { get; set; }

        [Required(ErrorMessage = "Category is required")]
        public int CategoryId { get; set; }

        public DateTime CreatedDate { get; set; }
    }

    public class UpdateProductModelDto
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Price is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Price must be greater than zero.")]
        public int Price { get; set; }
        [Required(ErrorMessage = "InStock is required")]
        public int InStock { get; set; }
        public int SupplierId { get; set; }
        [Required(ErrorMessage = "Category is required")]
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
