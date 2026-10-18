using System.Net;

namespace PurchaseApi.Exception.ExceptionBase
{
    public class PurchaseFailedException : PurchaseExceptionBase
    {
        public PurchaseFailedException(string message) : base(message)
        {
            
        }
        public override int StatusCode => (int)HttpStatusCode.NotFound;
        public override List<string> GetErros()
        {
            return [Message];
        }
    }
}
