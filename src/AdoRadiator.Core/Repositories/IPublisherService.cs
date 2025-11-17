
namespace AdoRadiator.Core.Repositories
{
    public interface IPublisherService
    {
        Task PublishAsync(string body);
    }
}