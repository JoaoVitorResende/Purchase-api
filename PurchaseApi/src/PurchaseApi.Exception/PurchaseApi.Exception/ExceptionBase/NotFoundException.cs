using PurchaseApi.Exception.ExceptionBase;
using System.Net;

namespace StoreApi.Exception.ExceptionBase
{
    public class NotFoundException: PurchaseExceptionBase
    {
        public NotFoundException(string message) : base(message)
        {

        }
        public override int StatusCode => (int)HttpStatusCode.NotFound;
        public override List<string> GetErros()
        {
            return [Message];
        }
    }
}
