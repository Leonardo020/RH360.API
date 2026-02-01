namespace RH360.Domain.Payloads
{
    public class GenericResponsePayload
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;

        public GenericResponsePayload()
        {
            
        }

        public GenericResponsePayload(bool success, string message)
        {
            Success = success;
            Message = message;
        }
    }
}
