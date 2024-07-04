
using System.ComponentModel.DataAnnotations;

namespace com.productManagementSystem.DBEntity.ViewModels
{
    public class SupplierModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string BusinessName { get; set; }
        public string ContactNumber { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string SearchInput { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }

    public class CreateSupplierModelDto
    {
        [Required(ErrorMessage = "FirstName is required")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "LastName is required")]
        public string LastName { get; set; }
        public string BusinessName { get; set; }
        [Required(ErrorMessage = "ContactNumber is required")]
        public string ContactNumber { get; set; }
        [Required(ErrorMessage = "Address is required")]
        public string Address { get; set; }
        [Required(ErrorMessage = "City is required")]
        public string City { get; set; }
        public DateTime CreatedDate { get; set; }
    }

    public class UpdateSupplierModelDto
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "FirstName is required")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "LastName is required")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "ContactNumber is required")]
        public string BusinessName { get; set; }
        public string ContactNumber { get; set; }
        [Required(ErrorMessage = "Address is required")]
        public string Address { get; set; }
        [Required(ErrorMessage = "City is required")]
        public string City { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
