using Microsoft.AspNetCore.Mvc;
using IdentityLogin.Models;
using IdentityLogin.Repository;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace IdentityLogin.Controllers
{
    public class ProductsCategoriesController : Controller
    {
        private readonly ShoppingCartsContext _context;
        private readonly IProductCategoriesRepo _productCategoriesRepo;
        public ProductsCategoriesController(ShoppingCartsContext context,IProductCategoriesRepo productCategoriesRepo)
        {
            _context = context;
            _productCategoriesRepo = productCategoriesRepo;
        } 
        public IActionResult Categories()
        {
            var productcategory = _productCategoriesRepo.GetItemsForDropdown();
            List<Product> lstProduct = _context.Products.ToList();
            ViewBag.ItemsForDropdown = productcategory;

            return View(lstProduct);
        }
        [HttpPost]
        public IActionResult GetItemCategoryList(int selectedItemId)
        {
            var itemList =_productCategoriesRepo.GetItemsFromProducts(selectedItemId);
            return PartialView("_ProductList", itemList);
        }
        [HttpPost]
        public IActionResult Search(string searchTerm)
        {
            List<Product> listProduct = _context.Products.Where(x => x.Name == searchTerm).ToList();
            return PartialView("_ProductList", listProduct);
        }
        //public List<SelectListItem> GetItemsForDropdown()
        //{

        //    List<ProductsCategory> lstProductsCategories = _context.ProductsCategories.ToList();
        //    var selectListItems = lstProductsCategories.Select(item => new SelectListItem
        //    {
        //        Value = item.ProductCategoryId.ToString(),
        //        Text = item.Name
        //    }).ToList();

        //    return selectListItems;
        //}
        //public List<Product> GetItemsFromProducts(int selectedItemId)
        //{
        //    List<Product> lstProduct = _context.Products.Where(x => x.ProductCategoryId == selectedItemId).ToList();
        //    return lstProduct;
        //}
    }
}
