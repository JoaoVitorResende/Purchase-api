using PurchaseApi.Exception.ExceptionBase;
using System.Net;

namespace StoreApi.Exception.ExceptionBase
{
    public class ErrorOnValidationException: PurchaseExceptionBase
    {
        private readonly List<string> _erros;

        public override int StatusCode => (int)HttpStatusCode.BadRequest;

        public ErrorOnValidationException(List<string> errorMessages) : base(string.Empty)
        {
            _erros = errorMessages;
        }
        public override List<string> GetErros()
        {
            return _erros;
        }
    }
}
