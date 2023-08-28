namespace IdentityLogin.Repository
{
    public interface ISessionService
    {
        int GetSessionOrderId(HttpContext httpContext);
        void SetSessionOrderId(HttpContext httpContext, int orderId);
    }
}
