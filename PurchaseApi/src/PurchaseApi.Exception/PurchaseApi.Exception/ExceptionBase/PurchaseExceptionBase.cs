namespace PurchaseApi.Exception.ExceptionBase
{
    public abstract class PurchaseExceptionBase : SystemException
    {
        public PurchaseExceptionBase(string message) : base(message)
        {
            
        }
        public abstract int StatusCode { get; }
        public abstract List<string> GetErros();
    }
}
