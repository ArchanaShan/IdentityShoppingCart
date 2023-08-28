namespace IdentityLogin.Repository
{
    public interface IOrderRepo
    {
        Task<int> IncreaseQuantity(int productId,int orderId);
        Task<int> UpdatedQuantity(int productId, int orderId);
        Task<int> DecreaseQuantity(int productId,int orderId);
    }
}
