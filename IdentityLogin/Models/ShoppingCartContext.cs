using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IdentityLogin.Models
{
    public class ShoppingCartContext : IdentityDbContext<ApplicationUser>
    {
        public ShoppingCartContext()
        {
        }

        public ShoppingCartContext(DbContextOptions<ShoppingCartContext> options)
            : base(options)
        {
           
        }
        protected ShoppingCartContext(DbContextOptions options)
            : base(options)
        {

        }

    }
}
