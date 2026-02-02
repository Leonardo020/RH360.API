using MediatR;
using Microsoft.Extensions.Logging;

namespace RH360.Application.Behaviors
{
    public class LoggingBehavior<TRequest, TResponse>
        : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;

        public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
        {
            _logger = logger;
        }

        public async Task<TResponse> Handle(
            TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation("Init {Request}", typeof(TRequest).Name);

            var response = await next();

            _logger.LogInformation("Finish {Request}", typeof(TRequest).Name);

            return response;
        }
    }

}
