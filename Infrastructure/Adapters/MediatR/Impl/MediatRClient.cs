using MediatR;
using System.Threading.Tasks;

namespace Infrastructure.Adapters.MediatR.Impl
{
    public class MediatRClient : IMediatRClient
    {
        private readonly IMediator _mediator;

        public MediatRClient(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task PublishDomainEvent<T>(T domainEvent)
        {
            await _mediator.Publish(domainEvent);
        }
    }
}
