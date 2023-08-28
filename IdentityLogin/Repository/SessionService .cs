namespace IdentityLogin.Repository
{
    public class SessionService:ISessionService
    {
        private const string SessionOrderId = "CurrentOrderId";

        public int GetSessionOrderId(HttpContext httpContext)
        {
            return httpContext.Session.GetInt32(SessionOrderId) ?? 0;
        }

        public void SetSessionOrderId(HttpContext httpContext, int orderId)
        {
            httpContext.Session.SetInt32(SessionOrderId, orderId);
        }
    }
}
