using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PurchaseApi.Communication;
using PurchaseApi.Exception.ExceptionBase;

namespace PurchaseApi.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.Exception is PurchaseExceptionBase)
            {
                HandleProjectException(context);
            }
            else
            {
                ThrowUnkowError(context);
            }
        }

        public void HandleProjectException(ExceptionContext context)
        {
            var storeApiException = (PurchaseExceptionBase)context.Exception;
            var errorResponse = new ResponsesErrorJson(storeApiException.GetErros());

            context.Result = new ObjectResult(errorResponse)
            {
                StatusCode = storeApiException.StatusCode
            };
            context.ExceptionHandled = true;
        }

        public void ThrowUnkowError(ExceptionContext context)
        {
            var errorResponse = new ResponsesErrorJson("Erro desconhecido");
            context.Result = new ObjectResult(errorResponse)
            {
                StatusCode = StatusCodes.Status500InternalServerError
            };
            context.ExceptionHandled = true;
        }
    }
}
