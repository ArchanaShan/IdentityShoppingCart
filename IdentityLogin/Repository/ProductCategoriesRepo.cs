using IdentityLogin.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace IdentityLogin.Repository
{
    public class ProductCategoriesRepo: IProductCategoriesRepo
    {
        private readonly ShoppingCartsContext _dbContext;

        public ProductCategoriesRepo(ShoppingCartsContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<SelectListItem> GetItemsForDropdown()
        {

            List<ProductsCategory> lstProductsCategories = _dbContext.ProductsCategories.ToList();
            var selectListItems = lstProductsCategories.Select(item => new SelectListItem
            {
                Value = item.ProductCategoryId.ToString(),
                Text = item.Name
            }).ToList();

            return selectListItems;
        }
        public List<Product> GetItemsFromProducts(int selectedItemId)
        {
            List<Product> lstProduct = _dbContext.Products.Where(x => x.ProductCategoryId == selectedItemId).ToList();
            return lstProduct;
        }
    }
}
