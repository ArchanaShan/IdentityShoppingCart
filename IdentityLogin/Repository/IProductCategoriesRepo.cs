using IdentityLogin.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace IdentityLogin.Repository
{
    public interface IProductCategoriesRepo
    {
        List<SelectListItem> GetItemsForDropdown();
        List<Product> GetItemsFromProducts(int selectedItemId);
    }
}
