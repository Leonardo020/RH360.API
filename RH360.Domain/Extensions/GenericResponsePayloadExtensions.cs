using RH360.Domain.Payloads;

namespace RH360.Domain.Extensions
{
    public static class GenericResponsePayloadExtensions
    {
        public static GenericResponsePayload AsSuccess(this GenericResponsePayload payload, string? message = null)
        {
            payload.Success = true;
            if (!string.IsNullOrWhiteSpace(message))
                payload.Message = message;

            return payload;
        }

        public static GenericResponsePayload AsError(this GenericResponsePayload payload, string? message = null)
        {
            payload.Success = false;
            if (!string.IsNullOrWhiteSpace(message))
                payload.Message = message;

            return payload;
        }

        public static GenericResponsePayload AppendMessage(this GenericResponsePayload payload, string additional)
        {
            if (!string.IsNullOrWhiteSpace(additional))
            {
                if (string.IsNullOrEmpty(payload.Message))
                    payload.Message = additional;
                else
                    payload.Message = $"{payload.Message} {additional}";
            }

            return payload;
        }

        public static GenericResponsePayload WithMessage(this GenericResponsePayload payload, string message)
        {
            payload.Message = message;
            return payload;
        }

        public static GenericResponsePayload ToSuccess(this string message)
            => new GenericResponsePayload(true, message);

        public static GenericResponsePayload ToError(this string message)
            => new GenericResponsePayload(false, message);
    }
}
