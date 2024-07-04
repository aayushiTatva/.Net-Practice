
using com.productManagementSystem.DBEntity.DataModels;
using com.productManagementSystem.DBEntity.ViewModels;
using com.productManagementSystem.generic.repositories.Interface;
using com.productManagementSystem.services.Interface;

namespace com.productManagementSystem.services
{
    public class ProductService : IProductService
    {
        #region Configuration
        private readonly IGenericRepositories<Product> _productRepository;
        private readonly IGenericRepositories<Category> _categoryRepository;
        private readonly IGenericRepositories<Supplier> _supplierRepository;
        private readonly IGenericRepositories<OrderProduct> _orderproductRepository;
        private readonly IGenericRepositories<Order> _orderRepository;

        public ProductService(IGenericRepositories<Product> productRepository, IGenericRepositories<Category> categoryRepository, IGenericRepositories<Supplier> supplierrepository,
            IGenericRepositories<OrderProduct> orderproductrepository, IGenericRepositories<Order> orderRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _supplierRepository = supplierrepository;
            _orderproductRepository = orderproductrepository;
            _orderRepository = orderRepository;
        }
        #endregion Configuration

        #region Product_GetAll
        public List<ProductModel> GetAll()
        {
            var pm = (from pr in _productRepository.GetAll()
                      join cm in _categoryRepository.GetAll()
                      on pr.CategoryId equals cm.Id into productCategoryGroup
                      from pcg in productCategoryGroup.DefaultIfEmpty()
                      where pr.Isdeleted == false
                      select new ProductModel
                      {
                          Id = pr.Id,
                          Name = pr.Name,
                          Price = (int)pr.Price,
                          SupplierName = _supplierRepository.GetAll().FirstOrDefault(e => e.Id == pr.SupplierId).FirstName + " " + _supplierRepository.GetAll().FirstOrDefault(e => e.Id == pr.SupplierId).LastName,
                          CategoryName = pcg.Name,
                          InStock = (int)pr.InStock,
                          CreatedDate = (DateTime)pr.CreatedDate
                      }).ToList();
            return pm;
        }

        /*public List<ProductModel> GetAll(ProductModel sortCriteria)
        {
            var query = from pr in _productRepository.GetAll()
                        join cm in _categoryRepository.GetAll()
                        on pr.CategoryId equals cm.Id into productCategoryGroup
                        from pcg in productCategoryGroup.DefaultIfEmpty()
                        where pr.Isdeleted == false
                        select new ProductModel
                        {
                            Id = pr.Id,
                            Name = pr.Name,
                            Price = (int)pr.Price,
                            SupplierName = _supplierRepository.GetAll().FirstOrDefault(e => e.Id == pr.SupplierId).FirstName + " " + _supplierRepository.GetAll().FirstOrDefault(e => e.Id == pr.SupplierId).LastName,
                            CategoryName = pcg.Name,
                            InStock = (int)pr.InStock,
                            CreatedDate = (DateTime)pr.CreatedDate
                        };

            if (sortCriteria != null && !string.IsNullOrEmpty(sortCriteria.SearchInput))
            {
                query = query.Where(p => p.Name.Contains(sortCriteria.SearchInput) ||
                                         p.SupplierName.Contains(sortCriteria.SearchInput) ||
                                         p.CategoryName.Contains(sortCriteria.SearchInput));
            }

            switch (sortCriteria.SortBy.ToLower())
            {
                case "name":
                    query = sortCriteria.Ascending ? query.OrderBy(p => p.Name) : query.OrderByDescending(p => p.Name);
                    break;
                case "price":
                    query = sortCriteria.Ascending ? query.OrderBy(p => p.Price) : query.OrderByDescending(p => p.Price);
                    break;
                case "categoryname":
                    query = sortCriteria.Ascending ? query.OrderBy(p => p.CategoryName) : query.OrderByDescending(p => p.CategoryName);
                    break;
                case "suppliername":
                    query = sortCriteria.Ascending ? query.OrderBy(p => p.SupplierName) : query.OrderByDescending(p => p.SupplierName);
                    break;
                default:
                    query = query.OrderBy(p => p.Name); // Default sort by Name ascending
                    break;
            }

            return query.ToList();
        }*/

        #endregion Product_GetAll

        #region Product_CreateProduct
        public async Task<bool> CreateProduct(CreateProductModelDto product)
        {
            Product sDto = new()
            {
                Name = product.Name,
                Price = (int)product.Price,
                InStock = product.InStock,
                SupplierId = (int)product.SupplierId,
                CreatedDate = DateTime.Now,
                CategoryId = product.CategoryId
            };
            _productRepository.AddAsync(sDto);
            return true;
        }
        #endregion Product_CreateProduct

        #region Product_GetById
        public ProductModel GetById(int Id)
        {
            var product = _productRepository.GetAll().FirstOrDefault(e => e.Id == Id);
            ProductModel cm = new()
            {
                Id = product.Id,
                Name = product.Name,
                Price = (int)product.Price,
                InStock = (int)product.InStock,
                SupplierId = (int)product.SupplierId,
                SupplierName = _supplierRepository.GetAll().FirstOrDefault(e => e.Id == product.SupplierId).FirstName + " " + _supplierRepository.GetAll().FirstOrDefault(e => e.Id == product.SupplierId).LastName,
                CategoryName = _categoryRepository.GetAll().FirstOrDefault(e => e.Id == product.CategoryId).Name,
                CreatedDate = (DateTime)product.CreatedDate
            };
            return cm;
        }
        #endregion Product_GetById

        #region Product_UpdateProduct
        public async Task<bool> UpdateProduct(int Id, UpdateProductModelDto product)
        {
            var uDto = _productRepository.GetAll().FirstOrDefault(e => e.Id == Id);
            if (uDto == null)
            {
                return false;
            }
            else
            {
                uDto.Name = product.Name;
                uDto.Price = product.Price;
                uDto.InStock = product.InStock;
                uDto.SupplierId = product.SupplierId;
                uDto.CategoryId = product.CategoryId;
                uDto.ModifiedDate = DateTime.Now;
                _productRepository.Update(uDto);
            }
            return true;
        }
        #endregion Product_UpdateProduct

        #region Product_DeleteProduct
        public async Task<bool> DeleteProduct(int Id)
        {
            var orders = _orderproductRepository.GetAll().FirstOrDefault(e => e.ProductId == Id);
            var product = _productRepository.GetAll().FirstOrDefault(e => e.Id == Id);
            if (product == null)
            {
                return false;
            }
            else
            {
                product.Isdeleted = true;
                _productRepository.Update(product);
            }
            return true;
        }
        #endregion Product_DeleteProduct
    }
}
