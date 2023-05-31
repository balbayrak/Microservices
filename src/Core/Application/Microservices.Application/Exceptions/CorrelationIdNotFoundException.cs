namespace Microservices.Application.Exceptions
{
    public class CorrelationIdNotFoundException : NotFoundException
    {
        public const string exceptionMessage ="X-Correlation-ID header not found in request headers";
        public CorrelationIdNotFoundException() : base(exceptionMessage)
        {
        }
    }
}
