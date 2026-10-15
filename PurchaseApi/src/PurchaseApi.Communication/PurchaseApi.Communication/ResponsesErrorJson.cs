namespace PurchaseApi.Communication
{
    public class ResponsesErrorJson
    {
        public List<string> ErrorMessages { get; set; }
        public ResponsesErrorJson(List<string> message)
        {
            ErrorMessages = message;
        }
        public ResponsesErrorJson(string message)
        {
            ErrorMessages = [message];
        }
    }
}
