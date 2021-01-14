using System.Threading.Tasks;

namespace Infrastructure.Adapters.MediatR
{
    public interface IMediatRClient
    {
        Task PublishDomainEvent<T>(T domainEvent);
    }
}
